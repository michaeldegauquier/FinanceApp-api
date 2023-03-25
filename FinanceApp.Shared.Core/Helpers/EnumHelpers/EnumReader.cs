using System.ComponentModel;
using System.Reflection;

namespace FinanceApp.Shared.Core.Helpers.EnumHelpers
{
    public static class EnumReader
    {
        public static string GetDescription<T>(this T enumValue) where T : struct
        {
            Type type = enumValue.GetType();
            if (!type.IsEnum)
                throw new ArgumentException("EnumerationValue must be of Enum type", "enumerationValue");

            MemberInfo[] memberInfo = type.GetMember(enumValue.ToString() ?? "");
            if (memberInfo != null && memberInfo.Length > 0)
            {
                object[] attrs = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (attrs != null && attrs.Length > 0)
                    return ((DescriptionAttribute)attrs[0]).Description;
            }
            return enumValue.ToString() ?? "";
        }
    }
}
