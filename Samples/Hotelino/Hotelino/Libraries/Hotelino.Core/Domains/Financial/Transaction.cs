using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Hotelino.Core.Domains.Administrative;
namespace Hotelino.Core.Domains.Financial
{    public class Transaction : BaseEntity
    {

        [Required]
        [MinLength(0)] [MaxLength(250)]
        public string Name { get; set; } 
    
        public DateTime transaction_date { get; set; } 
    
        public string CustomerId { get; set; }
        [ForeignKey("CustomerId")]
        public virtual Customer Customer { get; set; }
            
        public string EmployeesId { get; set; }
        [ForeignKey("EmployeesId")]
        public virtual Employees Employees { get; set; }
            
        public string PaymentId { get; set; }
        [ForeignKey("PaymentId")]
        public virtual Payment Payment { get; set; }
            
        public virtual IEnumerable<Report> Reports { get; set; }
            
    }
}