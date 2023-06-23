using Backender.Translator.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Razor.Templating.Core;
using File = Backender.Translator.Models.File;
using Backender.Generator;
using Backender.Generator.Templates;

namespace Backender.Translator.Templates
{
    public class RepoTemplate : ITemplateBase
    {
        public List<Table> Tables { get; set; }
        public Project EntityProject { get; set; }
        public Project Project { get; set; }
        public RepoTemplate(List<Table> tables, Project project, Project entityProject)
        {
            Project = project;
            EntityProject = entityProject;
            Tables = tables;
        }
        public async Task<File> OnCreateAsync()
        {
            File file = new File();
            file.Name = "Repo";
            file.Path = Helper.CreateFilePath(Project);
            file.BodyContext = await RazorTemplateEngine.RenderAsync("/Templates/RepoTemplate.cshtml", this);
            return file;
        }
    }
}
