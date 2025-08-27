using System.Text.Json.Serialization;

namespace JsonPlaceholderImporter.Models
{
    public class Photo
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
        public string ThumbnailUrl { get; set; } = string.Empty;

        public int AlbumId { get; set; }
        [JsonIgnore]
        public Album? Album { get; set; }
    }
}
