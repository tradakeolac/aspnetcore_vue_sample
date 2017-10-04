namespace Saleman.Web.Infrastructure.Helpers
{
    using WebFramework.Infrastructure.Helpers;
    using System.Net;

    public sealed class WebUtilityAdapter : IHttpUtility
    {
        public string HtmlEncode(string value)
        {
            return WebUtility.HtmlEncode(value);
        }

        public string UrlEncode(string value)
        {
            return WebUtility.UrlEncode(value);
        }
    }
}
