using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet;
using YamlDotNet.Serialization;
namespace Backender.Translator
{
	public static class YamlSerializer
	{
	    static IDeserializer _deserializer;
		public static IDeserializer Deserializer { 
			get {
				if (_deserializer == null) {
					_deserializer = new DeserializerBuilder().Build();
				}
				return _deserializer; 
			}
		}
		public static TObject Deserialize<TObject>(string yaml)
		{
			return Deserializer.Deserialize<TObject>(yaml);
		}
	}
}
