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

                    context.Hotel.Remove(hotel);

                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Verilen GUID 'ye ait otel bilgisi bulunamadı");
                }
            }
        }

        public IEnumerable<DtoHotelAuthDetail> GetAllHotelsAuthorizedPersonDetail()
        {

            var hotelList = context.Hotel.ToList();

            List<DtoHotelAuthDetail> responseDto = new List<DtoHotelAuthDetail>();

            foreach (var hotel in hotelList)
            {
                var contacts = context.ContactInformation.Where(contact => contact.HotelUuid == hotel.Uuid).ToList();

                responseDto.Add(new DtoHotelAuthDetail()
                {
                    AuthorizedName = hotel.AuthorizedName,
                    AuthorizedSurname = hotel.AuthorizedSurname,
                    CompanyTitle = hotel.CompanyTitle,
                    ContactInformation = (from y in contacts select new ContactInfoClean { InformationContent = y.InformationContent, InformationType = y.InformationType }).ToList()
                });
            }

            return responseDto;
        }

    }
}
