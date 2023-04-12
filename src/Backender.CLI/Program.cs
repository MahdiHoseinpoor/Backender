using Microsoft.CodeAnalysis.CSharp;
using Backender.CodeEditor.CSharp;
using Backender.CodeGenerator;
using System.ComponentModel;
using Backender.Translator;
using System.Text.Json;
using System;

namespace Backender.ConsoleApp
{
	internal class Program
	{
		static async Task Main(string[] args)
		{
			string FileName = "";
			Engine app = new();

			WriteMessage("===================== Welcome to Backender! =====================\n\n", ConsoleColor.Blue);
			while (true)
			{
#if DEBUG
				FileName = "G:\\shoping site.yaml";
#else

				FileName = ReadLine("Enter your Configuration File Location",ConsoleColor.White)!.Trim();
#endif


				if (File.Exists(FileName))
				{
					WriteMessage($"--- {FileName} -> The Config File Processing has been start");
					var config = await ConfigBuilder(FileName);
					var messages = ConfigChecker.Run(config);
					if (messages.Any())
					{
						WriteMessage($"	ErrorCode | Description\n", ConsoleColor.White);
						foreach (var message in messages)
						{
							ConsoleColor consoleColor = ConsoleColor.White;
							switch (message.MessageType)
							{
								case MessageType.Error:
									consoleColor = ConsoleColor.Red;
									break;
								case MessageType.Warning:
									consoleColor = ConsoleColor.Yellow;
									break;
								case MessageType.Message:
									consoleColor = ConsoleColor.Cyan;
									break;
								default:
									break;
							}
							WriteMessage($"	{message.Code} | {message.Description}\n", consoleColor);
						}
						if (messages.Any(p => p.MessageType == MessageType.Error))
						{
#if DEBUG
							Console.ReadKey();
							Environment.Exit(0);
#else
							continue;
#endif
						}

					}

					WriteMessage($"	Backender Engine Start to Generate!");
					await app.Run(config);
					Console.ForegroundColor = ConsoleColor.Green;
					Console.Write($"	Your Project Has Been Created in ");
					Console.ForegroundColor = ConsoleColor.Yellow;
					Console.Write(app.SavePath + "\n");
					Console.ForegroundColor = ConsoleColor.White;
					Console.ReadKey();
					Environment.Exit(0);
				}
				else
				{
					WriteMessage($"	I can not found Config file in '{FileName}'", ConsoleColor.Red);
				}

			}

			//app.Run("G:\\Shopping Site.json", "G:\\NewShoppingSite");

		}
		public static void WriteMessage(string content, ConsoleColor consoleColor = ConsoleColor.White)
		{
			Console.ForegroundColor = consoleColor;
			Console.WriteLine($" {content}");
		}
		public static string ReadLine(string message, ConsoleColor messageColor)
		{
			Console.ForegroundColor = messageColor;
			Console.Write($" {message}");
			Console.ForegroundColor = ConsoleColor.White;
			Console.Write($": ");
			return Console.ReadLine();
		}
		public static string ReadLine()
		{
			Console.ForegroundColor = ConsoleColor.Cyan;
			Console.Write($" {Environment.UserName}");
			Console.ForegroundColor = ConsoleColor.White;
			Console.Write($":");
			Console.ForegroundColor = ConsoleColor.Blue;
			Console.Write($"~");
			Console.ForegroundColor = ConsoleColor.White;
			Console.Write($"$ ");
			return Console.ReadLine();
		}
		private async static Task<Config> ConfigBuilder(string FileName)
		{
			var config = new Config();
			var configFile = await File.ReadAllTextAsync(FileName);
			if (Path.GetExtension(FileName).ToLower() == ".json")
			{
				config = JsonSerializer.Deserialize<Config>(configFile);
			}
			if (Path.GetExtension(FileName).ToLower() == ".yaml")
			{
				config = YamlSerializer.Deserialize<Config>(configFile);
			}
			else if (Path.GetExtension(FileName).ToLower() == ".palino")
			{
				config = PalinoSerializer.Deserialize(configFile);
			}
			return config;
		}

	}
}