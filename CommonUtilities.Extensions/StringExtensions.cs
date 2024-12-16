using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;

namespace CommonUtilities.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Defines if string is in <paramref name="values"/> list.
        /// </summary>
        /// <param name="text">Value to search for in <paramref name="values"/>.</param>
        /// <param name="values">List of possible values.</param>
        /// <returns>True - if <paramref name="text"/> is in <paramref name="values"/>; <para/>
        /// False - otherwise.</returns>
        public static bool In(this string text, params string[] values) 
            => values.Any(value => text.Equals(value, StringComparison.InvariantCultureIgnoreCase));
    
        /// <summary>
        /// Saves string text to clipboard.
        /// </summary>
        /// <param name="text">Text to save to the clipboard.</param>
        public static void SaveToClipboard(this string text)
        {
            var processStartInfo = new ProcessStartInfo
            {
                FileName = string.Empty,
                Arguments = string.Empty,
                RedirectStandardInput = false,
                RedirectStandardOutput = false,
                RedirectStandardError = false,
                UseShellExecute = true,
                CreateNoWindow = true
            };
            
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                processStartInfo.FileName = "cmd.exe";
                processStartInfo.Arguments = $"/c echo {text} | clip";
            }

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                processStartInfo.FileName = "bash";
                processStartInfo.Arguments = $"-c \"echo {text} | xclip -selection clipboard\"";
            }

            if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                processStartInfo.FileName = "bash";
                processStartInfo.Arguments = $"-c \"echo '{text.Replace("'", "'\\''")}' | pbcopy\""; 
            }
            
            if (processStartInfo.FileName == string.Empty || processStartInfo.Arguments == string.Empty) return;
            
            var process = new Process()
            {
                StartInfo = processStartInfo
            };
        
            process.Start();
            process.WaitForExit();
        }

        /// <summary>
        /// Opens string text in browser.
        /// </summary>
        /// <param name="text">Url value.</param>
        public static void OpenInBrowser(this string text)
        {
            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = text,
                    UseShellExecute = true
                }
            };
            
            process.Start();
            process.WaitForExit();
        }
    }
}