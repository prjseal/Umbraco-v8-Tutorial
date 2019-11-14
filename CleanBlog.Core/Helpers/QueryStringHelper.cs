using System.Web;

namespace CleanBlog.Core.Helpers
{
    public static class QueryStringHelper
    {
        public static int GetIntFromQueryString(HttpRequestBase request, string key, int fallbackValue = 0)
        {
            var stringValue = request.QueryString[key];
            if (stringValue != null && !string.IsNullOrWhiteSpace(stringValue) &&
                int.TryParse(stringValue, out var numericValue))
            {
                return numericValue;
            }

            return fallbackValue;
        }
    }
}
