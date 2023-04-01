using FinanceApp.Shared.Core.Attributes;
using FinanceApp.Shared.Core.Helpers;
using System.Net;

namespace FinanceApp.Shared.Core.Extensions
{
    public static class EnumExtensions
    {
        public static HttpStatusCode GetStatusCode<T>(this T enumValue) where T : struct
        {
            return enumValue.ReadAttribute<T, StatusCodeAttribute>()?.StatusCode ?? HttpStatusCode.OK;
        }

        public static string GetMessage<T>(this T enumValue) where T : struct
        {
            return enumValue.ReadAttribute<T, MessageAttribute>()?.Message ?? "";
        }
    }
}
