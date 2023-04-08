using Backender.CodeEditor.CSharp;
using Backender.CodeEditor.CSharp.Objects;
using Backender.Translator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backender.CodeGenerator.Patterns.Repo
{
	public static class UnitOfWorkHandler
	{
		public static Class UnitOfWorkGenerate(this List<Entity> entities, ref Project proj, List<string> options = null)
		{
			var unitofwork = proj.AddClass("UnitOfWork" , baseClassName: "IDisposable", Options: options);
			unitofwork.AddServices(proj);
			throw new NotImplementedException();
		}
		private static void AddServices(this Class _class, Project proj)
		{
			foreach (var ServiceClass in proj.CsFiles.OfType<Class>().Where(p => p.Name.EndsWith("Service")))
			{
				var serviceFieldName = $"_{ServiceClass.Name.ToLower()}";
				_class.AddField(ServiceClass.Name, serviceFieldName, false, AccessModifier.Private);
				var ServicesParameters = ServiceClass.InnerItems.OfType<Constructor>().Select(p => p.Parameters);
				var ServiceParametersName = new List<string>();
				foreach (var ServiceParameters in ServicesParameters)
				{
					foreach (var ServiceParameter in ServiceParameters)
					{
						ServiceParametersName.Add(ServiceParameter.Name);
					}
				}
				string GetInnerCode = $"if ({serviceFieldName} == null)\n" +
					"{\n" +
					$"{serviceFieldName} = new {ServiceClass.Name}({string.Join(',', ServiceParametersName)});\n" +
					"}\n" +
					$"return {serviceFieldName};";
				_class.AddProperty(ServiceClass.Name, ServiceClass.Name, getInnerCode: GetInnerCode);
			}
		}
		private static void AddRepos(this Class _class, List<Entity> entities,Field dbcontext)
		{
			foreach (var entity in entities)
			{
				var repoFieldName = $"_{entity.EntityName.ToLower()}";
				_class.AddField($"Repo<{entity.EntityName}>", repoFieldName, false, AccessModifier.Private);
	
				string GetInnerCode = $"if ({repoFieldName} == null)\n" +
					"{\n" +
					$"{repoFieldName} = new {entity.EntityName}({dbcontext.Name});\n" +
					"}\n" +
					$"return {repoFieldName};";
				_class.AddProperty($"Repo<{entity.EntityName}>", entity.EntityName, getInnerCode: GetInnerCode);
			}
		}

	}
}
