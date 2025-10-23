using Backender.Generator.Templates;
using Backender.Translator.Handlers;
using Backender.Translator.Templates;
using System.Threading.Tasks;

namespace Backender.Generator.Steps
{
    public class DtoGenerationStep : IGenerationStep
    {
        public async Task ExecuteAsync(GenerationContext context)
        {
            var coreProject = context.GetProjectByRole("Core");
            foreach (var table in context.Tables)
            {
                if (!table.Options.HasOption("MiddleEntity"))
                {
                    ITemplateBase dtoTemplate = new DtoTemplate(context.Tables, table, coreProject);
                    var file = await dtoTemplate.OnCreateAsync();

                    coreProject.Files.Add(file);
                    context.GeneratedFiles.Add(file);
                }
            }
        }
    }
}