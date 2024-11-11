using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;

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
        public Point Location { get; set; }
        public string? Address { get; set; }
        public virtual IEnumerable<ContactInformation>? ContactInformation { get; set; }
    }
}
