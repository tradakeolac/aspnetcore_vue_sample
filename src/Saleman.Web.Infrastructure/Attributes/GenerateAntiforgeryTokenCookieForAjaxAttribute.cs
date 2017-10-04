namespace Saleman.Web.Infrastructure.Attributes
{
    using Microsoft.AspNetCore.Antiforgery;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc.Filters;

    public class GenerateAntiforgeryTokenCookieForAjaxAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            var antiforgery = context.HttpContext.RequestServices.GetService(typeof(IAntiforgery)) as IAntiforgery;

            // We can send the request token as a JavaScript-readable cookie, 
            // and Angular will use it by default.
            var tokens = antiforgery.GetAndStoreTokens(context.HttpContext);
            context.HttpContext.Response.Cookies.Append(
                Constants.AntiforgeryKeys.AntiforgeryHeader,
                tokens.RequestToken,
                new CookieOptions() { HttpOnly = false });
        }
    }
}
