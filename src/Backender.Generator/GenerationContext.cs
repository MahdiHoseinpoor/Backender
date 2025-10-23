// In Backender.Generator/GenerationContext.cs
using Backender.Core.Models;
using Backender.Translator;
using Backender.Translator.Handlers;
using System.Linq;
using File = Backender.Core.Models.File;

namespace Backender.Generator
{
    public class GenerationContext
    {
        public Blueprint Blueprint { get; }
        public Solution Solution { get; }
        public List<Table> Tables { get; }
        public IList<File> GeneratedFiles { get; } = new List<File>();

        // The constructor is now completely dynamic.
        public GenerationContext(Blueprint blueprint)
        {
            Blueprint = blueprint;
            // It delegates all construction logic to the handler.
            Solution = SolutionHandler.CreateFromBlueprint(blueprint);
            Tables = TableHandler.CreateTables(blueprint);
        }

        // This is the new, dynamic way for steps to find the correct project.
        public Project GetProjectByRole(string role)
        {
            var project = Solution.Projects.FirstOrDefault(p => p.Role.Equals(role, StringComparison.OrdinalIgnoreCase));

            if (project == null)
            {
                // Or throw a more specific exception
                throw new InvalidOperationException($"Blueprint does not define a project with the role '{role}'.");
            }

            return project;
        }
    }
}