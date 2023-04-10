using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backender.Translator
{
    public class Col
    {
        public string ColName { get; set; }
        public string ColType { get; set; }
		public string Options { get; set; } = string.Empty;
    }

    public class Domains
    {
		public List<Enum_> Enums { get; set; } = new List<Enum_>();
		public List<Entity> Entites { get; set; } = new List<Entity>();
        public List<RealationShip> RealationShips { get; set; } = new List<RealationShip>();
    }
	public class Enum_
	{
		public string EnumName { get; set; }
		public List<EnumValue_> EnumValues { get; set; } = new List<EnumValue_>();
	}
	public class EnumValue_
	{
		public string Name { get; set; }	
		public int Value { get; set; }	
	}

	public class Entity
    {
        public string EntityName { get; set; }
        public string EntityCategory { get; set; }
        public List<Col> Cols { get; set; } = new List<Col>();
	}

    public class RealationShip
    {
        public string Entity1 { get; set; }
        public string Entity2 { get; set; }
        public string RealationShipType { get; set; }
    }

    public class Config
    {
		public string Version { get; set; } = "net7.0";
		public string SavePath { get; set; }
        public string SolutionName { get; set; }
		public string SoltionNameSpace { get; set; }

		public Domains Domains { get; set; }
    }
}
