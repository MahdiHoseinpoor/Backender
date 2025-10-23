using Backender.Generator.Templates;
using Backender.Translator.Templates;
using System.Threading.Tasks;

namespace Backender.Generator.Steps
{
    public class EnumGenerationStep : IGenerationStep
    {
        public async Task ExecuteAsync(GenerationContext context)
        {
            var coreProject = context.GetProjectByRole("Core");
            foreach (var enum_ in context.Blueprint.Domains.Enums)
            {
                ITemplateBase enumTemplate = new EnumTemplate(enum_, coreProject);
                var file = await enumTemplate.OnCreateAsync();

                coreProject.Files.Add(file);
                context.GeneratedFiles.Add(file);
            }
        }
    }
}