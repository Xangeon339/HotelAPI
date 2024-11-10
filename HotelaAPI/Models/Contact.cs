namespace HotelAPI.Models
{
    public class Contact
    {
        public Guid Uuid { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public virtual Hotel Hotel { get; set; }
        public virtual ContactInformation ContactInformation { get; set; }
    }
}
