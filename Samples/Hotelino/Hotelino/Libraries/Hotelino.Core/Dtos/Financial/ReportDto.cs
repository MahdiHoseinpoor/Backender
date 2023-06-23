using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hotelino.Core.Dtos.Financial
{
    public class ReportDto : BaseDto
    {

        public string information { get; set; }
    
        public DateTime date { get; set; }
    
        public TransactionDto TransactionDto { get; set; }
            
    }
}