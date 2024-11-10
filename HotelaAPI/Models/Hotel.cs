using System.ComponentModel.DataAnnotations;

namespace HotelAPI.Models
{
    public class Hotel
    {
        [Key]
        public Guid Uuid { get; set; }
        public string CompanyTitle { get; set; }
        public string AuthorizedName { get; set; }
        public string AuthorizedSurname { get; set; }

        public virtual ContactInformation? ContactInformation { get; set; }
    }
}
