namespace Saleman.Web.Infrastructure.Attributes
{
    using Microsoft.AspNetCore.Mvc;

    /// <content>
    /// Provides additional implementation specific to ASP.NET Core.
    /// </content>
    /// <summary>
    /// Represents the metadata that describes the <see cref="T:Microsoft.AspNetCore.Mvc.ApiVersion">API versions</see> associated with a service.
    /// </summary>
    public class ApiConfigurableVersionAttribute : ApiVersionAttribute
    {
        protected ApiConfigurableVersionAttribute(ApiVersion version) : base(version)
        {
        }
    }
}
