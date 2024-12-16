using System;
using System.Text;

namespace CommonUtilities.Extensions
{
    /// <summary>
    /// Provides string extensions
    /// </summary>
    public static class ConsoleExtensions
    {
        /// <summary>
        /// Request user input via console.
        /// </summary>
        /// <param name="message">Request message</param>
        /// <returns>User input as consoleKeyInfo</returns>
        public static ConsoleKeyInfo AsConsoleKeyInfoRequest(this string message)
        {
            Console.Write(message);
          
            var response = Console.ReadKey();
          
            Console.SetCursorPosition(0, Console.CursorTop);
            
            return response;
        }
        
        /// <summary>
        /// Confirms action via console.
        /// </summary>
        /// <param name="message">Action confirmation message.</param>
        /// <param name="confirmationState">Confirmation key that represents as "yes".</param>
        /// <returns>True - if user pressed <paramref name="confirmationState"/> key.<para/>False - otherwise.</returns>
        public static bool AsConfirmationConsoleRequest(
            this string message, 
            ConsoleKey confirmationState = ConsoleKey.Y) 
        {
            return message.AsConsoleKeyInfoRequest().Key == confirmationState;
        }

        /// <summary>
        /// Requests string response from the user.
        /// </summary>
        /// <param name="message">Request message.</param>
        /// <returns>Users input as raw string</returns>
        public static string AsLineConsoleRequest(this string message)
        {
            Console.WriteLine(message);
            
            var result = Console.ReadLine();
            
            Console.SetCursorPosition(0, Console.CursorTop);
            
            return result;
        }
        
        /// <summary>
        /// Requests multi line value from the console.
        /// </summary>
        /// <param name="message">Request message.</param>
        /// <returns>Multi-line user input.</returns>
        public static string AsMultiLineConsoleRequest(this string message)
        {
            Console.WriteLine(message);
            var result = new StringBuilder();

            var userInput = Console.ReadKey();

            while (userInput.Key != ConsoleKey.Enter && userInput.Modifiers != ConsoleModifiers.Shift)
                result.Append(userInput.KeyChar);

            return result.ToString();
        }

        /// <summary>
        /// Converts user input to int.
        /// </summary>
        /// <param name="message">Request message.</param>
        /// <returns>Integer value of the input or 0 if input could not be retrieved.</returns>
        public static int AsIntConsoleRequest(this string message) 
            => int.TryParse(message.AsLineConsoleRequest(), out var result) ? result : int.MinValue;

        /// <summary>
        /// Gets integer value as index.
        /// </summary>
        /// <param name="message">Request message.</param>
        /// <param name="maxLenght">Max lenght of the indexing</param>
        /// <param name="index">Value between 0 and <paramref name="maxLenght"/>.</param>
        /// <returns>True - if user provides right value between 0 and <paramref name="maxLenght"/>.
        /// <para/>False - otherwise.</returns>
        public static bool AsIndexConsoleRequest(this string message, int maxLenght, out int index)
        {
            index = message.AsIntConsoleRequest() - 1;
            return index < 0 || index >= maxLenght;
        }
    }
}