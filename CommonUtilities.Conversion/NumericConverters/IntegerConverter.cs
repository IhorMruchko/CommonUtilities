using System;
using CommonUtilities.Conversion.Base;

namespace CommonUtilities.Conversion.NumericConverters
{
    /// <inheritdoc />
    public struct IntegerConverter : IConverter
    {
        /// <inheritdoc />
        public Type TargetType => typeof(int);

        /// <inheritdoc />
        public bool CanConvert(string value)
          => int.TryParse(value, out _);

        /// <inheritdoc />
        public object Convert(string value) 
            => int.Parse(value);
    }
}