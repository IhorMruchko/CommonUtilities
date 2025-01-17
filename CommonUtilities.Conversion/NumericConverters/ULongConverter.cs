using System;
using CommonUtilities.Conversion.Base;

namespace CommonUtilities.Conversion.NumericConverters
{
    /// <inheritdoc />
    public struct ULongConverter : IConverter
    {
        /// <inheritdoc />
        public Type TargetType => typeof(ulong);

        
        /// <inheritdoc />
        public bool CanConvert(string value) 
            => value.EndsWith("ul")
            && ulong.TryParse(value.TrimEnd('l').TrimEnd('u'), out _);

        /// <inheritdoc />
        public object Convert(string value) 
            => ulong.Parse(value.TrimEnd('l').TrimEnd('u'));
    }
}