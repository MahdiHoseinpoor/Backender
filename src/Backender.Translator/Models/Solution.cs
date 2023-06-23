using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backender.Translator.Models
{
    public class Solution
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }
        public string NameSpace { get; set; }
        public List<Project> Projects { get; set; } = new List<Project>();
    }
}
