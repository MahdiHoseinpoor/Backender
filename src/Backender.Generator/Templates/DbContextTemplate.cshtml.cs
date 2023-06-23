using Backender.Translator;
using Backender.Generator;
using Backender.Generator.Templates;
using Backender.Translator.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Razor.Templating.Core;
using File = Backender.Translator.Models.File;

namespace Backender.Translator.Templates
{
    public class DbContextTemplate : ITemplateBase
    {
        public List<Table> Tables { get; set; }
        public Project EntityProject { get; set; }
        public Project Project { get; set; }
        public DbContextTemplate(List<Table> tables, Project project, Project entityProject)
        {   
            Project = project;
            EntityProject = entityProject;
            Tables = tables;
        }
        public async Task<File> OnCreateAsync()
        {
            File file = new File();
            file.Name = "ApplicationDbContext";
            file.Path = Helper.CreateFilePath(Project);
            file.BodyContext = await RazorTemplateEngine.RenderAsync("/Templates/DbContextTemplate.cshtml", this);
            return file;
        }
    }
}
