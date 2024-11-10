using HotelAPI.Models;

namespace HotelAPI.Services.HotelData
{
    public interface IHotelDataService
    {
        void AddHotel(Hotel hotel);
        void DeleteHotel(Guid hotelId);
        IEnumerable<Hotel> GetAllHotelsAuthorizedPerson();
    }
}
