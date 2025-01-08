using System.Linq;
using CommonUtilities.Console.Entities;
using CommonUtilities.Conversion;

namespace CommonUtilities.Console.Extensions
{
    public static class ParameterExtensions
    {
        internal static bool CanExecute(this Parameter parameter, string argument)
            => parameter.Attributes.All(attribute => attribute.Validate(parameter.ParameterReference, argument)) 
            && ConversionManager.TryConvert(parameter.ParameterReference.ParameterType, argument, out _);

        internal static object Convert(this Parameter parameter, object value)
            => parameter.Attributes.Aggregate(value, (current, attribute) => attribute.Convert(current));
    }
}