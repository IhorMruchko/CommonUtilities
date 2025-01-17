using System;
using System.Linq;
using CommonUtilities.Conversion.Base;

namespace CommonUtilities.Conversion.NumericConverters
{
    /// <inheritdoc />
    public struct DecimalConverter : IConverter
    {
        /// <inheritdoc />
        public Type TargetType => typeof(decimal);

        /// <inheritdoc />
        public bool CanConvert(string value)
            => value.EndsWith("m") 
            && (value.Count(i => i == '.') <= 1
            || value.Count(i => i == ',') <= 1)
            && decimal.TryParse(value.Replace('.', ',').TrimEnd('m'), out _);

        /// <inheritdoc />
        public object Convert(string value) 
            => decimal.Parse(value.Replace('.', ',').TrimEnd('m'));
    }
}