using Hotelino.Core;
using Hotelino.Data;
using Hotelino.Core.Domains.Rooms;
using Microsoft.EntityFrameworkCore;
namespace Hotelino.Services.Rooms
{
     public class RoomClassService : IRoomClassService
    {
        #region Fields
        public IRepo<RoomClass> _roomClassRepo;
        #endregion

        #region Ctor
        public RoomClassService(IRepo<RoomClass> RoomClassRepo)
        {
            _roomClassRepo = RoomClassRepo;
        }
        #endregion

        #region Utilities
        #region RoomClass
        public IList<RoomClass> GetAllRoomClasses()
        {
            return  _roomClassRepo.GetAll().ToList();
        }
        public RoomClass GetRoomClassById(string id)
        {
            return  _roomClassRepo.GetById(id);
        }
        public bool InsertRoomClass(RoomClass roomclass)
        {
            _roomClassRepo.Insert(roomclass);
            return _roomClassRepo.Save();
        }
        public bool UpdateRoomClass(RoomClass roomclass)
        {
            _roomClassRepo.Update(roomclass);
            return _roomClassRepo.Save();
        }
        public bool DeleteRoomClass(RoomClass roomclass)
        {
            _roomClassRepo.Delete(roomclass);
            return _roomClassRepo.Save();
        }
        public bool DeleteRoomClass(string id)
        {
            _roomClassRepo.Delete(id);
            return _roomClassRepo.Save();
        }
        #endregion
        #endregion
    }
}