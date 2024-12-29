using System;
using System.Linq;

namespace CommonUtilities.Conversion
{
    public struct FloatConverter : IConverter
    {
        public uint Priority => 0;
        
        public Type TargetType => typeof(double);

        public bool CanConvert(string value)
            => value.EndsWith("f") 
            && (value.Count(i => i == '.') == 1
            || value.Count(i => i == ',') == 1)
            && float.TryParse(value.Replace('.', ',').TrimEnd('f'), out _); 

        public object Convert(string value) 
            => float.Parse(value.Replace('.', ',').TrimEnd('f'));
    }
}