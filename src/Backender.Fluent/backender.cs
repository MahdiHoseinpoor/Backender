namespace Backender.Fluent
{
    /// <summary>
    /// The main entry point for defining and generating a Backender solution.
    /// </summary>
    public static class backender
    {
        /// <summary>
        /// Begins the definition of a new Backender blueprint.
        /// </summary>
        /// <param name="solutionName">The name of the solution (e.g., "MyProject").</param>
        /// <param name="rootNamespace">The root namespace for the solution (e.g., "MyProject.Api").</param>
        /// <param name="savePath">The directory where the project will be generated.</param>
        public static BlueprintBuilder Define(string solutionName, string rootNamespace, string savePath)
        {
            return new BlueprintBuilder(solutionName, rootNamespace, savePath);
        }
    }
}