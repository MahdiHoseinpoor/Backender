using Backender.Generator.Templates;
using Backender.Translator.Templates;
using System.Threading.Tasks;

namespace Backender.Generator.Steps
{
    public class EntityGenerationStep : IGenerationStep
    {
        public async Task ExecuteAsync(GenerationContext context)
        {
            var coreProject = context.GetProjectByRole("Core");
            foreach (var table in context.Tables)
            {
                ITemplateBase entityTemplate = new EntityTemplate(context.Tables, table, coreProject);
                var file = await entityTemplate.OnCreateAsync();

                coreProject.Files.Add(file);
                context.GeneratedFiles.Add(file);
            }
        }
    }
}