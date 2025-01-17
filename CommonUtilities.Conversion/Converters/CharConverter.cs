using System;
using System.Linq;
using CommonUtilities.Conversion.Base;

namespace CommonUtilities.Conversion.Converters
{
    /// <inheritdoc />
    public struct CharConverter : IConverter
    {
        /// <inheritdoc />
        public Type TargetType => typeof(char);

        /// <inheritdoc />
        public bool CanConvert(string value) 
            => value.Any(char.IsLetter)
            && char.TryParse(value.FirstOrDefault(char.IsLetter).ToString(), out _);

        /// <inheritdoc />
        public object Convert(string value)
            => char.Parse(value.FirstOrDefault(char.IsLetter).ToString());
    }
}