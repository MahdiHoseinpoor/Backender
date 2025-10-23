using Backender.Generator.Templates;
using Backender.Translator.Templates;
using System.Linq;
using System.Threading.Tasks;

namespace Backender.Generator.Steps
{
    public class ServiceGenerationStep : IGenerationStep
    {
        public async Task ExecuteAsync(GenerationContext context)
        {
            var coreProject = context.GetProjectByRole("Core");
            var dataProject = context.GetProjectByRole("Data");
            var servicesProject = context.GetProjectByRole("Services");

            foreach (var table in context.Tables.Where(p => Helper.IsNormalEntity(p)))
            {
                // Generate IService
                ITemplateBase iServiceTemplate = new IServiceTemplate(table, servicesProject, coreProject, dataProject);
                var iServiceFile = await iServiceTemplate.OnCreateAsync();
                servicesProject.Files.Add(iServiceFile);
                context.GeneratedFiles.Add(iServiceFile);

                // Generate Service
                ITemplateBase serviceTemplate = new ServiceTemplate(table, servicesProject, coreProject, dataProject);
                var serviceFile = await serviceTemplate.OnCreateAsync();
                servicesProject.Files.Add(serviceFile);
                context.GeneratedFiles.Add(serviceFile);
            }
        }
    }
}