using System;
using System.Linq;
using CommonUtilities.Extensions;

namespace CommonUtilities.Conversion
{
    public struct BooleanConverter : IConverter
    {
        public uint Priority => 0;
        
        public Type TargetType => typeof(bool);

        public bool CanConvert(string value)
            => value.In("true", "false", "yes", "no", "0", "1");
        public object Convert(string value) 
            => value.In("true", "yes", "1");
    }
}