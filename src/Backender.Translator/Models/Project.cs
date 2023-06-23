using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backender.Translator.Models
{
    public class Project
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }
        public string SDK { get; set; } = "Microsoft.NET.Sdk";
        public string NameSpace { get; set; }
        public string Path { get; set; }
        public List<File> Files { get; set; } = new();
        public List<Project> ReferenceProjects { get; set; } = new();
        public List<Package> Packages { get; set; } = new();
    }
}
