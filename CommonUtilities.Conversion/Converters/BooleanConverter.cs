using System;
using System.Linq;
using CommonUtilities.Conversion.Base;

namespace CommonUtilities.Conversion.Converters
{
    /// <inheritdoc />
    public struct BooleanConverter : IConverter
    {
        /// <summary>
        /// All possible boolean literals.
        /// </summary>
        private static readonly string[] AllLiterals = { "true", "false", "yes", "no", "0", "1" };
        
        /// <summary>
        /// Literals that represents 'true' value. 
        /// </summary>
        private static readonly string[] TrueLiterals = { "true", "yes", "1" };

        /// <inheritdoc />
        public Type TargetType => typeof(bool);

        /// <inheritdoc />
        public bool CanConvert(string value)
            => AllLiterals.Contains(value.ToLower());

        /// <inheritdoc />
        public object Convert(string value) 
            => TrueLiterals.Contains(value.ToLower());
    }
}