using HotelAPI.Database;
using HotelAPI.Models;
using Newtonsoft.Json;

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
                bool hotel = context.Hotel.Any(hotel => hotel.Uuid == contactInformation.HotelUuid);

                if (hotel)
                {
                    if(contactInformation.InformationType == EnmInformationType.Location)
                    {
                        Location loc = JsonConvert.DeserializeObject<Location>(contactInformation.InformationContent);

                        if(loc == null)
                        {
                            throw new Exception("Location bilgisi Location classının json çıktısı olarak gönderilmelidir");
                        }
                    }

                    contactInformation.Uuid = Guid.NewGuid();

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
                    throw new Exception("Verilen GUID ye ait bir iletişim bilgisi bulunamadı");
                }
            }
        }
    }
}
