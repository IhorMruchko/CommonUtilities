using System;
using System.Linq;

namespace CommonUtilities.Conversion
{
    public struct DecimalConverter : IConverter
    {
        public uint Priority => 0;
        
        public Type TargetType => typeof(decimal);

        public bool CanConvert(string value)
            => value.EndsWith("d") &&
            (value.Count(i => i == '.') == 1
            || value.Count(i => i == ',') == 1)
            && decimal.TryParse(value.Replace('.', ',').TrimEnd('d'), out _); 

        public object Convert(string value) 
            => decimal.Parse(value.Replace('.', ',').TrimEnd('d'));
    }
}