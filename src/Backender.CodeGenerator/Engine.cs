using Backender.CodeEditor.CSharp;
using Backender.CodeEditor.CSharp.Objects;
using Backender.CodeGenerator.Patterns.Repo;
using Backender.Translator;
using System;
using System.Diagnostics;
using System.Text.Json;
using System.Xml.Linq;
using Enum = Backender.CodeEditor.CSharp.Objects.Enum;

namespace Backender.CodeGenerator
{
    public class Engine
    {
		public string FileName { get; set; }
		public Engine(string fileName)
		{
			FileName = fileName;
		}
		public Engine()
		{

		}
		public void Run()
		{
			var configFile = File.ReadAllText(FileName);
			var config = new Config();
			if (Path.GetExtension(FileName).ToLower() == ".json")
			{
				config = JsonSerializer.Deserialize<Config>(configFile);
			}
			if (Path.GetExtension(FileName).ToLower() == ".yaml")
			{
				config = YamlSerializer.Deserialize<Config>(configFile);
			}
			else if(Path.GetExtension(FileName).ToLower() == ".palino")
			{
				config = PalinoSerializer.Deserialize(configFile);
			}
			RepoPatternGenerator generator = new(config);
			var solution = generator.Run();
			Build(solution);
			//Build(savePath, solution);
		}
        public static void CreateSourceFiles(string savePath, Solution solution,List<SourceFile> sourceFiles)
        {
            foreach (var sourceFile in sourceFiles)
            {
                if (!Directory.Exists(sourceFile.Path))
                {
                    Directory.CreateDirectory(Path.Combine(savePath,solution.Name, sourceFile.Path));
                }
                File.WriteAllText(Path.Combine(savePath, solution.Name, sourceFile.Path, $"{sourceFile.Name}.cs"), sourceFile.SourceCode);
            }
        }
        public static void Build(Solution solution)
        {
            RepoSourceGenerator sourceGenerator = new ();
            var CsFileSources = new List<SourceFile>();
			string savePath = solution.SavePath;

			Process cmd = new ();
            cmd.StartInfo.FileName = "cmd.exe";
            cmd.StartInfo.RedirectStandardInput = true;
            cmd.StartInfo.UseShellExecute = false;
            cmd.Start();

			if (!CsFileSources.Any(p => p.Name == "BaseEntity"))
			{
				CsFileSources.Add(sourceGenerator.AddBaseEntity(solution.GetProjectByName(solution.Name + ".Core")));
			}
			CsFileSources.AddRange(sourceGenerator.AddBaseRepo(solution.GetProjectByName(solution.Name+".Data")));


            using (StreamWriter sw = cmd.StandardInput)
            {
                if (sw.BaseStream.CanWrite)
                {
                    if (!Directory.Exists(Path.Combine(savePath, solution.Name)))
                    {
                        Directory.CreateDirectory(Path.Combine(savePath, solution.Name));
                    }
                    sw.WriteLine($"cd /d {Path.Combine(savePath, solution.Name)}");
					var solutionPath = Path.Combine(savePath, solution.Name, $"{solution.Name}.sln");
					if (!File.Exists(solutionPath))
					{
						sw.WriteLine($"dotnet new sln --name {solution.Name}");
					}
					foreach (var project in solution.Projects)
                    {
                        if (!Directory.Exists(Path.Combine(savePath, solution.Name , project.Name)))
                        {
                            Directory.CreateDirectory(Path.Combine(savePath, solution.Name,project.Name));
                        }
						if (!File.Exists(Path.Combine(savePath, solution.Name, project.Name , $"{project.Name}.csproj")))
						{
							sw.WriteLine($"dotnet new classlib -n {project.Name} -f net7.0");
							sw.WriteLine($"del {project.Name}\\Class1.cs");
						}
                        sw.WriteLine($"dotnet sln add {project.Name}");
                        sw.WriteLine($"dotnet add {project.Name} package Microsoft.EntityFrameworkCore.SqlServer");
                    }
                    foreach (var project in solution.Projects)
                    {
                        foreach (var projectRef in  project.ProjectReference)
                        {
                            sw.WriteLine($"dotnet add {project.Name}/{project.Name}.csproj reference {projectRef.Name}/{projectRef.Name}.csproj");
                        }
                        foreach (var csFile in project.CsFiles)
                        {
                            var CsFileSource = new SourceFile();
                            if (csFile is Class @class)
                            {
                                CsFileSource = sourceGenerator.ClassToSource(@class);
                            }
                            else if (csFile is Interface @interface)
                            {
                                CsFileSource = (sourceGenerator.InterfaceToSource(@interface));
                            }
							else if (csFile is Enum @enum)
							{
								CsFileSource = (sourceGenerator.EnumToSource(@enum));
							}
							CsFileSource.Path = sourceGenerator.GetFilePath(project, csFile.NameSpace);
                            CsFileSources.Add(CsFileSource);
                        }
                    }
                }
            }
			CreateSourceFiles(savePath, solution, CsFileSources);
        }
    }
}
