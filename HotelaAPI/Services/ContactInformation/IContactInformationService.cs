namespace HotelAPI.Services.ContactInformation
{
    public interface IContactInformationService
    {
        Guid AddContactInformation(HotelAPI.Models.ContactInformation contactInformation);
        void DeleteContactInformation(Guid contactInformationId);
    }
}
