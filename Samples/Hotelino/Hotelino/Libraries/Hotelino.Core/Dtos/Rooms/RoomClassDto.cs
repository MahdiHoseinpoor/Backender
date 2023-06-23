using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hotelino.Core.Dtos.Rooms
{
    public class RoomClassDto : BaseDto
    {

        public string Name { get; set; }
    
    }
}