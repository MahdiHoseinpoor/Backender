using Backender.Cli.Commands;
using System;
using System.CommandLine;
using System.CommandLine.Builder;
using System.CommandLine.Parsing;
using System.IO;
using System.Threading.Tasks;

namespace Backender.Cli
{
    internal class Program
    {
        const string Banner = @"
       ██████╗░░█████╗░░█████╗░██╗░░██╗███████╗███╗░░██╗██████╗░███████╗██████╗░
       ██╔══██╗██╔══██╗██╔══██╗██║░██╔╝██╔════╝████╗░██║██╔══██╗██╔════╝██╔══██╗
       ██████╦╝███████║██║░░╚═╝█████═╝░█████╗░░██╔██╗██║██║░░██║█████╗░░██████╔╝
       ██╔══██╗██╔══██║██║░░██╗██╔═██╗░██╔══╝░░██║╚████║██║░░██║██╔══╝░░██╔══██╗
       ██████╦╝██║░░██║╚█████╔╝██║░╚██╗███████╗██║░╚███║██████╔╝███████╗██║░░██║
       ╚═════╝░╚═╝░░╚═╝░╚════╝░╚═╝░░╚═╝╚══════╝╚═╝░░╚══╝╚═════╝░╚══════╝╚═╝░░╚═╝";

        static async Task<int> Main(string[] args)
        {
            var rootCommand = new RootCommand("Backender CLI: A tool to generate backend projects from an XML blueprint.")
            {
                CreateGenerateCommand(),
                CreateValidateCommand(),
                CreateInitCommand()
            };
            rootCommand.Name = "Backender";

            if (args.Length == 0)
            {
                ConsoleHelper.WriteMessage(Banner, ConsoleColor.Blue);
                ConsoleHelper.WriteMessage("\nBackender.Cli v3.0.0-preview.2\nCreated by: Mahdi Hoseinpoor\n", ConsoleColor.Cyan);
                ConsoleHelper.WriteMessage("Use 'Backender --help' to see available commands.");
                return 0;
            }

            var commandLineBuilder = new CommandLineBuilder(rootCommand);
            commandLineBuilder.UseDefaults();
            var parser = commandLineBuilder.Build();

            return await parser.InvokeAsync(args);
        }

        private static Command CreateGenerateCommand()
        {
            var blueprintArgument = new Argument<FileInfo>(
                name: "blueprint",
                description: "The path to the XML blueprint file.")
            {
                Arity = ArgumentArity.ExactlyOne
            }.ExistingOnly(); // Built-in validation for file existence

            var command = new Command("generate", "Generates the full project structure from a blueprint file.")
            {
                blueprintArgument
            };

            command.SetHandler(GenerateCommand.ExecuteAsync, blueprintArgument);
            return command;
        }

        private static Command CreateValidateCommand()
        {
            var blueprintArgument = new Argument<FileInfo>(
                name: "blueprint",
                description: "The blueprint file to validate.")
            {
                Arity = ArgumentArity.ExactlyOne
            }.ExistingOnly();

            var command = new Command("validate", "Validates the syntax and integrity of a blueprint file.")
            {
                blueprintArgument
            };

            command.SetHandler(ValidateCommand.ExecuteAsync, blueprintArgument);
            return command;
        }

        private static Command CreateInitCommand()
        {
            var fileNameOption = new Option<string>(
                aliases: new[] { "--file", "-f" },
                description: "The name of the blueprint file to create.",
                getDefaultValue: () => "blueprint.xml");

            var command = new Command("init", "Creates a new, empty 'blueprint.xml' file in the current directory.")
            {
                fileNameOption
            };

            command.SetHandler(InitCommand.ExecuteAsync, fileNameOption);
            return command;
        }
    }
}