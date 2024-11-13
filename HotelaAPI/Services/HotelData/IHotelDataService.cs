using HotelAPI.Models;

namespace HotelAPI.Services.HotelData
{
    public interface IHotelDataService
    {
        Guid AddHotel(Hotel hotel);
        void DeleteHotel(Guid hotelId);
        IEnumerable<DtoHotelAuthDetail> GetAllHotelsAuthorizedPersonDetail();
    }
}
