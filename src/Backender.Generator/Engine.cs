using Backender.Translator;
using Backender.Translator.Handlers;
using Backender.Generator.Templates;

using Backender.Translator.Models;
using Backender.Translator.Templates;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Emit;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using File = Backender.Translator.Models.File;

namespace Backender.Generator
{
    public class Engine
    {
        public async Task RunAsync(Blueprint Blueprint)
        {
            if (string.IsNullOrEmpty(Blueprint.SavePath))
            {
                Blueprint.SavePath = Path.Combine(Environment.GetFolderPath(
                  Environment.SpecialFolder.MyDoc‌​uments), "BackenderV2 2023", "Sources", Blueprint.Solution.SolutionName);
            }
            var Files = new List<File>();
            Solution solution = SolutionHandler.CreateSolution(Blueprint.Solution.SolutionName, Blueprint.Solution.SolutionNamespace);
            List<Table> Tables = TableHandler.CreateTables(Blueprint);
            Project CoreProj = SolutionHandler.CreateProject(solution.Name + ".Core", solution.NameSpace + ".Core", "Libraries")
                .AddPackageToProject("FluentValidation", "11.5.2"); ;
            Project DataProj = SolutionHandler.CreateProject(solution.Name + ".Data", solution.NameSpace + ".Data", "Libraries")
                .AddProjectReferences(CoreProj)
                .AddPackageToProject("Microsoft.EntityFrameworkCore.SqlServer", "7.0.5");

            Project ServicesProj = SolutionHandler.CreateProject(solution.Name + ".Services", solution.NameSpace + ".Services", "Libraries")
                .AddProjectReferences(CoreProj, DataProj)
                .AddPackageToProject("Microsoft.EntityFrameworkCore.SqlServer", "7.0.5");

            //Project ApiProj = SolutionHandler.CreateProject(solution.Name + ".Api", solution.NameSpace + ".Api", "Presentation", "Microsoft.NET.Sdk.Web")
            //    .AddProjectReferences(CoreProj, DataProj, ServicesProj)
            //    .AddPackageToProject("Microsoft.AspNetCore.OpenApi", "7.0.2")
            //    .AddPackageToProject("Swashbuckle.AspNetCore", "6.4.0")
            //    .AddPackageToProject("Microsoft.EntityFrameworkCore.Design", "7.0.5");
            foreach (var enum_ in Blueprint.Domains.Enums)
            {
                await CreateEnumFileAsync(Files, enum_, CoreProj);
            }
            foreach (var table in Tables)
            {
                await CreateEntityFileAsync(Files, Tables, table, CoreProj);

                if (!table.Options.HasOption("MiddleEntity"))
                {
                    await CreateDtoFileAsync(Files, Tables, table, CoreProj);
                }
            }
            foreach (var tableCategory in Tables.Select(p => p.Category).Distinct())
            {
                if (Tables.Where(p => p.Category == tableCategory).Any(p => p.IsNormalEntity()))
                {
                    await CreateDtoFactoryFileAsync(Files, Tables, tableCategory, ServicesProj, CoreProj);
                }
            }
            await CreateDbContextFileAsync(Files, Tables, CoreProj, DataProj);
            await CreateRepoFileAsync(Files, Tables, CoreProj, DataProj);
            solution.Projects.Add(CoreProj);
            solution.Projects.Add(DataProj);
            solution.Projects.Add(ServicesProj);
            //solution.Projects.Add(ApiProj);
            await CreateSolutionFilesAsync(Blueprint, Files, solution);
            foreach (var project in solution.Projects)
            {
                await CreateProjectsFilesAsync(Blueprint, Files, project);
            }
            await CreateUnitOfWorkFileAsync(Files, Tables, ServicesProj, CoreProj, DataProj);
            foreach (var table in Tables.Where(p => p.IsNormalEntity()))
            {
                await CreateServiceFileAsync(Files, table, CoreProj, DataProj, ServicesProj);
                //await CreateAPIControllerFileAsync(Files, table, ApiProj, CoreProj, DataProj, ServicesProj);
            }
            //await CreateAPIProgramFileAsync(Files, ApiProj, DataProj, ServicesProj);
            foreach (var file in Files)
            {
                await BuildFileAsync(file, Blueprint.SavePath);
            }
        }

        

        private async Task BuildFileAsync(File file, string savePath)
        {
            if (!Directory.Exists(file.Path))
            {
                Directory.CreateDirectory(Path.Combine(savePath, file.Path));
            }
            await System.IO.File.WriteAllTextAsync(Path.Combine(savePath, file.Path, file.Name + file.Extension), file.BodyContext);
        }

        private static async Task CreateProjectsFilesAsync(Blueprint Blueprint, List<File> Files, Project project)
        {
            ITemplateBase ProjectTemplate = new ProjectTemplate(project);
            var file = await ProjectTemplate.OnCreateAsync();
            Files.Add(file);
        }
        private static async Task CreateSolutionFilesAsync(Blueprint Blueprint, List<File> Files, Solution solution)
        {
            ITemplateBase SolutionTemplate = new SolutionTemplate(solution);
            var solutionFile = await SolutionTemplate.OnCreateAsync();
            Files.Add(solutionFile);
        }

        private static async Task CreateServiceFileAsync(List<File> Files, Table table, Project CoreProj, Project DataProj, Project ServicesProj)
        {
            IServiceTemplate iServicesTemplate = new IServiceTemplate(table, ServicesProj, CoreProj, DataProj);
            var file = await iServicesTemplate.OnCreateAsync();
            ServicesProj.Files.Add(file);
            Files.Add(file);

            ITemplateBase serviceTemplate = new ServiceTemplate(table, ServicesProj, CoreProj, DataProj);
            file = await serviceTemplate.OnCreateAsync();
            ServicesProj.Files.Add(file);
            Files.Add(file);
        }

        private static async Task CreateDbContextFileAsync(List<File> Files, List<Table> Tables, Project CoreProj, Project DataProj)
        {
            ITemplateBase dbContextTemplate = new DbContextTemplate(Tables, DataProj, CoreProj);
            var dbcontext = await dbContextTemplate.OnCreateAsync();
            DataProj.Files.Add(dbcontext);
            Files.Add(dbcontext);
        }
        private static async Task CreateRepoFileAsync(List<File> Files, List<Table> Tables, Project CoreProj, Project DataProj)
        {
            ITemplateBase IRepoTemplate = new IRepoTemplate(Tables, DataProj, CoreProj);
            var IRepo = await IRepoTemplate.OnCreateAsync();
            DataProj.Files.Add(IRepo);
            Files.Add(IRepo);

            ITemplateBase RepoTemplate = new RepoTemplate(Tables, DataProj, CoreProj);
            var Repo = await RepoTemplate.OnCreateAsync();
            DataProj.Files.Add(Repo);
            Files.Add(Repo);
        }

        private static async Task CreateEntityFileAsync(List<File> Files, List<Table> tables, Table CurrentTable, Project CoreProj)
        {
            ITemplateBase entityTemplate = new EntityTemplate(tables, CurrentTable, CoreProj);
            var file = await entityTemplate.OnCreateAsync();
            CoreProj.Files.Add(file);
            Files.Add(file);

        }
        private static async Task CreateEnumFileAsync(List<File> Files, Enum_ Enum_, Project CoreProj)
        {
            ITemplateBase enumTemplate = new EnumTemplate(Enum_, CoreProj);
            var file = await enumTemplate.OnCreateAsync();
            CoreProj.Files.Add(file);
            Files.Add(file);
        }
        private static async Task CreateDtoFileAsync(List<File> Files, List<Table> tables, Table CurrentTable, Project CoreProj)
        {
            ITemplateBase dtoTemplate = new DtoTemplate(tables, CurrentTable, CoreProj);
            var file = await dtoTemplate.OnCreateAsync();
            CoreProj.Files.Add(file);
            Files.Add(file);
        }
        private static async Task CreateDtoFactoryFileAsync(List<File> Files, List<Table> tables, string TableCategory, Project ServiceProj, Project CoreProj)
        {
            ITemplateBase dtoFactoryTemplate = new DtoFactoryTemplate(tables, TableCategory, ServiceProj, CoreProj);
            var file = await dtoFactoryTemplate.OnCreateAsync();
            ServiceProj.Files.Add(file);
            Files.Add(file);
        }
        private static async Task CreateUnitOfWorkFileAsync(List<File> Files, List<Table> tables, Project ServiceProj, Project CoreProj, Project DataProj)
        {
            ITemplateBase unitOfWorkTemplate = new UnitOfWorkTemplate(tables, ServiceProj, CoreProj, DataProj);
            var file = await unitOfWorkTemplate.OnCreateAsync();
            ServiceProj.Files.Add(file);
            Files.Add(file);
        }
    }
}
