using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelAPI.Models
{
    public class Hotel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]       
        public Guid Uuid { get; set; }
        public string? CompanyTitle { get; set; }
        public string? AuthorizedName { get; set; }
        public string? AuthorizedSurname { get; set; }
        public virtual IEnumerable<ContactInformation>? ContactInformation { get; set; }
    }
}
