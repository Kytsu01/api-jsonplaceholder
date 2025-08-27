using System.Text.Json.Serialization;

namespace JsonPlaceholderImporter.Models
{
    public class Address
    {
        public int Id { get; set; }
        public string Street { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string ZipCode { get; set; } = string.Empty;

        [JsonIgnore]
        public Geolocation Geo { get; set; } = new();
    }
}
