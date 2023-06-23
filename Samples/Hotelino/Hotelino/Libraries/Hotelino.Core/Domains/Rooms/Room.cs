using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hotelino.Core.Domains.Rooms
{    public class Room : BaseEntity
    {

        public string Description { get; set; } 
    
        public float Price { get; set; } 
    
        public string RoomClassId { get; set; }
        [ForeignKey("RoomClassId")]
        public virtual RoomClass RoomClass { get; set; }
            
    }
}