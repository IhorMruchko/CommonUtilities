using System;
using System.Linq;
using CommonUtilities.Conversion.Base;

namespace CommonUtilities.Conversion.NumericConverters
{
    /// <inheritdoc />
    public struct FloatConverter : IConverter
    {
        /// <inheritdoc />
        public uint Priority => 0;

        /// <inheritdoc />
        public Type TargetType => typeof(float);

        /// <inheritdoc />
        public bool CanConvert(string value)
            => value.EndsWith("f") 
            && (value.Count(i => i == '.') <= 1
            || value.Count(i => i == ',') <= 1)
            && float.TryParse(value.Replace('.', ',').TrimEnd('f'), out _);

        /// <inheritdoc />
        public object Convert(string value) 
            => float.Parse(value.Replace('.', ',').TrimEnd('f'));
    }
}