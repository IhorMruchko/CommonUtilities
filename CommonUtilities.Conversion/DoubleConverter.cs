using System;
using System.Linq;

namespace CommonUtilities.Conversion
{
    public struct DoubleConverter : IConverter
    {
        public uint Priority => 0;
        
        public Type TargetType => typeof(float);

        public bool CanConvert(string value)
            => (value.Count(i => i == '.') == 1
            || value.Count(i => i == ',') == 1)
            && double.TryParse(value.Replace('.', ','), out _); 

        public object Convert(string value) 
            => double.Parse(value.Replace('.', ','));
    }
}