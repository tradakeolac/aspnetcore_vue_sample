namespace WebFramework.Infrastructure.Helpers
{
    public interface IHttpUtility
    {
        string UrlEncode(string value);
        string HtmlEncode(string value);
    }
}
