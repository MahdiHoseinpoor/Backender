using Backender.Translator;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Backender.Translator.Handlers;
namespace Backender.Cli.Commands
{
    public static class ValidateCommand
    {
        public static Task ExecuteAsync(FileInfo blueprintFile)
        {
            try
            {
                ConsoleHelper.WriteMessage($"--- Validating blueprint: {blueprintFile.FullName} ---");
                BlueprintCompiler.Configure();
                var xmldoc = XmlDeserializer.GetXmlDocument(blueprintFile.FullName);
                var blueprint = XmlDeserializer.ConvertXmlToBlueprint(xmldoc);
                blueprint.Compile(blueprintFile.FullName);
                blueprint.Configuration();
                blueprint.Validate();
                bool hasErrors = ConsoleHelper.DisplayCompilerMessages(BlueprintCompiler.Messages);

                if (hasErrors)
                {
                    ConsoleHelper.WriteError("\nValidation failed with one or more errors.");
                }
                else
                {
                    ConsoleHelper.WriteSuccess("\nValidation successful. The blueprint has no errors.");
                }
            }
            catch (Exception ex)
            {
                ConsoleHelper.WriteError("\nAn unexpected error occurred during validation:", ex.ToString());
            }
            finally
            {
                BlueprintCompiler.Messages.Clear();
            }
            return Task.CompletedTask;
        }
    }
}