using System.Linq;
using System.Reflection;
using CommonUtilities.Console.Attributes;
using CommonUtilities.Console.Entities;

namespace CommonUtilities.Console.Extensions
{
    internal static class MethodInfoExtensions
    {
        internal static Parameter[] GetParams(this MethodInfo method) =>
            method.GetParameters()
                .Select(p => new Parameter
                { 
                    ParameterReference = p, 
                    Attributes         = p.GetCustomAttributes()
                                          .Where(a => a is ParameterAttribute)
                                          .Cast<ParameterAttribute>()
                                          .ToArray(),
                })
                .ToArray();
    }
}