using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using File = Backender.Translator.Models.File;

namespace Backender.Generator.Templates
{
    public interface ITemplateBase
    {
       public Task<File> OnCreateAsync();
    }
}
