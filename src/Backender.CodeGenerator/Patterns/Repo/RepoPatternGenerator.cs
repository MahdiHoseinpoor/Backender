using Backender.CodeEditor.CSharp;
using Backender.CodeEditor.CSharp.Objects;
using Backender.CodeGenerator;
using Backender.CodeGenerator.Patterns;
using Backender.Translator;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Backender.CodeGenerator.Patterns.Repo
{
	/// <summary>
	/// This class Create solutions, project and thier classes in Repository Pattern
	/// </summary>
	public class RepoPatternGenerator: SolutionGenerator
    {
        public Config _config;
        public Solution solution;
        public RepoPatternGenerator(Config Config)
        {
            _config = Config;
        }

        public override void OnConfigure()
        {
            throw new NotImplementedException();
        }

		/// <summary>
		/// Runing the Solution building Action 
		/// </summary>
		/// <returns>
		/// The Solution object that created.
		/// </returns>
		public override async Task<Solution> Run()
        {
            //Create Solution
            solution = new Solution(_config.SolutionName);
			solution.SavePath = _config.SavePath;
			//Add Projects
			var CoreProjName = _config.SolutionName + ".Core";
            var DataProjName = _config.SolutionName + ".Data";
            var ServicesProjName = _config.SolutionName + ".Services";
            var CoreProj = solution.AddProject(CoreProjName);
            var DataProj = solution.AddProject(DataProjName);
            var ServicesProj = solution.AddProject(ServicesProjName);

            //Add Project Refrences
            DataProj = DataProj.AddProjectReference(CoreProj);
            ServicesProj = ServicesProj.AddProjectReference(CoreProj);
            ServicesProj = ServicesProj.AddProjectReference(DataProj);

            //Create DbContext
            var DbContextClass = DataProj.AddClass("ApplicationDbContext",baseClassName:"DbContext");
            DbContextClass.UsingNameSpaces.Add(CoreProj.DefaultNameSpace);
			
			DbContextClass.UsingNameSpaces.Add("Microsoft.EntityFrameworkCore");

            //Create EnitityModels
            foreach (var Entity in _config.Domains.Entites)
            {
                var entityClass = Entity.EnitityGenerate(ref CoreProj);
                var DtoClass = Entity.DtoGenerate(ref CoreProj);
                var entityService = Entity.ServiceGenerate(ref ServicesProj, _config.Domains.RelationShips);
                DbContextClass.AddProperty($"DbSet<{Entity.EntityName}>", Entity.EntityName);           
            }
			
            var relations = _config.Domains.RelationShips;
            var enums = _config.Domains.Enums;
			CoreProj.AddEnums(ref enums);
			CoreProj.Addrelations(ref relations);
			CoreProj.AddDtorelations(ref relations);
			_config.Domains.Entites.FactoriesGenerate(ref ServicesProj,CoreProj);
			_config.Domains.Entites.UnitOfWorkGenerate(ref ServicesProj,CoreProj);

            CoreProj.AddMiddleClassesrelations(relations);
            foreach (var Relation_M2M in _config.Domains.RelationShips.Where(p => p.RelationShipType == "M2M"))
            {
                var middleClassDomain = EntityHandler.CreateMiddleClass(Relation_M2M.Entity1, Relation_M2M.Entity2);
                var MiddleClassEntity = middleClassDomain.Entites.FirstOrDefault();
                var MiddleClass = MiddleClassEntity.ServiceGenerate(ref ServicesProj, middleClassDomain.RelationShips);
            }
			foreach (var EntityNameSpace in CoreProj.CsFiles.OfType<Class>().Where(p => p.BaseClassName == "BaseEntity").Select(p=>p.NameSpace).Distinct())
			{
				DbContextClass.UsingNameSpaces.Add(EntityNameSpace);
			}
			return solution;
        }
      
    }
}
