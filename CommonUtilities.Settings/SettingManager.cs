using System;
using System.Collections.Generic;
using System.Linq;
using CommonUtilities.Settings.Extensions;
using CommonUtilities.Settings.Settings;

namespace CommonUtilities.Settings
{
    public class SettingManager
    {
        private static SettingManager _instance;
        
        private static readonly Dictionary<string, Setting> Settings;
        
        private SettingManager() {}
        
        public static SettingManager Instance => _instance ?? (_instance = new SettingManager());
        
        static SettingManager()
        {
            Settings = AppDomain.CurrentDomain.GetAssemblies()
                                 .SelectMany(assembly => assembly.GetTypes())
                                 .Where(type => type.IsValidSettingType())
                                 .SelectMany(type => type.ToSettings())
                                 .ToDictionary(t => t.Key, t => t);
            
            Console.WriteLine(string.Join("\n", Settings.Select(t => $"{t.Key}: {t.Value}")));
        }

        public object this[string key]
        {
            get => Settings[key].Value;
            set => Settings[key].Value = value;
        }
    }
}