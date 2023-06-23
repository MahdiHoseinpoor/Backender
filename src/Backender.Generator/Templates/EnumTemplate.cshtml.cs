using Backender.Translator;
using Backender.Translator.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Razor.Templating.Core;
using System;
using File = Backender.Translator.Models.File;
using Backender.Generator;
using Backender.Generator.Templates;

namespace Backender.Translator.Templates
{ 
    public class EnumTemplate : ITemplateBase
    {
        public Enum_ Enum_ { get; set; }
        public Project Project { get; set; }
        public EnumTemplate(Enum_ enum_, Project project)
        {
            Enum_ = enum_;
            Project = project;
        }
        public async Task<File> OnCreateAsync()
        {
            File file = new File();
            file.Name = Enum_.EnumName;
            file.Path = Helper.CreateFilePath(Project, "Enums");
            file.Options["Namespace"] = Project.NameSpace + ".Enums";

            file.BodyContext = await RazorTemplateEngine.RenderAsync("/Templates/EnumTemplate.cshtml", this);
            return file;
        }
    }
}
