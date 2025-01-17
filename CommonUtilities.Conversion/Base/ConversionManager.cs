using System;
using System.Collections.Generic;
using System.Linq;
using CommonUtilities.Conversion.Converters;
using CommonUtilities.Conversion.NumericConverters;

namespace CommonUtilities.Conversion.Base
{
    /// <summary>
    /// Provides conversion utilities.
    /// </summary>
    public static class ConversionManager
    {
        private static readonly List<IConverter> Converters;
    
        static ConversionManager()
        {
            Converters = AppDomain.CurrentDomain
                .GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .Where(type => type.GetInterface(nameof(IConverter)) != null 
                            && (type.GetConstructor(Array.Empty<Type>()) != null
                            ||  type.IsValueType))
                .Select(type => (IConverter)Activator.CreateInstance(type))
                .ToList();
        }

        /// <summary>
        /// Tries to convert string representation of value to the <paramref name="target"/> type.
        /// </summary>
        /// <param name="target">Result (targeting) type.</param>
        /// <param name="value">String representation of the value type.</param>
        /// <param name="result">Value that was parsed. Null - if value could not be parsed.</param>
        /// <returns>
        /// True - if <paramref name="value"/> could be converted to <paramref name="target"/> type. <para/>
        /// False - otherwise.
        /// </returns>
        public static bool TryConvert(Type target, string value, out object result)
        {
            result = null;
            var converter = Converters.FirstOrDefault(c => target == c.TargetType && c.CanConvert(value));
        
            if (converter is null) return false;

            result = converter.Convert(value);
            return true;
        }
    }
}

