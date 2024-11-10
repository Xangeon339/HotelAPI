using HotelAPI.Database;
using HotelAPI.Models;
using Microsoft.EntityFrameworkCore.Metadata;

namespace HotelAPI.Services.HotelData
{
    public class HotelDataService : IHotelDataService
    {
        private readonly DatabaseContext context;

        public HotelDataService(DatabaseContext context)
        {
            this.context = context;
        }

        public void AddHotel(Hotel hotel)
        {
                using (context)
                {

                    hotel.Uuid = Guid.NewGuid();

                    context.Hotel.Add(hotel);

                    context.SaveChanges();
  
                }
        }

        public void DeleteHotel(Guid hotelId)
        {

            using (context)
            {
                var hotel = context.Hotel.FirstOrDefault(hotel => hotel.Uuid == hotelId);

                if (hotel != null)
                {
                   /* var contacts = context.ContactInformation.Where(contact => contact.hotel)
                    if ()
                    {

                    }*/

                    context.Hotel.Remove(hotel);

                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Silme işlemi yapılamadı");
                }
            }
        }

        public IEnumerable<Hotel> GetAllHotelsAuthorizedPerson()
        {
            return context.Hotel.ToList();
        }

    }
}
