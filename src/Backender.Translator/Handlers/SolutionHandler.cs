using Backender.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using File = Backender.Core.Models.File;

namespace Backender.Translator.Handlers
{
    public static class SolutionHandler
    {
        public static Solution CreateFromBlueprint(Blueprint blueprint)
        {
            var solution = new Solution
            {
                Name = blueprint.Solution.SolutionName,
                NameSpace = blueprint.Solution.SolutionNamespace
            };

            if (blueprint.Solution.UseDefaultStructure || !blueprint.Solution.Projects.Any())
            {
                CreateDefaultSolutionStructure(solution);
            }
            else
            {
                CreateCustomSolutionStructure(solution, blueprint.Solution.Projects);
            }

            return solution;
        }
        private static void CreateDefaultSolutionStructure(Solution solution)
        {
            var coreProject = new Project
            {
                Name = $"{solution.Name}.Core",
                NameSpace = $"{solution.NameSpace}.Core",
                Path = "Libraries",
                Role = "Core"
            };
            coreProject.Packages.Add(new Package { Name = "FluentValidation", Version = "11.5.2" });

            var dataProject = new Project
            {
                Name = $"{solution.Name}.Data",
                NameSpace = $"{solution.NameSpace}.Data",
                Path = "Libraries",
                Role = "Data"
            };
            dataProject.Packages.Add(new Package { Name = "Microsoft.EntityFrameworkCore.SqlServer", Version = "7.0.5" });
            dataProject.ReferenceProjects.Add(coreProject);

            var servicesProject = new Project
            {
                Name = $"{solution.Name}.Services",
                NameSpace = $"{solution.NameSpace}.Services",
                Path = "Libraries",
                Role = "Services"
            };
            servicesProject.Packages.Add(new Package { Name = "Microsoft.EntityFrameworkCore.SqlServer", Version = "7.0.5" });
            servicesProject.ReferenceProjects.Add(coreProject);
            servicesProject.ReferenceProjects.Add(dataProject);

            solution.Projects.AddRange(new[] { coreProject, dataProject, servicesProject });
        }
        private static void CreateCustomSolutionStructure(Solution solution, List<Project_> projectDefinitions)
        {
            foreach (var projDef in projectDefinitions)
            {
                string projectName = projDef.Name.Replace("{Solution.Name}", solution.Name);
                var project = new Project
                {
                    Name = projectName,
                    NameSpace = $"{solution.NameSpace}.{projectName.Split('.').Last()}",
                    Path = projDef.Path,
                    SDK = projDef.Sdk,
                    Role = projDef.Role
                };
                foreach (var pkg in projDef.PackageReferences)
                {
                    project.Packages.Add(new Package { Name = pkg.Include, Version = pkg.Version });
                }
                solution.Projects.Add(project);
            }
            foreach (var projDef in projectDefinitions)
            {
                var currentProject = solution.Projects.First(p => p.Name == projDef.Name.Replace("{Solution.Name}", solution.Name));
                foreach (var projRef in projDef.ProjectReferences)
                {
                    string referenceName = projRef.Include.Replace("{Solution.Name}", solution.Name);
                    var referencedProject = solution.Projects.FirstOrDefault(p => p.Name == referenceName);
                    if (referencedProject != null)
                    {
                        currentProject.ReferenceProjects.Add(referencedProject);
                    }
                }
            }
        }
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
