using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backender.Translator.Models
{
    public class File
    {
        public string Name { get; set; }
        public string Extension { get; set; } = ".cs";
        public string Path { get; set; }
        public string BodyContext { get; set; }
        public List<InnerFileItem> InnerFiles { get; set; } = new List<InnerFileItem>();
        public Dictionary<string, string> Options { get; set; } = new Dictionary<string, string>();
    }
}
