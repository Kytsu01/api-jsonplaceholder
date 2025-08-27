using System.Text.Json.Serialization;

namespace JsonPlaceholderImporter.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;

        public int PostId { get; set; }
        [JsonIgnore]
        public Post? Post { get; set; }
    }
}
