using Microsoft.AspNetCore.Http;

namespace Maruko.AspNetMvc.Extensions
{
    public class HttpContextUtil
    {
        private static IHttpContextAccessor _accessor;
        public static HttpContext Current => _accessor.HttpContext;
        public static void Configure(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }
    }
}
