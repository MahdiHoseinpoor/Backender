using Backender.Translator.Models;
using Backender.Generator;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Razor.Templating.Core;
using File = Backender.Translator.Models.File;
using Backender.Generator.Templates;
using Backender.Translator.Handlers;

namespace Backender.Translator.Templates
{
    public class DtoTemplate : ITemplateBase
    {
        public Table CurrentTable { get; set; }
        public List<Table> Tables { get; set; }
        public Project Project { get; set; }
        public DtoTemplate(List<Table> tables, Table currentTable, Project project)
        {
            CurrentTable = currentTable;
            Tables = tables;
            Project = project;
        }
        public async Task<File> OnCreateAsync()
        {
            File file = new File();
            if (!CurrentTable.Options.HasOption("baseentity"))
            {
                file.Name = CurrentTable.Name + "Dto";
            }
            else
            {
                file.Name = "BaseDto";
            }
            file.Path = Helper.CreateFilePath(Project, "Dtos");
            file.Options["Namespace"] = Project.NameSpace + ".Dtos" + (!string.IsNullOrEmpty(CurrentTable.Category) ? $".{CurrentTable.Category}" : string.Empty);
            if (!string.IsNullOrEmpty(CurrentTable.Category))
            {
                file.Path = Path.Combine(file.Path, CurrentTable.Category);
            }
            file.BodyContext = await RazorTemplateEngine.RenderAsync("/Templates/DtoTemplate.cshtml", this);
            return file;
        }
    }
}
