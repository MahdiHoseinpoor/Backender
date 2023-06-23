using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Hotelino.Core.Dtos.Administrative;
namespace Hotelino.Core.Dtos.Financial
{
    public class TransactionDto : BaseDto
    {

        public string Name { get; set; }
    
        public DateTime transaction_date { get; set; }
    
        public CustomerDto CustomerDto { get; set; }
            
        public EmployeesDto EmployeesDto { get; set; }
            
        public PaymentDto PaymentDto { get; set; }
            
    }
}