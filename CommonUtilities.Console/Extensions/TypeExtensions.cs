using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CommonUtilities.Console.Attributes;
using CommonUtilities.Console.Entities;

namespace CommonUtilities.Console.Extensions
{
    internal static class TypeExtensions
    {
        internal static Command[] ToCommands(this IEnumerable<Type> types, int inheritanceLevel = 0) =>
            types.Where(t  => t.GetCustomAttribute<CommandAttribute>() != null 
                           && (inheritanceLevel > 0 || !t.IsNested))
                 .Select(t => t.ToCommand())
                 .ToArray();
        
        internal static Command ToCommand(this Type type, int inheritanceLevel = 0) =>
            new Command
            {
                ReferenceType = type,
                Attribute     = type.GetCustomAttribute<CommandAttribute>(),
                SubCommands   = type.GetNestedTypes(BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public).ToCommands(inheritanceLevel + 1),
                Overloads     = type.GetOverloads()
            };
        
        internal static Overload[] GetOverloads(this Type type) =>
            type.GetMethods(BindingFlags.Public | BindingFlags.Static)
                .Where(t  => t.ReturnType == typeof(string) 
                             && (t.GetCustomAttribute<OverloadAttribute>() != null 
                             || t.Name.Equals("Execute")
                       ))
                .Select(t => new Overload
                {
                    OverloadReference = t,
                    Parameters        = t.GetParams()
                })
                .ToArray();
    }
}