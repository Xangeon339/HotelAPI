namespace HotelAPI.Models
{
    public class DtoHotelAuthDetail
    {
        public string? CompanyTitle { get; set; }
        public string? AuthorizedName { get; set; }
        public string? AuthorizedSurname { get; set; }
        public IEnumerable<ContactInfoClean>? ContactInformation { get; set; }
    }

    public class ContactInfoClean
    {
        public EnmInformationType InformationType { get; set; }
        public string? InformationContent { get; set; }
    }
}
