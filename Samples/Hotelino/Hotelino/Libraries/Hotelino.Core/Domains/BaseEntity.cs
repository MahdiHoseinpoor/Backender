using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hotelino.Core.Domains
{    public class BaseEntity 
    {

        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();
    
        public DateTime CreatedAt_ { get; set; } 
    
        public DateTime ModifiedAt_ { get; set; } 
    
    }
}