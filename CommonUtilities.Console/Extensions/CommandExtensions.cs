using System.Linq;
using CommonUtilities.Console.Entities;

namespace CommonUtilities.Console.Extensions
{
    public static class CommandExtensions
    {
        internal static bool CanExecute(this Command command, ArgumentBag arguments, int currentIndex = 0)
            => arguments[currentIndex].hasValue
               && arguments[currentIndex].value.Equals(command.Attribute.Name)
               && (command.SubCommands.Any(c => c.CanExecute(arguments, currentIndex + 1))
               || command.Overloads.Any(over => over.CanExecute(arguments, currentIndex + 1)));


        internal static string Execute(this Command command, ArgumentBag arguments, int currentIndex = 1)
        {
            while (true)
            {
                var executableOverload = command.Overloads.FirstOrDefault(o  => o.CanExecute(arguments, currentIndex));
                var executableCommand = command.SubCommands.FirstOrDefault(c => c.CanExecute(arguments, currentIndex));

                if (executableCommand == null)
                {
                    return executableOverload != null
                        ? executableOverload.Execute(arguments, currentIndex)
                        : "Something went wrong :(";
                }
                
                ++currentIndex;
                command = executableCommand;
            }
        }
    }
}