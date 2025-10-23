using Backender.Generator;
using Backender.Translator;
using Backender.Translator.Handlers.ValidationControls;
using Backender.Translator.Handlers;
namespace Backender.Fluent
{
    public class BlueprintBuilder
    {
        private readonly Blueprint _blueprint = new();

        internal BlueprintBuilder(string solutionName, string rootNamespace, string savePath)
        {
            _blueprint.SavePath = savePath;
            _blueprint.Solution = new Solution_
            {
                SolutionName = solutionName,
                SolutionNamespace = rootNamespace,
                UseDefaultStructure = true // Default to the simple structure
            };
            _blueprint.Domains = new Domains();
        }

        /// <summary>
        /// Defines the domain models, including entities, enums, and their relationships.
        /// </summary>
        public BlueprintBuilder WithDomains(Action<DomainsBuilder> configure)
        {
            var domainsBuilder = new DomainsBuilder(_blueprint.Domains);
            configure(domainsBuilder);
            return this;
        }

        /// <summary>
        /// Sets the validation library to be used (e.g., DataAnnotation or FluentValidation).
        /// </summary>
        public BlueprintBuilder UseValidation(ValidationControl validation)
        {
            _blueprint.ValidationControl = validation.ToString();
            return this;
        }

        /// <summary>
        /// Builds the blueprint, runs the generator engine, and creates the project files.
        /// This is the final step in the chain.
        /// </summary>
        public async Task GenerateAsync()
        {
            BlueprintCompiler.Configure();
            var configuredBlueprint = _blueprint.Configuration();
            configuredBlueprint.Validate();

            var errorMessages = BlueprintCompiler.Messages
                .Where(m => m.MessageType == MessageType.Error)
                .ToList();

            if (errorMessages.Any())
            {
                var errors = string.Join("\n", errorMessages.Select(e => $"[{e.Code}]: {e.Description}"));
                throw new InvalidOperationException($"Blueprint validation failed:\n{errors}");
            }

            var engine = new EngineBuilder().WithDefaultPipeline().Build();
            await engine.RunAsync(configuredBlueprint);
        }
    }
}