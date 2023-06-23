using Backender.Translator.Handlers;
using Backender.Translator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backender.Generator
{
    public static class Helper
    {
        public static string CreateFilePath(Project project,params string[] paths)
        {
            string path = Path.Combine(project.Path,project.Name);
            var _paths = new List<string>();
            _paths.Add(path);
            _paths.AddRange(paths);
             path = Path.Combine(_paths.ToArray());
            return path;
        }
        public static bool IsNormalEntity(this Table table) {
            return !(table.Options.HasOption("MiddleEntity") || table.Options.HasOption("baseentity"));
        }
    }
}
