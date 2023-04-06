using Microsoft.CodeAnalysis.CSharp;
using Backender.CodeEditor.CSharp;
using Backender.CodeGenerator;
using System.ComponentModel;

namespace Backender.ConsoleApp
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
		
			Engine app = new();

			WriteMessage("===================== Welcome to Backender! =====================", ConsoleColor.Blue);
			while (true)
			{
#if DEBUG
			app.FileName = "G:\\shoping site.yaml";
#else

				WriteMessage("--- Enter your Configuration File Location:");
				Console.Write(">>> ");
				app.FileName = Console.ReadLine()!.Trim();
#endif
				if (File.Exists(app.FileName))
				{
					WriteMessage($"--- {app.FileName} -> The Config File Processing has been start");
					WriteMessage($"--- Backender Engine Start to Generate!");
					await app.Run();
					Console.ForegroundColor = ConsoleColor.Green;
					Console.Write($"--- Your Project Has Been Created in ");
					Console.ForegroundColor = ConsoleColor.Yellow;
					Console.Write(app.SavePath + "\n");
					Console.ForegroundColor = ConsoleColor.White;
					Console.ReadKey();
					Environment.Exit(0);
				}
				else
				{
					WriteMessage($"---  I can not found Config file in '{app.FileName}'", ConsoleColor.Red);
				}
				
			}

			//app.Run("G:\\Shopping Site.json", "G:\\NewShoppingSite");

		}
		public static void WriteMessage(string content, ConsoleColor consoleColor = ConsoleColor.White)
		{
			Console.ForegroundColor = consoleColor;
			Console.WriteLine($"{content}\n");
		}

	}
}