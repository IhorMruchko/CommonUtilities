using System.Linq;
using CommonUtilities.Console.Entities;
using CommonUtilities.Conversion;

namespace CommonUtilities.Console.Extensions
{
    public static class OverloadExtensions
    {
        internal static bool CanExecute(this Overload overload, string[] arguments)
            => overload.Parameters.Length == arguments.Length 
            && overload.Parameters.Select((parameter, index) => parameter.CanExecute(arguments[index])).All(t => t);

        internal static string Execute(this Overload overload, string[] arguments)
            => (string) overload.OverloadReference.Invoke(
                null,
                overload.Parameters.Select((parameter, index) => ConversionManager.TryConvert(
                    parameter.ParameterReference.ParameterType,
                    arguments[index],
                    out var result
                ) ? parameter.Convert(result) : null)
                .ToArray()
            );
    }
}