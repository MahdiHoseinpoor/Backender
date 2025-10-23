using Backender.Generator.Templates;
using Backender.Translator.Templates;
using System.Threading.Tasks;

namespace Backender.Generator.Steps
{
    public class RepoGenerationStep : IGenerationStep
    {
        public async Task ExecuteAsync(GenerationContext context)
        {
            var coreProject = context.GetProjectByRole("Core");
            var dataProject = context.GetProjectByRole("Data");

            // Generate IRepo.cs
            ITemplateBase iRepoTemplate = new IRepoTemplate(context.Tables, dataProject, coreProject);
            var iRepoFile = await iRepoTemplate.OnCreateAsync();
            dataProject.Files.Add(iRepoFile);
            context.GeneratedFiles.Add(iRepoFile);

            // Generate Repo.cs
            ITemplateBase repoTemplate = new RepoTemplate(context.Tables, dataProject, coreProject);
            var repoFile = await repoTemplate.OnCreateAsync();
            dataProject.Files.Add(repoFile);
            context.GeneratedFiles.Add(repoFile);
        }
    }
}