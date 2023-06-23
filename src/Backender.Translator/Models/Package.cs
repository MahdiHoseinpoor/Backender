namespace Backender.Translator.Models
{
    public class Package
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }
        public string Version { get; set; }
    }
}