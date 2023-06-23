using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hotelino.Core.Dtos
{
    public class BaseDto
    {

        public string Id { get; set; }
    
        public DateTime CreatedAt_ { get; set; }
    
        public DateTime ModifiedAt_ { get; set; }
    
    }
}