using System;
using System.Linq;

namespace CommonUtilities.Conversion.Base
{
    /// <summary>
    /// Provides predefined converter for Enum types.
    /// </summary>
    /// <typeparam name="TEnum">Enum type that could be converted.</typeparam>
    public abstract class EnumConverter<TEnum> : IConverter
        where TEnum : Enum
    {
        /// <inheritdoc />
        public Type TargetType => typeof(TEnum);

        /// <inheritdoc />
        public bool CanConvert(string value)
        {
            try
            {
                return Enum.GetNames(TargetType).Select(name => name.ToLower()).Contains(value.ToLower());
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <inheritdoc />
        public object Convert(string value) => Enum.Parse(TargetType, value, true);
    }
}