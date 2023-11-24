using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Http;

namespace UHP.WebApi.Configurations
{
    public static class ResponseConfiguration
    {
        public static void AddApplicationError(this HttpResponse response, string message)
        {
            response.Headers.Add("Application-Error", Strings.RemoveAllNonPrintableCharacters(message));
            // CORS
            response.Headers.Add("access-control-expose-headers", "Application-Error");
        }
    }
    
    public static class Strings
    {
        public static string RemoveAllNonPrintableCharacters(string target)
        {
            return Regex.Replace(target, @"\p{C}+", string.Empty);
        }
    }
}