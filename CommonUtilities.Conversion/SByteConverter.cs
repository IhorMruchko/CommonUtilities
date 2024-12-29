using System;
using System.Linq;

namespace CommonUtilities.Conversion
{
    public struct SByteConverter : IConverter
    {
        public uint Priority => 0;
        
        public Type TargetType => typeof(sbyte);

        public bool CanConvert(string value)
            => value.EndsWith("sb") && sbyte.TryParse(value.TrimEnd('b').TrimEnd('s'), out _); 

        public object Convert(string value) 
            => sbyte.Parse(value.TrimEnd('b').TrimEnd('s'));
    }
}