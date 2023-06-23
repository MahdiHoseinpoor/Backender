using Backender.Translator.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Razor.Templating.Core;
using File = Backender.Translator.Models.File;
using Backender.Generator;
using Backender.Generator.Templates;

namespace Backender.Translator.Templates
{
    public class UnitOfWorkTemplate : ITemplateBase
    {
        public List<Table> Tables { get; set; }
        public Project EntityProject { get; set; }
        public Project DataProject { get; set; }
        public Project Project { get; set; }
        public UnitOfWorkTemplate(List<Table> tables,  Project project, Project entityProject, Project dataProject)
        {
            Project = project;
            Tables = tables;
            EntityProject = entityProject;
            DataProject = dataProject;
        }
        public async Task<File> OnCreateAsync()
        {
            File file = new File();
            file.Name = "UnitOfWork";
            file.Path = Helper.CreateFilePath(Project);
            file.BodyContext = await RazorTemplateEngine.RenderAsync("/Templates/UnitOfWorkTemplate.cshtml", this);
            return file;
        }
    }
}
