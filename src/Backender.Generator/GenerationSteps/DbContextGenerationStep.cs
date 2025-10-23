using Backender.Generator.Templates;
using Backender.Translator.Templates;
using System.Threading.Tasks;

namespace Backender.Generator.Steps
{
    public class DbContextGenerationStep : IGenerationStep
    {
        public async Task ExecuteAsync(GenerationContext context)
        {
            var coreProject = context.GetProjectByRole("Core");
            var dataProject = context.GetProjectByRole("Data");

            ITemplateBase dbContextTemplate = new DbContextTemplate(context.Tables, dataProject, coreProject);
            var file = await dbContextTemplate.OnCreateAsync();

            dataProject.Files.Add(file);
            context.GeneratedFiles.Add(file);
        }
    }
}