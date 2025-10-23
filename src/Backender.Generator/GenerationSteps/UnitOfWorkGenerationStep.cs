using Backender.Generator.Templates;
using Backender.Translator.Templates;
using System.Threading.Tasks;

namespace Backender.Generator.Steps
{
    public class UnitOfWorkGenerationStep : IGenerationStep
    {
        public async Task ExecuteAsync(GenerationContext context)
        {
            var coreProject = context.GetProjectByRole("Core");
            var dataProject = context.GetProjectByRole("Data");
            var servicesProject = context.GetProjectByRole("Services");

            ITemplateBase uowTemplate = new UnitOfWorkTemplate(context.Tables, servicesProject, coreProject, dataProject);
            var file = await uowTemplate.OnCreateAsync();

            servicesProject.Files.Add(file);
            context.GeneratedFiles.Add(file);
        }
    }
}