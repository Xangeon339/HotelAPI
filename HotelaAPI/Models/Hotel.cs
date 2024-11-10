using System.ComponentModel.DataAnnotations;

namespace HotelAPI.Models
{
    public class Hotel
    {
        [Key]
        public Guid Uuid { get; set; }
        public string CompanyTitle { get; set; }

    }
}
