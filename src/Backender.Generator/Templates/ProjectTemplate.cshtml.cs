using Backender.Translator.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Razor.Templating.Core;
using Backender.Generator;
using File = Backender.Translator.Models.File;
using Backender.Generator.Templates;

namespace Backender.Translator.Templates
{
    public class ProjectTemplate : ITemplateBase
    {

        public Project Project { get; set; }
        public ProjectTemplate(Project project)
        {
            Project = project;
        }
        public async Task<File> OnCreateAsync()
        {
            File file = new File();
            file.Name = Project.Name;
            file.Path = Helper.CreateFilePath(Project);
            file.Extension = ".csproj";
            file.BodyContext = await RazorTemplateEngine.RenderAsync("/Templates/ProjectTemplate.cshtml", this);
            return file;
        }

    }
}
