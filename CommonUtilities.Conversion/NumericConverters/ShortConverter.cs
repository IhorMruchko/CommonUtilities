using System;
using CommonUtilities.Conversion.Base;

namespace CommonUtilities.Conversion.NumericConverters
{
    /// <inheritdoc />
    public struct ShortConverter : IConverter
    {
        /// <inheritdoc />
        public Type TargetType => typeof(short);

        /// <inheritdoc />
        public bool CanConvert(string value) 
            => value.EndsWith("s")
            && short.TryParse(value.TrimEnd('s'), out _);

        /// <inheritdoc />
        public object Convert(string value) 
            => short.Parse(value.TrimEnd('s'));
    }
}