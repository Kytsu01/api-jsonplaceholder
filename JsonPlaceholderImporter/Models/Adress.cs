namespace JsonPlaceholderImporter.Models
{
    public class Adress
    {
        public int Id { get; set; }
        public string Street { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string ZipCode { get; set; } = string.Empty;

        public Geolocation Geo { get; set; } = new();
    }
}
