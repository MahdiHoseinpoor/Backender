using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Hotelino.Core.Domains.Financial;
namespace Hotelino.Core.Domains.Administrative
{    public class Employees : BaseEntity
    {

        [Required]
        [MinLength(0)] [MaxLength(250)]
        public string FirstName { get; set; } 
    
        [Required]
        [MinLength(0)] [MaxLength(250)]
        public string LastName { get; set; } 
    
        public string Address { get; set; } 
    
        [Required]
        [EmailAddress]
        public string Email { get; set; } 
    
        public string username { get; set; } 
    
        public string HashedPasswords { get; set; } 
    
        public virtual IEnumerable<Transaction> Transactions { get; set; }
            
        public string JobDepartmentId { get; set; }
        [ForeignKey("JobDepartmentId")]
        public virtual JobDepartment JobDepartment { get; set; }
            
    }
}