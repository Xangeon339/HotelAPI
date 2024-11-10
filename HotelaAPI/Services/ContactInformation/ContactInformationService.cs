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
                var hotel = context.Hotel.FirstOrDefault(predicate: hotel => hotel.Uuid == contactInformation.Hotel.Uuid);

                if (hotel == null) 
                {
                    throw new Exception("İletişim bilgisine bağlı otel bulunamadı");
                }

                context.ContactInformation.Add(contactInformation);

                context.SaveChanges();

            }
        }

        public void DeleteContactInformation(Guid contactId)
        {
            using (context)
            {
                var contact = context.ContactInformation.FirstOrDefault(contact => contact.Uuid == contactId);

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
