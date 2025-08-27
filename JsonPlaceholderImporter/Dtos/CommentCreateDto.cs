namespace JsonPlaceholderImporter.Dtos
{
    public class CommentCreateDto
    {
        public int postId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Body {  get; set; } = string.Empty;
    }
}
