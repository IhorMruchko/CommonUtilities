using System;
using System.Linq;

namespace CommonUtilities.Conversion
{
    public struct ByteConverter : IConverter
    {
        public uint Priority => 0;
        
        public Type TargetType => typeof(byte);

        public bool CanConvert(string value)
            => value.EndsWith("b") && byte.TryParse(value.TrimEnd('b'), out _); 

        public object Convert(string value) 
            => byte.Parse(value.TrimEnd('b'));
    }
}