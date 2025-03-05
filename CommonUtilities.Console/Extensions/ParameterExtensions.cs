using System.Linq;
using CommonUtilities.Console.Attributes;
using CommonUtilities.Console.Entities;
using CommonUtilities.Conversion.Base;

namespace CommonUtilities.Console.Extensions
{
    public static class ParameterExtensions
    {
        internal static bool CanExecute(this Parameter parameter, string argument)
            => parameter.Attributes.All(attribute => attribute.Validate(parameter.ParameterReference, argument)) 
            && ConversionManager.TryConvert(parameter.ParameterReference.ParameterType, argument, out _);

        internal static object Convert(this Parameter parameter, object value)
            => parameter.Attributes.Aggregate(value, (current, attribute) => attribute.Convert(current));

        internal static bool IsPositional(this Parameter parameter) => !parameter.IsOptional();

        internal static bool IsOptional(this Parameter parameter)
            => parameter.ParameterReference.IsOptional;

        internal static string GetName(this Parameter parameter)
        {
            var optionalAttribute = (OptionalAttribute)parameter.Attributes
                .FirstOrDefault(attribute => attribute is OptionalAttribute);
            return optionalAttribute == null
                ? parameter.ParameterReference.Name
                : optionalAttribute.Name ?? optionalAttribute.Symbol.ToString();
        }
    }
}