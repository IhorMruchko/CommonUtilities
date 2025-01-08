using System.Linq;
using CommonUtilities.Console.Entities;

namespace CommonUtilities.Console.Extensions
{
    public static class CommandExtensions
    {
        internal static bool CanExecute(this Command command, string[] arguments) 
            => arguments.Length != 0 
            && command.Attribute.Name.Equals(arguments[0]) 
            && (command.SubCommands.Any(subCommand => subCommand.CanExecute(arguments.Skip(1).ToArray())) 
            || command.Overloads.Any(overload      => overload.CanExecute(arguments.Skip(1).ToArray())));

        internal static string Execute(this Command command, string[] arguments)
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
    }
}