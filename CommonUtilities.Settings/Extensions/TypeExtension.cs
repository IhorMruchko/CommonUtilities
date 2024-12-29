using System;
using System.Linq;
using System.Reflection;
using CommonUtilities.Settings.Attributes;
using CommonUtilities.Settings.Settings;

namespace CommonUtilities.Settings.Extensions
{
    internal static class TypeExtension
    {
        public static bool IsValidSettingType(this Type type)
            => type.GetCustomAttribute<SettingProviderAttribute>() != null
               || type.GetFields(BindingFlags.Static | BindingFlags.Public)
                   .Any(m => m.GetCustomAttribute<SettingAttribute>() != null)
               || type.GetProperties(BindingFlags.Static | BindingFlags.Public)
                   .Any(p => p.GetCustomAttribute<SettingAttribute>() != null);

        public static Setting[] ToSettings(this Type type)
        {
            if (type.GetCustomAttribute<SettingProviderAttribute>() == null)
            {
                return type.GetMemberInfo()
                           .Where(info => info.GetCustomAttribute<SettingAttribute>() != null)
                           .Select(i => i.ToSetting())
                           .ToArray();
            }
            
            return type.GetMemberInfo()
                       .Select(info => info.ToSetting())
                       .ToArray();
        }

        private static MemberInfo[] GetMemberInfo(this Type type) =>
            type.GetFields(BindingFlags.Static | BindingFlags.Public)
                .Cast<MemberInfo>()
                .Concat(type.GetProperties(BindingFlags.Static | BindingFlags.Public))
                .ToArray();
    }
}