using Backender.Translator.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Razor.Templating.Core;
using File = Backender.Translator.Models.File;
using Backender.Generator;
using Backender.Generator.Templates;

namespace Backender.Translator.Templates
{
    public class ServiceTemplate : ITemplateBase
    {
        public Table Table { get; set; }
        public Project EntityProject { get; set; }
        public Project DbContextProject { get; set; }
        public Project Project { get; set; }
        public ServiceTemplate(Table table, Project project, Project entityProject, Project dbContextProject)
        {
            Project = project;
            EntityProject = entityProject;
            Table = table;
            DbContextProject = dbContextProject;
        }
        public async Task<File> OnCreateAsync()
        {
            File file = new File();
            file.Name = $"{Table.Name}Service";
            file.Path = Helper.CreateFilePath(Project);
            if (!string.IsNullOrEmpty(Table.Category))
            {
                file.Path = Path.Combine(file.Path, Table.Category);
            }
            file.BodyContext = await RazorTemplateEngine.RenderAsync("/Templates/ServiceTemplate.cshtml", this);
            return file;
        }
    }
}
