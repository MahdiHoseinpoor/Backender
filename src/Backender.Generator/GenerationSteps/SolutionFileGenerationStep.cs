using Backender.Generator.Templates;
using Backender.Translator.Templates;
using System.Threading.Tasks;

namespace Backender.Generator.Steps
{
    public class SolutionFileGenerationStep : IGenerationStep
    {
        public async Task ExecuteAsync(GenerationContext context)
        {
            ITemplateBase solutionTemplate = new SolutionTemplate(context.Solution);
            var file = await solutionTemplate.OnCreateAsync();
            context.GeneratedFiles.Add(file);
        }
    }
}