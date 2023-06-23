using Hotelino.Core;
using Hotelino.Data;
using Hotelino.Core.Domains.Rooms;
using Microsoft.EntityFrameworkCore;
namespace Hotelino.Services.Rooms
{
    public interface IRoomClassService
    {
        public IList<RoomClass> GetAllRoomClasses();
        public RoomClass GetRoomClassById(string id);
        public bool InsertRoomClass(RoomClass roomclass);
        public bool UpdateRoomClass(RoomClass roomclass);
        public bool DeleteRoomClass(RoomClass roomclass);
        public bool DeleteRoomClass(string id);
    }
}