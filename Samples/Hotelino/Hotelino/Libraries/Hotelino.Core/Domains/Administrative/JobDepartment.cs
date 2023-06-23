using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hotelino.Core.Domains.Administrative
{    public class JobDepartment : BaseEntity
    {

        public string title { get; set; } 
    
        public virtual IEnumerable<Employees> Employeeses { get; set; }
            
    }
}