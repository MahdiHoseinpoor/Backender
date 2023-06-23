using Backender.Translator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using File = Backender.Translator.Models.File;

namespace Backender.Translator.Handlers
{
    public static class SolutionHandler
    {
        public static Solution CreateSolution(string name,string NameSpace)
        {
            Solution solution = new Solution()
            {
                Name = name,
                NameSpace = NameSpace
            };
            return solution;
        }
        public static Project CreateProject(string name, string NameSpace,string Path="",string Sdk = "Microsoft.NET.Sdk")
        {
            Project project = new Project()
            {
                Name = name,
                NameSpace = NameSpace,
                Path = Path,
                SDK = Sdk
            };
            return project;
        }
        public static Project AddPackageToProject(this Project project ,string name,string version)
        {
            Package package = new Package()
            {
                Name = name,
                Version = version
            };
            project.Packages.Add(package);
            return project;
        }
        public static Project AddProjectReference(this Project project, Project reference)
        {
            project.ReferenceProjects.Add(reference);
            return project;
        }
        public static Project AddProjectReferences(this Project project,params Project[] references)
        {
            project.ReferenceProjects.AddRange(references);
            return project;
        }
        public static List<string> GetNameSpaces(this Project project,string contains="")
        {
            var namespaces = project.Files.Select(p => p.Options["Namespace"]).Where(p=>p.StartsWith(project.NameSpace + contains)).Distinct().ToList();
           
            return namespaces;
        }
        public static string GetNameSpaceOfFile(this Project project, string fileName)
        {
            var Namespace = project.Files.FirstOrDefault(p=>p.Name== fileName).Options["Namespace"];

            return Namespace;
        }
        public static File GetFileByName(this Project project, string fileName)
        {
            var file = project.Files.FirstOrDefault(p => p.Name == fileName);
            return file;
        }
    }

}
