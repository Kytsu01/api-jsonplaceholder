using System.Text.Json.Serialization;

namespace JsonPlaceholderImporter.Models
{
    public class Todo
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public bool Completed { get; set; }

        public int UserId { get; set; }
        [JsonIgnore]
        public User? User { get; set; }

    }
}
