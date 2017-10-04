using Autofac;
using Autofac.Extensions.DependencyInjection;
using Saleman.Data.EntityFramework;
using WebFramework.Infrastructure.Initialization;
using Saleman.Web.Infrastructure.DependencyInjection;
using Saleman.Web.Infrastructure.ErrorHandling;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using NLog.Web;
using Swashbuckle.AspNetCore.Swagger;
using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Saleman.Web.Api.Swaggers;
using Microsoft.Extensions.FileProviders;
using System.Reflection;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.Net.Http.Headers;

namespace Saleman.Web.Api
{
    public class Startup
    {
        IHostingEnvironment _hostingEnvironment;
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddJsonFile("payments.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"payments.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();

            // Logs
            env.ConfigureNLog("nlog.config");

            this._hostingEnvironment = env;
        }

        public IConfigurationRoot Configuration { get; }

        public IContainer ApplicationContainer { get; private set; }

        // ConfigureServices is where you register dependencies. This gets
        // called by the runtime before the Configure method, below.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            // Angular's default header name for sending the XSRF token.
            services.AddAntiforgery(options => options.HeaderName = Saleman.Web.Infrastructure.Constants.AntiforgeryKeys.AntiforgeryHeader);

            // Add caching
            services.AddMemoryCache();

            // Add services to the collection.
            services.AddMvc()
                .AddControllersAsServices();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            // Register the Swagger generator, defining one or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Saleman Api", Version = "v1" });
                c.OperationFilter<AuthorizationHeaderParameterOperationFilter>();
            });

            services.AddApiVersioning(
                o =>
                {
                    o.AssumeDefaultVersionWhenUnspecified = true;
                    o.DefaultApiVersion = new ApiVersion(1, 0);
                });

            services.AddDbContext<SalemanDbContext>(options =>
            {
                options.UseSqlite("FileName=./Saleman.db");
                //options.UseMySql(@"Server=localhost;database=saleman;uid=saleman;pwd=Saleman#2017;");

                // Register the entity sets needed by OpenIddict.
                // Note: use the generic overload if you need
                // to replace the default OpenIddict entities.
                options.UseOpenIddict();
            });

            // Identity
            services.AddIdentity<IdentityUser, IdentityRole>(IdentityOptions)
                .AddEntityFrameworkStores<SalemanDbContext>()
                .AddDefaultTokenProviders();

            services.AddOpenIddict(options =>
            {
                // Register the Entity Framework stores.
                options.AddEntityFrameworkCoreStores<SalemanDbContext>();
                // Register the ASP.NET Core MVC binder used by OpenIddict.
                // Note: if you don't call this method, you won't be able to
                // bind OpenIdConnectRequest or OpenIdConnectResponse parameters.
                options.AddMvcBinders();
                // Enable the token endpoint.
                options.EnableTokenEndpoint("/connect/token");
                // Enable the password flow.
                options.AllowPasswordFlow();
                // During development, you can disable the HTTPS requirement.
                options.DisableHttpsRequirement();
            });

            // Register file provider
            RegisterFileProvider(services);

            // Caching
            //services.AddEnyimMemcached(options => Configuration.GetSection("enyimMemcached").Bind(options));

            // Create the container builder.
            var builder = new ContainerBuilder();

            // Register services
            builder.RegisterInstance(this.Configuration)
                            .As<IConfiguration>()
                            .SingleInstance();

            // Register dependencies, populate the services from
            // the collection, and build the container. If you want
            // to dispose of the container at the end of the app,
            // be sure to keep a reference to it as a property or field.
            IServiceCollectionBuilder autofacServices = new AutofacServiceCollection(builder);
            InitializationContext configurationContext = new AutofacInitializationContext(autofacServices);

            // Initialize modules
            InitializationProcessor.Initialize(configurationContext);

            builder.Populate(services);
            this.ApplicationContainer = builder.Build();

            // Create the IServiceProvider based on the container.
            return new AutofacServiceProvider(this.ApplicationContainer);
        }

        private static void IdentityOptions(IdentityOptions options)
        {
            options.Cookies.ApplicationCookie.LoginPath = "/Account/SignIn";
        }

        private void RegisterFileProvider(IServiceCollection services)
        {
            // File 
            var physicalProvider = _hostingEnvironment.ContentRootFileProvider;
            //var embeddedProvider = new EmbeddedFileProvider(Assembly.GetEntryAssembly());
            //var compositeProvider = new CompositeFileProvider(physicalProvider, embeddedProvider);

            services.AddSingleton<IFileProvider>(physicalProvider);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions
                {
                    HotModuleReplacement = true
                });
            }
            else
            {
                loggerFactory.AddConsole(Configuration.GetSection("Logging"));
                loggerFactory.AddDebug();
            }

            app.UseCors(builder =>
                // This will allow any request from any server. Tweak to fit your needs!
                // The fluent API is pretty pleasant to work with.
                builder.AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowAnyOrigin()
            );

            // Register the validation middleware, that is used to decrypt
            // the access tokens and populate the HttpContext.User property.
            app.UseOAuthValidation();
            // Register the OpenIddict middleware.
            app.UseOpenIddict();

            //add NLog to ASP.NET Core
            loggerFactory.AddNLog();

            //add NLog.Web
            app.AddNLogWeb();

            // Caching
            //app.UseEnyimMemcached();

            // Add error handling
            app.UseContentNegotiateExceptionHandling();

            app.UseStaticFiles(new StaticFileOptions
            {
                OnPrepareResponse = context =>
                {
                    context.Context.Response.Headers["Cache-Control"] =
                "private, max-age=43200";

                    context.Context.Response.Headers["Expires"] =
                            DateTime.UtcNow.AddHours(12).ToString("R");
                }
            });

            //app.UseMvcWithDefaultRoute();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                routes.MapSpaFallbackRoute(
                    name: "spa-fallback",
                    defaults: new { controller = "Home", action = "Index" });
            });

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUi(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Saleman Leave Management Api V1");
            });
        }
    }
}
