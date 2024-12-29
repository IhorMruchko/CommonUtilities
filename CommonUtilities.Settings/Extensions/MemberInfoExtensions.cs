using System.Reflection;
using CommonUtilities.Settings.Settings;

namespace CommonUtilities.Settings.Extensions
{
    internal static class MemberInfoExtensions
    {
        public static Setting ToSetting(this MemberInfo memberInfo) =>
            new Setting
            {
                Key = (memberInfo.DeclaringType?.Name ?? "") + "." + memberInfo.Name,
                Member = memberInfo,
                Value = memberInfo.GetValue()
            };

        public static void SetValue(this MemberInfo memberInfo, object value)
        {
            switch (memberInfo)
            {
                case FieldInfo fieldInfo:
                    fieldInfo.SetValue(null, value);
                    break;
                case PropertyInfo propertyInfo:
                    propertyInfo.SetValue(null, value);
                    break;
            }
        }
        
        private static object GetValue(this MemberInfo memberInfo)
        {
            switch (memberInfo)
            {
                case FieldInfo fieldInfo:
                    return fieldInfo.GetValue(null);
                case PropertyInfo propertyInfo:
                    return propertyInfo.GetValue(null);
                default:
                    return null;
            }
        }
    }
}