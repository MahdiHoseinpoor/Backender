using Backender.Generator.Steps;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Backender.Generator
{
    /// <summary>
    /// A Fluent API to construct and configure a code generation Engine.
    /// This builder is fully self-contained and has no external dependencies.
    /// </summary>
    public class EngineBuilder
    {
        private readonly List<IGenerationStep> _steps = new();
        private ILogger _logger = NullLogger.Instance;

        /// <summary>
        /// Initializes a new instance of the EngineBuilder.
        /// </summary>
        public EngineBuilder()
        {
        }

        /// <summary>
        /// Provides a logger for the engine and builder to use.
        /// </summary>
        public EngineBuilder WithLogger(ILogger logger)
        {
            _logger = logger ?? NullLogger.Instance;
            return this;
        }

        /// <summary>
        /// Adds a pre-instantiated generation step to the pipeline.
        /// Use this for custom steps that have their own dependencies.
        /// </summary>
        public EngineBuilder AddStep(IGenerationStep step)
        {
            _steps.Add(step);
            return this;
        }

        /// <summary>
        /// Adds a generation step of a specific type that has a parameterless constructor.
        /// </summary>
        public EngineBuilder AddStep<T>() where T : IGenerationStep, new()
        {
            // The logic is now much simpler.
            return AddStep(new T());
        }

        /// <summary>
        /// Removes all steps of a given type from the pipeline.
        /// </summary>
        public EngineBuilder WithoutStep<T>() where T : IGenerationStep
        {
            _steps.RemoveAll(step => step is T);
            return this;
        }

        /// <summary>
        /// CONVENTION: Configures the engine with the standard, recommended pipeline.
        /// </summary>
        public EngineBuilder WithDefaultPipeline()
        {
            _steps.Clear();
            AddStep<EnumGenerationStep>();
            AddStep<EntityGenerationStep>();
            AddStep<DtoGenerationStep>();
            AddStep<DbContextGenerationStep>();
            AddStep<RepoGenerationStep>();
            AddStep<DtoFactoryGenerationStep>();
            AddStep<ServiceGenerationStep>();
            AddStep<UnitOfWorkGenerationStep>();
            AddStep<SolutionFileGenerationStep>();
            AddStep<ProjectFilesGenerationStep>();
            return this;
        }

        /// <summary>
        /// Constructs the final Engine instance with the configured pipeline and logger.
        /// </summary>
        public Engine Build()
        {
            if (!_steps.Any())
            {
                throw new InvalidOperationException("Cannot build an engine with no generation steps. Call WithDefaultPipeline() to add the default steps.");
            }

            var engineLogger = _logger as ILogger<Engine> ?? new Logger<Engine>(NullLoggerFactory.Instance);
            return new Engine(_steps, engineLogger);
        }
    }
}