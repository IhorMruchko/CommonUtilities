using System;

namespace CommonUtilities.Conversion
{
    public struct StringConverter : IConverter
    {
        public uint Priority => 0;
        
        public Type TargetType => typeof(string);

        public bool CanConvert(string value)
            => true;

        public object Convert(string value) 
            => value;
    }
}

