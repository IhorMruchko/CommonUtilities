using System;
using CommonUtilities.Conversion.Base;

namespace CommonUtilities.Conversion.NumericConverters
{
    /// <inheritdoc />
    public struct ByteConverter : IConverter
    {
        /// <inheritdoc />
        public uint Priority => 0;

        /// <inheritdoc />
        public Type TargetType => typeof(byte);

        /// <inheritdoc />
        public bool CanConvert(string value)
            => value.EndsWith("b") && byte.TryParse(value.TrimEnd('b'), out _);

        /// <inheritdoc />
        public object Convert(string value) 
            => byte.Parse(value.TrimEnd('b'));
    }
}