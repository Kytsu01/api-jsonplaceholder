using System.Text.Json.Serialization;

namespace JsonPlaceholderImporter.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Website {  get; set; } = string.Empty;

        public Address Address { get; set; } = new();
        public Company Company { get; set; } = new();

        [JsonIgnore]
        public ICollection<Post> Posts { get; set; } = new List<Post>();
        [JsonIgnore]
        public ICollection<Todo> Todos { get; set; } = new List<Todo>();
        [JsonIgnore]
        public ICollection<Album> Albums { get; set; } = new List<Album>();
    }
}
