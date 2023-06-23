using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hotelino.Core.Dtos.Financial
{
    public class PaymentDto : BaseDto
    {

        public DateTime Payments_date { get; set; }
    
    }
}