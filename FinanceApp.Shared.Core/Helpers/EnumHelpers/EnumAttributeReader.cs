using FinanceApp.Shared.Core.Attributes;
using System.Reflection;

namespace FinanceApp.Shared.Core.Helpers.EnumHelpers
{
    public static class EnumAttributeReader
    {
        public static int GetStatusCode<TEnum>(this TEnum enumValue) where TEnum : struct
        {
            var memberInfo = GetEnumMemberInfo(enumValue);
            if (memberInfo == null)
                return 0;
            var messageAttribute = GetAttribute<StatusCodeAttribute>(memberInfo);

            return messageAttribute?.StatusCode ?? 0;
        }

        public static string GetMessage<TEnum>(this TEnum enumValue) where TEnum : struct
        {
            var memberInfo = GetEnumMemberInfo(enumValue);
            if (memberInfo == null)
                return "";
            var messageAttribute = GetAttribute<MessageAttribute>(memberInfo);

            return messageAttribute?.Message ?? enumValue.ToString() ?? "";
        }

        private static MemberInfo? GetEnumMemberInfo<T>(T enumValue) where T : struct
        {
            if (!IsEnumType(enumValue))
                throw new ArgumentException("enumValue must be of Enum type", nameof(enumValue));

            return typeof(T).GetMember(enumValue.ToString() ?? "")?.FirstOrDefault();
        }

        private static bool IsEnumType<T>(T enumValue) where T : struct
        {
            return typeof(T).IsEnum;
        }

        private static TAttribute? GetAttribute<TAttribute>(MemberInfo memberInfo) where TAttribute : Attribute
        {
            if (memberInfo == null)
                return null;

            return memberInfo.GetCustomAttribute<TAttribute>(false);
        }
    }
}
