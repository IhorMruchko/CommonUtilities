using System;
using System.Linq;
using CommonUtilities.Console.Entities;
using CommonUtilities.Console.Extensions;

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
        
        /// <summary>
        /// Executes right handler from the user input.
        /// </summary>
        /// <param name="arguments">User input from the command line.</param>
        /// <returns>Response from the handler.</returns>
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
    }
}