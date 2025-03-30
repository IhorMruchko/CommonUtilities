using System;
using System.Linq;
using CommonUtilities.Console.Entities;
using CommonUtilities.Conversion.Base;

namespace CommonUtilities.Console.Extensions
{
    public static class OverloadExtensions
    {
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


        internal static string Execute(this Overload overload, ArgumentBag arguments, int currentIndex, int shift = 0)
            => overload.Execute(overload.Parameters
                .Select(p => p.IsOptional() 
                    ? arguments[p.GetName()].hasValue 
                        ? arguments[p.GetName()].value
                        : p.ParameterReference.DefaultValue?.ToString() ?? string.Empty 
                    : arguments[currentIndex + shift++].value)
                .ToArray());

        internal static bool CanExecute(
            this Overload overload,
            ArgumentBag arguments,
            int currentArgumentIndex)
            => overload.Parameters.Count(t => t.IsPositional()) + currentArgumentIndex == arguments.Length
               && overload.Parameters.Where(parameter => parameter.IsPositional())
                   .Select((parameter, index) => arguments[currentArgumentIndex + index].hasValue
                                                 && parameter.CanExecute(arguments[currentArgumentIndex + index].value))
                   .All(t => t)
               && arguments.Optionals.All(optional => overload.Parameters.Select(p => p.GetName()).Contains(optional))
               && overload.Parameters.Where(p => p.IsOptional()).Select(p => !arguments[p.GetName()].hasValue 
                                                                          || arguments[p.GetName()].hasValue
                                                                          && p.CanExecute(arguments[p.GetName()].value))
                   .All(t => t);
    }
}