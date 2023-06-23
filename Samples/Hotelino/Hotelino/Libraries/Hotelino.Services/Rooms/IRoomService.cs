using Hotelino.Core;
using Hotelino.Data;
using Hotelino.Core.Domains.Rooms;
using Microsoft.EntityFrameworkCore;
namespace Hotelino.Services.Rooms
{
    public interface IRoomService
    {
        public IList<Room> GetAllRooms();
        public Room GetRoomById(string id);
        public bool InsertRoom(Room room);
        public bool UpdateRoom(Room room);
        public bool DeleteRoom(Room room);
        public bool DeleteRoom(string id);
    }
}