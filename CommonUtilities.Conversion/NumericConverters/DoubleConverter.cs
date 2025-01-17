using System;
using System.Linq;
using CommonUtilities.Conversion.Base;

namespace CommonUtilities.Conversion.NumericConverters
{
    /// <inheritdoc />
    public struct DoubleConverter : IConverter
    {
        /// <inheritdoc />
        public Type TargetType => typeof(double);

        /// <inheritdoc />
        public bool CanConvert(string value)
            => value.EndsWith("d") 
            && (value.Count(i => i == '.') <= 1
            || value.Count(i => i == ',') <= 1)
            && double.TryParse(value.Replace('.', ',').TrimEnd('d'), out _);

        /// <inheritdoc />
        public object Convert(string value) 
            => double.Parse(value.Replace('.', ',').TrimEnd('d'));
    }
}