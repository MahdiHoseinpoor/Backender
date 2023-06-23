using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hotelino.Core.Domains.Rooms
{    public class RoomClass : BaseEntity
    {

        [Required]
        [MinLength(0)] [MaxLength(250)]
        public string Name { get; set; } 
    
        public virtual IEnumerable<Room> Rooms { get; set; }
            
    }
}