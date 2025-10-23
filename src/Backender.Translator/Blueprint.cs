using System.Xml.Serialization;

namespace Backender.Translator
{
    public class BlueprintTag
    {
        [XmlAttribute("Id")]
        public string Id { get; set; }
    }
    public class Col : BlueprintTag
    {
        [XmlAttribute("Name")]
        public string ColName { get; set; }

        [XmlAttribute("Type")]
        public string ColType { get; set; }

        [XmlAttribute("Options")]
        public string Options { get; set; } = string.Empty;
    }

    public class Domains : BlueprintTag
    {
        [XmlElement("Enum")]
        public List<Enum_> Enums { get; set; } = new List<Enum_>();
        [XmlElement("Entity")]

        public List<Entity> Entities { get; set; } = new List<Entity>();
        [XmlElement("RelationShip")]

        public List<RelationShip> RelationShips { get; set; } = new List<RelationShip>();
        [XmlElement("GlobalOption")]

        public List<GlobalOption> GlobalOptions { get; set; } = new List<GlobalOption>();
      

    }

    public class Enum_ : BlueprintTag
    {
        [XmlAttribute("Name")]
        public string EnumName { get; set; }

        [XmlElement("EnumValue")]
        public List<EnumValue_> EnumValues { get; set; } = new List<EnumValue_>();
    }

    public class EnumValue_ : BlueprintTag
    {
        [XmlAttribute("Name")]
        public string Name { get; set; }

        [XmlAttribute("Value")]
        public int Value { get; set; }
    }
    public class Entity : BlueprintTag
    {
        [XmlAttribute("Name")]
        public string EntityName { get; set; }

        [XmlAttribute("Category")]
        public string EntityCategory { get; set; }

        [XmlElement("Col")]
        public List<Col> Cols { get; set; } = new List<Col>();

        [XmlAttribute("Options")]
        public string Options { get; set; } = string.Empty;

        [XmlIgnore]
        public Dictionary<string, string> DataBag { get; set; } = new Dictionary<string, string>();

    }

    public class RelationShip : BlueprintTag
    {
        [XmlAttribute("Entity1")]
        public string Entity1 { get; set; }

        [XmlAttribute("Entity2")]
        public string Entity2 { get; set; }

        [XmlAttribute("Type")]
        public string RelationShipType { get; set; }
    }

    public class GlobalOption : BlueprintTag
    {
        [XmlAttribute("EntityCols")]
        public string EntityCols { get; set; }

        [XmlAttribute("Options")]
        public string Options { get; set; }
    }

    public class Reference : BlueprintTag
    {
        [XmlAttribute("Type")]
        public string Type { get; set; }

        [XmlAttribute("Href")]
        public string Href { get; set; }
    }

    [XmlRoot("Blueprint")]
    public class Blueprint : BlueprintTag
    {
        [XmlElement("Solution")]
        public Solution_ Solution { get; set; }

        [XmlAttribute("SavePath")]
        public string SavePath { get; set; }

        [XmlAttribute("ValidationControl")]
        public string ValidationControl { get; set; } = "data annotation";


        [XmlElement("Reference")]
        public List<Reference> References { get; set; } = new List<Reference>();

        [XmlElement("Domains")]
        public Domains Domains { get; set; }
    }

    public class Solution_ : BlueprintTag
    {
        [XmlAttribute("Name")]
        public string SolutionName { get; set; }

        [XmlAttribute("Namespace")]
        public string SolutionNamespace { get; set; }

        [XmlAttribute("UseDefaultStructure")]
        public bool UseDefaultStructure { get; set; }

        [XmlElement("Projects")]
        public List<Project_> Projects { get; set; }
    }
    public class Project_ : BlueprintTag
    {
        [XmlAttribute("Name")]
        public string Name { get; set; }
        [XmlAttribute("Role")]
        public string Role { get; set; }
        [XmlAttribute("Path")]
        public string Path { get; set; }
        [XmlAttribute("Sdk")]
        public string Sdk { get; set; }
        [XmlElement("ProjectReferences")]
        public List<ProjectReference_> ProjectReferences { get; set; }
        [XmlElement("PackageReferences")]
        public List<PackageReference_> PackageReferences { get; set; }
    }
    public class ProjectReference_ : BlueprintTag
    {
        [XmlAttribute("Include")]
        public string Include { get; set; }
    }
    public class PackageReference_ : BlueprintTag
    {
        [XmlAttribute("Include")]
        public string Include { get; set; }

        [XmlAttribute("Version")]
        public string Version { get; set; }
    }

}