using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hotelino.Core.Dtos.Rooms
{
    public class RoomDto : BaseDto
    {

        public string Description { get; set; }
    
        public float Price { get; set; }
    
        public RoomClassDto RoomClassDto { get; set; }
            
    }
}