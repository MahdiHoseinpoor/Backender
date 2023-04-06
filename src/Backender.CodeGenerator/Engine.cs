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
		string _savePath;
		public string FileName { get; set; }
		public string SavePath { get { return _savePath; } }
		public Engine(string fileName)
		{
			FileName = fileName;
		}
		public Engine()
		{

		}
		public async Task Run()
		{
			var configFile = await File.ReadAllTextAsync(FileName);
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
			_savePath = Path.Combine(config.SavePath, config.SolutionName);
			RepoPatternGenerator generator = new(config);
			var solution =await generator.Run();
			await Build(solution);
			//Build(savePath, solution);
		}
        public static async Task CreateSourceFiles(string savePath, Solution solution,List<SourceFile> sourceFiles)
        {
            foreach (var sourceFile in sourceFiles)
            {
                if (!Directory.Exists(sourceFile.Path))
                {
                    Directory.CreateDirectory(Path.Combine(savePath,solution.Name, sourceFile.Path));
                }
               await File.WriteAllTextAsync(Path.Combine(savePath, solution.Name, sourceFile.Path, $"{sourceFile.Name}.cs"), sourceFile.SourceCode);
			  Log($"'{sourceFile.Name}.cs' has been created", ConsoleColor.White);

			}
		}
        public static async Task Build(Solution solution)
        {
            RepoSourceGenerator sourceGenerator = new ();
            var CsFileSources = new List<SourceFile>();
			string savePath = solution.SavePath;

			Process cmd = new ();
            cmd.StartInfo.FileName = "cmd.exe";
            cmd.StartInfo.RedirectStandardInput = true;
            cmd.StartInfo.UseShellExecute = false;
#if DEBUG
#else	
			cmd.StartInfo.CreateNoWindow = true;
#endif
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
					else
					{
						Log($"a solution named '{solution.Name}' in path '{solution.SavePath}' is existing",ConsoleColor.Yellow);
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
						else
						{
							Log($"a Project named '{project.Name}' is existing", ConsoleColor.Yellow);
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
			await cmd.WaitForExitAsync();

			await CreateSourceFiles(savePath, solution, CsFileSources);
        }
		public static void Log(string content, ConsoleColor consoleColor=ConsoleColor.White)
		{
			Console.ForegroundColor = consoleColor;
			Console.WriteLine($"------ [{DateTime.Now}] : {content}\n");
		}
	}
}
