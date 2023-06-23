using Backender.Generator.Templates;
using Backender.Translator.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Razor.Templating.Core;
using File = Backender.Translator.Models.File;

namespace Backender.Translator.Templates
{
    public class SolutionTemplate : ITemplateBase
    {
        public Solution Solution { get; set; }
        public string SavePath { get; set; }
        public SolutionTemplate(Solution solution,string savePath="")
        {
            Solution = solution;
            SavePath = savePath;
        }
        public async Task<File> OnCreateAsync()
        {
            File file = new File();
            file.Name = Solution.Name;
            file.Path = SavePath;
            file.Extension = ".sln";
            file.BodyContext = await RazorTemplateEngine.RenderAsync("/Templates/SolutionTemplate.cshtml", this);
            return file;
        }
    }
}
