using Backender.CodeEditor.CSharp;
using Backender.CodeEditor.CSharp.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backender.CodeGenerator.Patterns.Repo
{

    public class RepoSourceGenerator : SourceGenerator
    {
        public List<SourceFile> AddBaseRepo(Project dataProj)
        {
            var IRepoSourceFile = new SourceFile();
            var RepoSourceFile = new SourceFile();
            var sourceFiles = new List<SourceFile>();
            var IRepoSource = BaseSources.IRepoSource;
            var RepoSource = BaseSources.RepoSource;
            var Namespace = dataProj.DefaultNameSpace;
            IRepoSource = SetNameSpace(IRepoSource, Namespace);
            RepoSource = SetNameSpace(RepoSource, Namespace);
            var dbContext = dataProj.CsFiles.OfType<Class>().FirstOrDefault(p => p.BaseClassName == "DbContext");
            RepoSource = RepoSource.Replace("$Dbcontext$", dbContext.Name);
            var usings =new List<string>();
            usings.Add(dbContext.NameSpace);
            usings.Add("System.Linq.Expressions");
            usings.Add("Microsoft.EntityFrameworkCore");
            foreach (var projReference in dataProj.ProjectReference)
            {
                usings.Add(projReference.DefaultNameSpace);
            }
            IRepoSource = AddUsings(IRepoSource, usings);
            RepoSource = AddUsings(RepoSource, usings);

            IRepoSourceFile.SourceCode = IRepoSource;
            RepoSourceFile.SourceCode = RepoSource;
            RepoSourceFile.Name = RepoSource;
            IRepoSourceFile.ProjectName = dataProj.Name;        
            RepoSourceFile.ProjectName = dataProj.Name;

            IRepoSourceFile.Name = "IRepo";
            RepoSourceFile.Name = "Repo"; 
            IRepoSourceFile.Path = GetFilePath(dataProj, Namespace);
            RepoSourceFile.Path = GetFilePath(dataProj, Namespace);

            sourceFiles.Add(IRepoSourceFile);
            sourceFiles.Add(RepoSourceFile);

         
            return sourceFiles;
        }
        public SourceFile AddBaseEntity(Project coreProj)
        {
            var usings = new List<string>();

            var SourceFile = new SourceFile();

            var Class = new Class()
            {
                Name = "BaseEntity",
                NameSpace = coreProj.DefaultNameSpace,
                AccessModifier = AccessModifier.Public,
                IsStatic = false,
                ProjectName = coreProj.Name,
                InnerItems = new List<InnerCsFileItem>()
            };
			var BaseEntityProperties = new List<Property>
			{
				new Property()
				{
					AccessModifier = AccessModifier.Public,
					DataType = "string",
					Name = "Id",
					ClassName = Class.Name

				}.AddAttributeToProperty("Key").AddRequiredNameSpaces("System.ComponentModel.DataAnnotations"),

				new Property()
				{
					AccessModifier = AccessModifier.Public,
					DataType = "DateTime",
					Name = "CreatedAt_",
					ClassName = Class.Name
				},
				new Property()
				{
					AccessModifier = AccessModifier.Public,
					DataType = "DateTime",
					Name = "ModifiedAt_",
					ClassName = Class.Name
				}
			};
			Class.InnerItems.AddRange(BaseEntityProperties);

            SourceFile.Name = "BaseEntity";
            SourceFile.SourceCode =  ClassToSource(Class).SourceCode;
            SourceFile.ProjectName = coreProj.Name;
            SourceFile.Path= GetFilePath(coreProj, Class.NameSpace);
            return SourceFile;
        }
		public SourceFile AddBaseDto(Project coreProj)
		{
			var usings = new List<string>();

			var SourceFile = new SourceFile();

			var Class = new Class()
			{
				Name = "BaseDto",
				NameSpace = coreProj.DefaultNameSpace,
				AccessModifier = AccessModifier.Public,
				IsStatic = false,
				ProjectName = coreProj.Name,
				InnerItems = new List<InnerCsFileItem>()
			};
			var BaseEntityProperties = new List<Property>
			{
				new Property()
				{
					AccessModifier = AccessModifier.Public,
					DataType = "string",
					Name = "Id",
					ClassName = Class.Name

				},

				new Property()
				{
					AccessModifier = AccessModifier.Public,
					DataType = "DateTime",
					Name = "CreatedAt_",
					ClassName = Class.Name
				},
				new Property()
				{
					AccessModifier = AccessModifier.Public,
					DataType = "DateTime",
					Name = "ModifiedAt_",
					ClassName = Class.Name
				}
			};
			Class.InnerItems.AddRange(BaseEntityProperties);

			SourceFile.Name = "BaseDto";
			SourceFile.SourceCode = ClassToSource(Class).SourceCode;
			SourceFile.ProjectName = coreProj.Name;
			SourceFile.Path = GetFilePath(coreProj, Class.NameSpace);
			return SourceFile;
		}

	}
}
