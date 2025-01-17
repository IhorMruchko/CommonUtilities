using System;
using CommonUtilities.Conversion.Base;

namespace CommonUtilities.Conversion.NumericConverters
{
    /// <inheritdoc />
    public struct UIntConverter : IConverter
    {
        /// <inheritdoc />
        public Type TargetType => typeof(uint);

        /// <inheritdoc />
        public bool CanConvert(string value) 
            => value.EndsWith("ui") 
            && uint.TryParse(value.TrimEnd('i').TrimEnd('u'), out _);

        /// <inheritdoc />
        public object Convert(string value)
            => uint.Parse(value.TrimEnd('i').TrimEnd('u'));
    }
}