using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace HotelAPI.Models
{
    //[Keyless]
    public class ContactInformation
    {
        [Key]
        public Guid Uuid { get; set; }
        public EnmInformationType InformationType { get; set; }
        public string? InformationContent { get; set; }
    }
}
