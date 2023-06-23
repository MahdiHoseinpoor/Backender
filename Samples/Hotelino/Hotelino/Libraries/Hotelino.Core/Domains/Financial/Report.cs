using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hotelino.Core.Domains.Financial
{    public class Report : BaseEntity
    {

        public string information { get; set; } 
    
        public DateTime date { get; set; } 
    
        public string TransactionId { get; set; }
        [ForeignKey("TransactionId")]
        public virtual Transaction Transaction { get; set; }
            
    }
}