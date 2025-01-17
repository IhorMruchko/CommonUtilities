using System;
using CommonUtilities.Conversion.Base;

namespace CommonUtilities.Conversion.NumericConverters
{
    /// <inheritdoc />
    public struct LongConverter : IConverter
    {
        /// <inheritdoc />
        public Type TargetType => typeof(long);

        /// <inheritdoc />
        public bool CanConvert(string value) 
            => value.EndsWith("l")
            && long.TryParse(value.TrimEnd('l'), out _);

        /// <inheritdoc />
        public object Convert(string value) 
            => long.Parse(value.TrimEnd('l'));
    }
}