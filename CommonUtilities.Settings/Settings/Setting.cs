using System.Reflection;
using CommonUtilities.Settings.Extensions;

namespace CommonUtilities.Settings.Settings
{
    public class Setting
    {
        private object _value;
        public string Key { get; set; }
        
        public MemberInfo Member { get; set; }

        public object Value
        {
            get => _value;
            set
            {
                _value = value;
                Member.SetValue(value);
            }
        }
    }
}