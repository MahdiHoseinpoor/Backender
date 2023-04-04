using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backender.CodeEditor.CSharp.Objects
{
    public class Solution
    {
        public string Name { get; set; }
        public string SavePath { get; set; }
        public List<Project> Projects { get; set; } = new List<Project>();

        public Solution(string name)
        {
            Name = name;
        }
        public Solution()
        {

        }

    }
}
