namespace JsonPlaceholderImporter.Models
{
    public class Todo
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public bool IsFinished { get; set; }

        public int UserId { get; set; }
        public User? User { get; set; }

    }
}
