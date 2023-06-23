using Hotelino.Core;
using Hotelino.Data;
using Hotelino.Core.Domains.Rooms;
using Microsoft.EntityFrameworkCore;
namespace Hotelino.Services.Rooms
{
     public class RoomService : IRoomService
    {
        #region Fields
        public IRepo<Room> _roomRepo;
        #endregion

        #region Ctor
        public RoomService(IRepo<Room> RoomRepo)
        {
            _roomRepo = RoomRepo;
        }
        #endregion

        #region Utilities
        #region Room
        public IList<Room> GetAllRooms()
        {
            return  _roomRepo.GetAll().ToList();
        }
        public Room GetRoomById(string id)
        {
            return  _roomRepo.GetById(id);
        }
        public bool InsertRoom(Room room)
        {
            _roomRepo.Insert(room);
            return _roomRepo.Save();
        }
        public bool UpdateRoom(Room room)
        {
            _roomRepo.Update(room);
            return _roomRepo.Save();
        }
        public bool DeleteRoom(Room room)
        {
            _roomRepo.Delete(room);
            return _roomRepo.Save();
        }
        public bool DeleteRoom(string id)
        {
            _roomRepo.Delete(id);
            return _roomRepo.Save();
        }
        #endregion
        #endregion
    }
}