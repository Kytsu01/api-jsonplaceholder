using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace JsonPlaceholderImporter.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;

        public int UserId { get; set; }
        public User? User { get; set; }


        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    }
}
