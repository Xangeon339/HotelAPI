using HotelAPI.Database;
using HotelAPI.Models;

namespace HotelAPI.Services.ContactInformation
{
    public class ContactInformationService : IContactInformationService
    {
        private readonly DatabaseContext context;

        public ContactInformationService(DatabaseContext context)
        {
            this.context = context;
        }

        public void AddContactInformation(HotelAPI.Models.ContactInformation contactInformation)
        {
            using (context)
            {
                var hotel = context.Hotel.FirstOrDefault(hotel => hotel.ContactInformation.Uuid == contactInformation.Uuid);

                if (hotel != null)
                {
                    context.ContactInformation.Add(contactInformation);

                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("İletişim bilgisine bağlı otel bulunamadı");
                }

            }
        }

        public void DeleteContactInformation(Guid contactInformationId)
        {
            using (context)
            {
                var contact = context.ContactInformation.FirstOrDefault(contact => contact.Uuid == contactInformationId);

                if (contact != null)
                {
                    context.ContactInformation.Remove(contact);

                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Silme işlemi yapılamadı");
                }
            }
        }
    }
}
