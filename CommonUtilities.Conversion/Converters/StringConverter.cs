using System;
using CommonUtilities.Conversion.Base;

namespace CommonUtilities.Conversion.Converters
{
    /// <inheritdoc />
    public struct StringConverter : IConverter
    {
        /// <inheritdoc />
        public Type TargetType => typeof(string);

        /// <inheritdoc />
        public bool CanConvert(string value)
            => true;

        /// <inheritdoc />
        public object Convert(string value) 
            => value;
    }
}

