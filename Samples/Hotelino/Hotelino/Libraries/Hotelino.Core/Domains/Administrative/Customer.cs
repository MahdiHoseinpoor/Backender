using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Hotelino.Core.Domains.Financial;
namespace Hotelino.Core.Domains.Administrative
{    public class Customer : BaseEntity
    {

        [Required]
        [MinLength(0)] [MaxLength(250)]
        public string FirstName { get; set; } 
    
        [Required]
        [MinLength(0)] [MaxLength(250)]
        public string LastName { get; set; } 
    
        [Required]
        [MinLength(0)] [MaxLength(250)]
        public string Address { get; set; } 
    
        [Required]
        [MinLength(0)] [MaxLength(250)]
        [EmailAddress]
        public string Email { get; set; } 
    
        public virtual IEnumerable<Transaction> Transactions { get; set; }
            
    }
}