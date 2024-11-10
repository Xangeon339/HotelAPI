namespace HotelAPI.Services.ContactInformation
{
    public interface IContactInformationService
    {
        void AddContactInformation(HotelAPI.Models.ContactInformation contactInformation);
        void DeleteContactInformation(Guid contactInformationId);
    }
}
