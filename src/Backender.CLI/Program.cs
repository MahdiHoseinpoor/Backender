using Microsoft.CodeAnalysis.CSharp;
using Backender.CodeEditor.CSharp;
using Backender.CodeGenerator;
using System.ComponentModel;

namespace Backender.ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Engine app = new();

			
#if DEBUG
				app.FileName = "G:\\shoping site.yaml";
#else
			Console.ForegroundColor = ConsoleColor.DarkGreen;
			Console.WriteLine("-------------------- Welcome to Backender! --------------------");
			Console.ForegroundColor = ConsoleColor.White;
			Console.WriteLine("Enter your Configuration File Location:");
			app.FileName = Console.ReadLine()!.Trim();
#endif

			app.Run();
			//app.Run("G:\\Shopping Site.json", "G:\\NewShoppingSite");

		}
	
    }
}