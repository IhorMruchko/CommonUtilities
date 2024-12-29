using System;

namespace CommonUtilities.Conversion
{
    public struct IntegerConverter : IConverter
    {
        public uint Priority => 0;
        
        public Type TargetType => typeof(int);

        public bool CanConvert(string value)
          => int.TryParse(value, out _); 

        public object Convert(string value) 
            => int.Parse(value);
    }
}