using Backender.Translator.Models;
using Backender.Generator;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Razor.Templating.Core;
using File = Backender.Translator.Models.File;
using Backender.Generator.Templates;

namespace Backender.Translator.Templates
{
    public class DtoFactoryTemplate : ITemplateBase
    {
        public List<Table> Tables { get; set; }
        public string TableCategory { get; set; }
        public Project Project { get; set; }
        public Project EntityProject { get; set; }

        public DtoFactoryTemplate(List<Table> tables, string tableCategory, Project project,Project entityProject)
        {
            Project = project;
            Tables = tables;
            TableCategory = tableCategory;
            EntityProject = entityProject;
        }
        public async Task<File> OnCreateAsync()
        {
            File file = new File();
            file.Name = TableCategory + "DtosFactory";
            file.Path = Helper.CreateFilePath(Project);
            if (!string.IsNullOrEmpty(TableCategory))
            {
                file.Path = Path.Combine(file.Path, TableCategory);
            }
            file.BodyContext = await RazorTemplateEngine.RenderAsync("/Templates/DtoFactoryTemplate.cshtml", this);
            return file;
        }
    }
}
