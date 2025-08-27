namespace JsonPlaceholderImporter.Dtos
{
    public class PhotoCreateDto
    {
        public int AlbumId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
        public string ThumbnailUrl { get; set; } = string.Empty;
    }
}
