using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace HotelAPI.Models
{
    public class Report
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Uuid { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
        public int RegisteredOtelCount { get; set; }
        public int RegisteredPhoneCount { get; set; }
        public DateTime DateRequested { get; set; }
        public EnmStatusType Status { get; set; }

    }
}
