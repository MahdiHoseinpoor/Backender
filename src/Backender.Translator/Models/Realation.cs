namespace Backender.Translator.Models
{
    public class Relation
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string FromTableId { get; set; }
        public string ToTableId { get; set; }
        public string TableName { get; set; }
        public RelationType RelationType { get; set; }
    }
    public enum RelationType
    {
        ToMany,
        ToOne
    }
}