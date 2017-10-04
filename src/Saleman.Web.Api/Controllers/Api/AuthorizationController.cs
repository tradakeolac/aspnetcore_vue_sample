using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AspNet.Security.OpenIdConnect.Server;
using AspNet.Security.OpenIdConnect.Primitives;
using System.Security.Claims;
using AspNet.Security.OpenIdConnect.Extensions;
using Saleman.Model.ServiceObjects;
using Saleman.Web.ViewModel;
using Saleman.Service;

namespace Saleman.Web.Api.Controllers.Api
{
    public class AuthorizationController : SalemanApiControllerBase
    {
        protected readonly IAuthenticationService AuthenticationService;
        public AuthorizationController(IServiceObjectFactory objectFactory, IViewModelFactory viewModelFactory,
            IAuthenticationService authenticationService, IUserService userService)
            : base(objectFactory, viewModelFactory, userService)
        {
            this.AuthenticationService = authenticationService;
        }

        [HttpPost("~/connect/token"), Produces("application/json")]
        public async Task<IActionResult> Exchange([FromForm] OpenIdConnectRequest request)
        {
            if (request.IsPasswordGrantType())
            {
                var user = await this.AuthenticationService.AuthenticateAsync(request.Username, request.Password);

                if (user.IsNullObject())
                {
                    return Forbid(OpenIdConnectServerDefaults.AuthenticationScheme);
                }
                // Create a new ClaimsIdentity holding the user identity.
                var identity = new ClaimsIdentity(
                    OpenIdConnectServerDefaults.AuthenticationScheme,
                    OpenIdConnectConstants.Claims.Name, null);
                // Add a "sub" claim containing the user identifier, and attach
                // the "access_token" destination to allow OpenIddict to store it
                // in the access token, so it can be retrieved from your controllers.
                identity.AddClaim(OpenIdConnectConstants.Claims.Subject, user.Id,
                    OpenIdConnectConstants.Destinations.AccessToken);
                identity.AddClaim(OpenIdConnectConstants.Claims.Name, user.Email,
                    OpenIdConnectConstants.Destinations.AccessToken);

                // ... add other claims, if necessary.
                var principal = new ClaimsPrincipal(identity);
                // Ask OpenIddict to generate a new token and return an OAuth2 token response.
                return SignIn(principal, OpenIdConnectServerDefaults.AuthenticationScheme);
            }
            throw new InvalidOperationException("The specified grant type is not supported.");
        }
    }
}
