using Backender.Generator.Templates;
using Backender.Translator.Templates;
using System.Linq;
using System.Threading.Tasks;

namespace Backender.Generator.Steps
{
    public class DtoFactoryGenerationStep : IGenerationStep
    {
        public async Task ExecuteAsync(GenerationContext context)
        {
            var coreProject = context.GetProjectByRole("Core");
            var servicesProject = context.GetProjectByRole("Services");

            foreach (var tableCategory in context.Tables.Select(p => p.Category).Distinct())
            {
                if (context.Tables.Where(p => p.Category == tableCategory).Any(p => Helper.IsNormalEntity(p)))
                {
                    ITemplateBase dtoFactoryTemplate = new DtoFactoryTemplate(context.Tables, tableCategory, servicesProject, coreProject);
                    var file = await dtoFactoryTemplate.OnCreateAsync();

                    servicesProject.Files.Add(file);
                    context.GeneratedFiles.Add(file);
                }
            }
        }
    }
}