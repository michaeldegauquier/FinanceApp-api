using System.Reflection;

namespace FinanceApp.Shared.Core.Helpers
{
    public static class AttributeReader
    {
        public static TAttribute? ReadAttribute<T, TAttribute>(this T value) 
            where T : struct 
            where TAttribute : Attribute
        {
            var memberInfo = GetMemberInfo(value);
            if (memberInfo == null)
                return null;

            return GetAttribute<TAttribute>(memberInfo);
        }

        private static MemberInfo? GetMemberInfo<T>(T enumValue) where T : struct
        {
            return typeof(T).GetMember(enumValue.ToString() ?? "")?.FirstOrDefault();
        }

        private static TAttribute? GetAttribute<TAttribute>(MemberInfo memberInfo) where TAttribute : Attribute
        {
            if (memberInfo == null)
                return null;

            return memberInfo.GetCustomAttribute<TAttribute>(false);
        }
    }
}
