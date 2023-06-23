using Backender.Translator;
using Backender.Generator;
using Backender.Generator.Templates;
using Backender.Translator.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Razor.Templating.Core;
using System.Reflection;
using File = Backender.Translator.Models.File;

namespace Backender.Translator.Templates
{
    public class EntityTemplate : ITemplateBase
    {
        public Table CurrentTable { get; set; }
        public List<Table> Tables { get; set; }
        public Project Project { get; set; }

        public EntityTemplate(List<Table> tables ,Table currentTable, Project project)
        {
            CurrentTable = currentTable;
            Tables = tables;
            Project = project;

        }
        public async Task<File> OnCreateAsync()
        {
            File file = new File();
            file.Name = CurrentTable.Name;
            file.Path = Helper.CreateFilePath(Project, "Domains");
            file.Options["Namespace"] = Project.NameSpace + ".Domains" + (!string.IsNullOrEmpty(CurrentTable.Category) ? $".{CurrentTable.Category}" : string.Empty);
            if (!string.IsNullOrEmpty(CurrentTable.Category))
            {
                file.Path = Path.Combine(file.Path, CurrentTable.Category);
            }
            file.BodyContext = await RazorTemplateEngine.RenderAsync("/Templates/EntityTemplate.cshtml", this);
            return file;
        }
    }
}
