using Backender.Generator.Templates;
using Backender.Translator.Templates;
using System.Threading.Tasks;

namespace Backender.Generator.Steps
{
    public class ProjectFilesGenerationStep : IGenerationStep
    {
        public async Task ExecuteAsync(GenerationContext context)
        {
            foreach (var project in context.Solution.Projects)
            {
                ITemplateBase projectTemplate = new ProjectTemplate(project);
                var file = await projectTemplate.OnCreateAsync();
                context.GeneratedFiles.Add(file);
            }
        }
    }
}