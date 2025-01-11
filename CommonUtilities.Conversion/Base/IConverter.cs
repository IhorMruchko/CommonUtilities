using System;

namespace CommonUtilities.Conversion.Base
{
    /// <summary>
    /// Interface defines converters from string to object type.
    /// </summary>
    public interface IConverter
    {
        /// <summary>
        /// Priority defines place in the handling chain.
        /// </summary>
        uint Priority { get; }
        
        /// <summary>
        /// Target conversion type. 
        /// </summary>
        Type TargetType { get; }
    
        /// <summary>
        /// Defines if conversion possible for <paramref name="value"/>.
        /// </summary>
        /// <param name="value">String representation of the possible value.</param>
        /// <returns>True - if <paramref name="value"/> could be converter to the TargetType. <para/>
        /// False - otherwise.
        /// </returns>
        bool CanConvert(string value);
        
        /// <summary>
        /// Converts <paramref name="value"/> to the target conversion type.
        /// </summary>
        /// <param name="value">String representation of the possible value type.</param>
        /// <returns>True - if <paramref name="value"/> could be converter to the TargetType. <para/>
        /// False - otherwise.
        /// </returns>
        object Convert(string value);
    }   
}