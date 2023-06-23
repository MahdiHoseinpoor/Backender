using Backender.Translator;
using Backender.Translator.Handlers;
using Backender.Generator;
using Backender.Translator.Handlers;
using Backender.Translator.Models;
using System.Text.Json;
using File = System.IO.File;
using File_ = Backender.Translator.Models.File;
using System.Text;
using System.Xml;

namespace Backender.Cli
{
    internal class Program
    {
         const string Banner =
@"
       ██████╗░░█████╗░░█████╗░██╗░░██╗███████╗███╗░░██╗██████╗░███████╗██████╗░
       ██╔══██╗██╔══██╗██╔══██╗██║░██╔╝██╔════╝████╗░██║██╔══██╗██╔════╝██╔══██╗
       ██████╦╝███████║██║░░╚═╝█████═╝░█████╗░░██╔██╗██║██║░░██║█████╗░░██████╔╝
       ██╔══██╗██╔══██║██║░░██╗██╔═██╗░██╔══╝░░██║╚████║██║░░██║██╔══╝░░██╔══██╗
       ██████╦╝██║░░██║╚█████╔╝██║░╚██╗███████╗██║░╚███║██████╔╝███████╗██║░░██║
       ╚═════╝░╚═╝░░╚═╝░╚════╝░╚═╝░░╚═╝╚══════╝╚═╝░░╚══╝╚═════╝░╚══════╝╚═╝░░╚═╝";
        const string Information =
@"
 Backender.Cli v2.0.0
 Created by: Mahdi Hoseinpoor
";

        static async Task Main(string[] args)
        {
            string FileName = string.Empty;
            if (args.Count() > 1)
            {
                FileName = args[0];
            }
            BlueprintCompiler.Configure();
            Engine engine = new Engine();
            ConsoleColor DefaultBackgroundColor = Console.BackgroundColor;
            WriteMessage(Banner, ConsoleColor.Blue);
            WriteMessage(Information, ConsoleColor.Blue);
            while (true)
            {
                try
                {
                    if (string.IsNullOrEmpty(FileName))
                    {
                        FileName = ReadLine("Enter The Location of Blueprint File", ConsoleColor.White)!.Trim();
                    }
                    var startTime = DateTime.Now.Ticks;
                    if (File.Exists(FileName))
                    {
                        var xmldoc = XmlDeserializer.GetXmlDocument(FileName);
                        WriteMessage($"--- {FileName} -> The Blueprint File Processing has been start");

                        var Blueprint = XmlDeserializer.ConvertXmlToBlueprint(xmldoc);
                        Blueprint.Compile(FileName);
                        Blueprint = Blueprint.Configuration();
                        var Tables = TableHandler.CreateTables(Blueprint);
                        var Files = new List<File_>();
                        Blueprint.Validate();
                        if (BlueprintCompiler.Messages.Any())
                        {
                            Console.BackgroundColor = ConsoleColor.Black;
                            WriteMessage($"   ErrorCode | Description ", ConsoleColor.White);
                            foreach (var message in BlueprintCompiler.Messages.OrderBy(p => p.MessageType))
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
                                WriteMessage($"	{message.Code} | {message.Description}", consoleColor);

                            }
                            Console.BackgroundColor = DefaultBackgroundColor;
                            if (BlueprintCompiler.Messages.Any(p => p.MessageType == MessageType.Error))
                            {
#if DEBUG
                            Console.ReadKey();
                            Environment.Exit(0);
#else
                                continue;
#endif
                            }
                        }

                        WriteMessage($"\n	Backender Engine Start to Generate!");
                        await engine.RunAsync(Blueprint);
                        var FinishTime = DateTime.Now.Ticks;
                        Console.ForegroundColor = ConsoleColor.Green;
                        var CreatedTime = FinishTime - startTime;
                        if ((CreatedTime / TimeSpan.TicksPerMillisecond) < 2000)
                        {
                            Console.Write($"  Your Project Has Been Created in {CreatedTime / TimeSpan.TicksPerMillisecond} Milliseconds!");

                        }
                        else
                        {
                            Console.Write($"  Your Project Has Been Created in {CreatedTime / TimeSpan.TicksPerSecond} Seconds!");
                        }
                        Console.ForegroundColor = ConsoleColor.White;
                        WriteMessage($"\nPress Any key To Close...");
                        Console.ReadKey();
                        Environment.Exit(0);


                    }
                    else
                    {
                        Console.BackgroundColor = ConsoleColor.Black;
                        WriteMessage($"	I can not found Blueprint file in '{FileName}'", ConsoleColor.Red);
                        Console.BackgroundColor = DefaultBackgroundColor;
                    }
                }
                catch (InvalidDataException e)
                {
                    WriteMessage($"\t{e.Message}", ConsoleColor.Red);
                }
                catch (XmlException)
                {
                    WriteMessage($"The Blueprint file has syntax error.", ConsoleColor.Red);
                }
                catch (Exception e)
                {
                    WriteMessage($"There is an Error: {e.Message}", ConsoleColor.Red);
                }
                finally
                {
                    FileName = string.Empty;
                }


            }
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
       
    }
}