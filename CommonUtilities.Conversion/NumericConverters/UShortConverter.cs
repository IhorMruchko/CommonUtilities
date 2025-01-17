using System;
using CommonUtilities.Conversion.Base;

namespace CommonUtilities.Conversion.NumericConverters
{
    /// <inheritdoc />
    public struct UShortConverter : IConverter
    {
        /// <inheritdoc />
        public Type TargetType => typeof(ushort);

        /// <inheritdoc />
        public bool CanConvert(string value) 
            => value.EndsWith("us") 
            && ushort.TryParse(value.TrimEnd('s').TrimEnd('u'), out _);

        /// <inheritdoc />
        public object Convert(string value) 
            => ushort.Parse(value.TrimEnd('s').TrimEnd('u'));
    }
}