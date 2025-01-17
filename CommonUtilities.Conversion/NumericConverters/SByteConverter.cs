using System;
using CommonUtilities.Conversion.Base;

namespace CommonUtilities.Conversion.NumericConverters
{
    /// <inheritdoc />
    public struct SByteConverter : IConverter
    {
        /// <inheritdoc />
        public Type TargetType => typeof(sbyte);

        /// <inheritdoc />
        public bool CanConvert(string value)
            => value.EndsWith("sb") 
            && sbyte.TryParse(value.TrimEnd('b').TrimEnd('s'), out _);

        /// <inheritdoc />
        public object Convert(string value) 
            => sbyte.Parse(value.TrimEnd('b').TrimEnd('s'));
    }
}