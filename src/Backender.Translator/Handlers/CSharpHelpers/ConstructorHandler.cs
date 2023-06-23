using Backender.Translator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backender.Translator.Handlers.CSharpHelpers
{
    public static class ConstructorHandler
    {
        public static Constructor CreateConstructor()
        {
            return new Constructor();
        }
        public static Constructor AddParameterToConstructor(this Constructor constructor,string dataType,string name)
        {
            var parameter = new Parameter()
            {
                Name = name,
                DataType = dataType
            };
            constructor.Parameters.Add(parameter);
            return constructor;
        }
    }
}
