using System;
using System.Linq;
using CommonUtilities.Conversion.Base;

namespace CommonUtilities.Conversion
{
    public struct CharConverter : IConverter
    {
        public uint Priority => 0;
        
        public Type TargetType => typeof(char);

        public bool CanConvert(string value) 
            => value.Any(char.IsLetter)
            && char.TryParse(value.FirstOrDefault(char.IsLetter).ToString(), out _);

        public object Convert(string value)
            => char.Parse(value.FirstOrDefault(char.IsLetter).ToString());
    }
}