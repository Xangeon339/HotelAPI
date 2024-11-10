using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelAPI.Models
{
    public class ContactInformation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Uuid { get; set; }
        public EnmInformationType InformationType { get; set; }
        public string? InformationContent { get; set; }
        [ForeignKey("Hotel")]
        public Guid HotelUuid { get; set; }
    }
}
