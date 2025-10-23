using Backender.Translator;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Backender.Cli
{
    public static class ConsoleHelper
    {
        public static void WriteMessage(string content, ConsoleColor color = ConsoleColor.White)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(content);
            Console.ResetColor();
        }

        public static void WriteError(string title, string details = null)
        {
            WriteMessage($"\nERROR: {title}", ConsoleColor.Red);
            if (!string.IsNullOrEmpty(details))
            {
                WriteMessage(details, ConsoleColor.DarkRed);
            }
        }

        public static void WriteSuccess(string content)
        {
            WriteMessage(content, ConsoleColor.Green);
        }

        public static void WriteWarning(string content)
        {
            WriteMessage($"WARNING: {content}", ConsoleColor.Yellow);
        }

        /// <summary>
        /// Displays compiler messages and returns true if any errors were found.
        /// </summary>
        public static bool DisplayCompilerMessages(List<Message> messages)
        {
            if (!messages.Any()) return false;

            WriteMessage("\n--- Blueprint Validation Results ---");
            var sortedMessages = messages.OrderBy(p => p.MessageType);
            bool hasErrors = false;

            foreach (var message in sortedMessages)
            {
                (string type, ConsoleColor color) = message.MessageType switch
                {
                    MessageType.Error => ("ERROR", ConsoleColor.Red),
                    MessageType.Warning => ("WARNING", ConsoleColor.Yellow),
                    MessageType.Message => ("INFO", ConsoleColor.Cyan),
                    _ => ("MESSAGE", ConsoleColor.White),
                };

                if (message.MessageType == MessageType.Error) hasErrors = true;
                WriteMessage($"[{type}] {message.Code}: {message.Description}", color);
            }

            return hasErrors;
        }
    }
}