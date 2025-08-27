using JsonPlaceholderImporter.Context;
using JsonPlaceholderImporter.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System.Text.Json;

namespace JsonPlaceholderImporter.Controllers.Imports
{
    [Route("api/import/photos")]
    [ApiController]
    public class PhotosImportController : ControllerBase
    {

        public AppDbContext _context;

        public PhotosImportController(AppDbContext context)
        { 
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Import()
        {

            const string url = "https://jsonplaceholder.typicode.com/photos";

            using var http = new HttpClient();
            var json = await http.GetStringAsync(url);

            var photos = JsonSerializer.Deserialize<List<ApiPhotos>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            int added = 0;

            foreach(var photo in photos)
            {
                var entity = new Photo
                {
                    AlbumId = photo.AlbumId,
                    Title = photo.Title ?? string.Empty,
                    Url = photo.Url ?? string.Empty,
                    ThumbnailUrl = photo.ThumbnailUrl ?? string.Empty
                };

                _context.Photos.Add(entity);
                added++;

            }

            await _context.SaveChangesAsync();
            return Ok(new { added });
        }

        internal class ApiPhotos 
        {
            public int Id { get; set; }
            public int AlbumId { get; set; }
            public string? Title { get; set; }
            public string? Url { get; set; }
            public string? ThumbnailUrl { get; set; }
        }


    }
}
