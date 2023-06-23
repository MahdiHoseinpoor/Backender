using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hotelino.Core.Domains.Financial
{    public class Payment : BaseEntity
    {

        public DateTime Payments_date { get; set; } 
    
        public virtual IEnumerable<Transaction> Transactions { get; set; }
            
    }
}