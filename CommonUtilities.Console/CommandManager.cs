using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CommonUtilities.Console.Attributes;
using CommonUtilities.Console.Entities;
using CommonUtilities.Conversion;

namespace CommonUtilities.Console
{
    /// <summary>
    /// Entry point for console execution.
    /// </summary>
    public static class CommandManager
    {
        private static readonly Command[] Commands;

        static CommandManager()
        {
            Commands = AppDomain.CurrentDomain
                                .GetAssemblies()
                                .SelectMany(t => t.GetTypes())
                                .ToCommands();
        }
        
        public static string Execute(string[] arguments)
        {
            foreach (var command in Commands)
            {
                if (command.CanExecute(arguments))
                {
                    return command.Execute(arguments);
                }
            }

            return "No command fit to arguments";
        }
        
        private static Command[] ToCommands(this IEnumerable<Type> types, int inheritanceLevel = 0) =>
            types.Where(t  => t.GetCustomAttribute<CommandAttribute>() != null 
                           && (inheritanceLevel > 0 || !t.IsNested))
                 .Select(t => t.ToCommand())
                 .ToArray();

        private static Command ToCommand(this Type type, int inheritanceLevel = 0) =>
            new Command
            {
                ReferenceType = type,
                Attribute     = type.GetCustomAttribute<CommandAttribute>(),
                SubCommands   = type.GetNestedTypes(BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public).ToCommands(inheritanceLevel + 1),
                Overloads     = type.GetOverloads()
            };

        private static Overload[] GetOverloads(this Type type) =>
            type.GetMethods(BindingFlags.Public | BindingFlags.Static)
                .Where(t  => t.ReturnType == typeof(string) 
                          && (t.GetCustomAttribute<OverloadAttribute>() != null 
                          || t.Name.Equals("Execute")))
                .Select(t => new Overload
                {
                    OverloadReference = t,
                    Parameters        = t.GetParams()
                })
                .ToArray();

        private static Parameter[] GetParams(this MethodInfo method) =>
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
        
        private static bool CanExecute(this Command command, string[] arguments) 
            => command.Attribute.Name.Equals(arguments[0]) 
            && (command.SubCommands.Any(subCommand => subCommand.CanExecute(arguments.Skip(1).ToArray())) 
            || command.Overloads.Any(overload      => overload.CanExecute(arguments.Skip(1).ToArray())));

        private static string Execute(this Command command, string[] arguments)
        {
            while (true)
            {
                var currentArguments = arguments.Skip(1).ToArray();
                var executableOverload = command.Overloads.FirstOrDefault(o => o.CanExecute(currentArguments));
                var executableCommand = command.SubCommands.FirstOrDefault(c => c.CanExecute(currentArguments));

                if (executableCommand == null)
                    return executableOverload != null
                        ? executableOverload.Execute(currentArguments)
                        : "Something went wrong :(";
                
                command = executableCommand;
                arguments = currentArguments;
            }
        }

        private static bool CanExecute(this Overload overload, string[] arguments)
            => overload.Parameters.Length == arguments.Length 
            && overload.Parameters.Select((parameter, index) => parameter.CanExecute(arguments[index])).All(t => t);

        private static string Execute(this Overload overload, string[] arguments)
            => (string) overload.OverloadReference.Invoke(
                null,
                overload.Parameters.Select((parameter, index) => ConversionManager.TryConvert(
                    parameter.ParameterReference.ParameterType,
                    arguments[index],
                    out var result
                ) ? result : null).ToArray()
            );


        private static bool CanExecute(this Parameter parameter, string argument)
            => parameter.Attributes.All(attribute => attribute.Validate(parameter.ParameterReference, argument)) 
            && ConversionManager.TryConvert(parameter.ParameterReference.ParameterType, argument, out _);
        
    }
}