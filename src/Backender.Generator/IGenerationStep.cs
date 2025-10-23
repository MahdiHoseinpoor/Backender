using System.Threading.Tasks;

namespace Backender.Generator
{
    public interface IGenerationStep
    {
        Task ExecuteAsync(GenerationContext context);
    }
}