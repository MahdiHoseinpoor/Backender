using Backender.Generator;
using Backender.Translator;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Backender.Translator.Handlers;
namespace Backender.Cli.Commands
{
    public static class GenerateCommand
    {
        public static async Task ExecuteAsync(FileInfo blueprintFile)
        {
            try
            {
                var stopwatch = Stopwatch.StartNew();
                ConsoleHelper.WriteMessage($"--- Processing blueprint: {blueprintFile.FullName} ---");

                BlueprintCompiler.Configure();
                var blueprint = LoadAndCompileBlueprint(blueprintFile);

                blueprint.Validate();
                if (ConsoleHelper.DisplayCompilerMessages(BlueprintCompiler.Messages))
                {
                    ConsoleHelper.WriteError("\nGeneration halted due to critical errors.");
                    return;
                }

                ConsoleHelper.WriteMessage("\n--- Starting code generation engine... ---");
                var engine = new EngineBuilder().WithDefaultPipeline().Build();
                await engine.RunAsync(blueprint);
                stopwatch.Stop();

                ConsoleHelper.WriteSuccess($"\nSuccess! Your project has been created in {stopwatch.Elapsed.TotalSeconds:F2} seconds!");
                ConsoleHelper.WriteMessage("Generation complete. You can close this window.", ConsoleColor.Gray);
            }
            catch (Exception ex)
            {
                ConsoleHelper.WriteError("\nAn unexpected error occurred during generation:", ex.ToString());
            }
            finally
            {
                BlueprintCompiler.Messages.Clear();
            }
        }

        private static Blueprint LoadAndCompileBlueprint(FileInfo blueprintFile)
        {
            var xmldoc = XmlDeserializer.GetXmlDocument(blueprintFile.FullName);
            var blueprint = XmlDeserializer.ConvertXmlToBlueprint(xmldoc);
            blueprint.Compile(blueprintFile.FullName);
            return blueprint.Configuration();
        }
    }
}