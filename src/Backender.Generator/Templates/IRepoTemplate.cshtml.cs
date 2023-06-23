using Backender.Translator.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Backender.Generator;
using Razor.Templating.Core;
using File = Backender.Translator.Models.File;
using Backender.Generator.Templates;

namespace Backender.Translator.Templates
{
    public class IRepoTemplate : ITemplateBase
    {
        public List<Table> Tables { get; set; }
        public Project EntityProject { get; set; }
        public Project Project { get; set; }
        public IRepoTemplate(List<Table> tables, Project project, Project entityProject)
        {
            Project = project;
            EntityProject = entityProject;
            Tables = tables;
        }
        public async Task<File> OnCreateAsync()
        {
            File file = new File();
            file.Name = "IRepo";
            file.Path = Helper.CreateFilePath(Project);
            file.BodyContext = await RazorTemplateEngine.RenderAsync("/Templates/IRepoTemplate.cshtml", this);
            return file;
        }
    }
}
