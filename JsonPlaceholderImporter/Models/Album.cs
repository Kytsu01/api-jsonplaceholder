namespace JsonPlaceholderImporter.Models
{
    public class Album
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;

        public int UserId { get; set; }
        public User? User { get; set; }

        public ICollection<Photo> Photos { get; set; } = new List<Photo>();
    }
}
