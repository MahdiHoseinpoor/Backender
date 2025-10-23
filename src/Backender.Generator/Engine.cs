using Backender.Translator;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using File = Backender.Core.Models.File;

namespace Backender.Generator
{
    /// <summary>
    /// The primary orchestrator for the code generation process.
    /// This engine is responsible for executing a series of generation steps
    /// and writing the resulting files to disk. It is completely decoupled
    /// from the specific steps it runs, which are provided via dependency injection.
    /// </summary>
    public class Engine
    {
        private readonly IEnumerable<IGenerationStep> _generationSteps;
        private readonly ILogger<Engine> _logger;

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the Engine.
        /// </summary>
        /// <param name="generationSteps">An ordered collection of generation steps to be executed.</param>
        /// <param name="logger">A logger for providing feedback during the generation process.</param>
        public Engine(IEnumerable<IGenerationStep> generationSteps, ILogger<Engine> logger)
        {
            _generationSteps = generationSteps;
            _logger = logger;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Executes the entire code generation pipeline based on the provided blueprint.
        /// </summary>
        /// <param name="blueprint">The blueprint object that defines what to generate.</param>
        public async Task RunAsync(Blueprint blueprint)
        {
            _logger.LogInformation("Code generation process started for solution: {SolutionName}", blueprint.Solution.SolutionName);

            var context = new GenerationContext(blueprint);
            await ExecuteGenerationStepsAsync(context);
            string savePath = DetermineSavePath(blueprint);
            await WriteFilesToDiskAsync(context.GeneratedFiles, savePath);

            _logger.LogInformation("Successfully generated {FileCount} files at: {SavePath}", context.GeneratedFiles.Count, savePath);
            _logger.LogInformation("Code generation process finished successfully.");
        }
        #endregion

        #region Private Helpers
        /// <summary>
        /// Iterates through the injected generation steps and executes them sequentially.
        /// </summary>
        private async Task ExecuteGenerationStepsAsync(GenerationContext context)
        {
            _logger.LogInformation("Executing {StepCount} generation steps...", _generationSteps.Count());

            foreach (var step in _generationSteps)
            {
                var stepName = step.GetType().Name;
                _logger.LogDebug("Executing step: {StepName}", stepName);

                try
                {
                    await step.ExecuteAsync(context);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred during the '{StepName}' step.", stepName);
                    throw;
                }
            }
            _logger.LogInformation("All generation steps completed successfully.");
        }

        /// <summary>
        /// Determines the root directory where the solution will be saved.
        /// Uses the path from the blueprint if provided, otherwise defaults to a standard location.
        /// </summary>
        private string DetermineSavePath(Blueprint blueprint)
        {
            if (!string.IsNullOrEmpty(blueprint.SavePath))
            {
                _logger.LogDebug("Using custom save path from blueprint: {SavePath}", blueprint.SavePath);
                return blueprint.SavePath;
            }

            var defaultPath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                "Backender 2026",
                "Sources",
                blueprint.Solution.SolutionName
            );

            _logger.LogDebug("No custom save path provided. Using default: {DefaultPath}", defaultPath);
            return defaultPath;
        }

        /// <summary>
        /// Writes a collection of File objects to the specified base path on the disk.
        /// </summary>
        private async Task WriteFilesToDiskAsync(IEnumerable<File> files, string basePath)
        {
            _logger.LogInformation("Writing {FileCount} files to disk...", files.Count());

            foreach (var file in files)
            {
                try
                {
                    string fullDirectoryPath = Path.Combine(basePath, file.Path);
                    string fullFilePath = Path.Combine(fullDirectoryPath, file.Name + file.Extension);
                    Directory.CreateDirectory(fullDirectoryPath);

                    _logger.LogDebug("Writing file: {FilePath}", fullFilePath);
                    await System.IO.File.WriteAllTextAsync(fullFilePath, file.BodyContext);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Failed to write file: {FileName}", file.Name + file.Extension);
                    throw;
                }
            }
        }
        #endregion
    }
}