using Hotelino.Core.Domains.Rooms;
using Hotelino.Core.Dtos.Rooms;
using Hotelino.Services.Administrative;
using Hotelino.Services.Financial;
using Hotelino.Services.Blog;


namespace Hotelino.Services.Rooms
{
    public class RoomsDtosFactory
    {
        #region PrepareMethods

            #region RoomClass 

        public RoomClassDto PrepareRoomClassDto(RoomClass roomclass)
        {
            var roomclassDto = new RoomClassDto()
            {

                Name = roomclass.Name
            };
            
            return roomclassDto;
        }
        public List<RoomClassDto> PrepareRoomClassDto(List<RoomClass> RoomClasses)
        {
            var roomclassDtos = new List<RoomClassDto>();
            foreach (var roomclass in RoomClasses)
            {
                    roomclassDtos.Add(PrepareRoomClassDto(roomclass));
            }
            return roomclassDtos;
        }
            

           #endregion

            #region Room 

        public RoomDto PrepareRoomDto(Room room)
        {
            var roomDto = new RoomDto()
            {

                Description = room.Description,

                Price = room.Price
            };

                        roomDto.RoomClassDto = PrepareRoomClassDto(_roomClassService.GetRoomClassById(room.RoomClassId));
                                
            return roomDto;
        }
        public List<RoomDto> PrepareRoomDto(List<Room> Rooms)
        {
            var roomDtos = new List<RoomDto>();
            foreach (var room in Rooms)
            {
                    roomDtos.Add(PrepareRoomDto(room));
            }
            return roomDtos;
        }
            

           #endregion
        #endregion
        #region fields
        public RoomClassService _roomClassService;

        #endregion

        #region ctor

        public RoomsDtosFactory(RoomClassService RoomClassService)
        {
        _roomClassService = RoomClassService;
        }
          
        #endregion
        
    }
}