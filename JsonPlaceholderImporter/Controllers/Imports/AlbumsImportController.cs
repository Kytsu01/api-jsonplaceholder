using JsonPlaceholderImporter.Context;
using JsonPlaceholderImporter.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace JsonPlaceholderImporter.Controllers.Imports
{
    [Route("api/import/albums")]
    [ApiController]
    public class AlbumsImportController : ControllerBase
    {
        public AppDbContext _context;

        public AlbumsImportController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Import()
        {

            const string url = "https://jsonplaceholder.typicode.com/albums";

            using var http = new HttpClient();
            var json = await http.GetStringAsync(url);

            var albums = JsonSerializer.Deserialize<List<ApiAlbums>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });


            int added = 0;

            foreach(var album in albums)
            {
                var entity = new Album
                {
                    UserId = album.UserId,
                    Title = album.Title ?? string.Empty
                };

                _context.Albums.Add(entity);
                added++;
            }

            await _context.SaveChangesAsync();
            return Ok(new { added });
        }

        internal class ApiAlbums 
        {
            public int Id { get; set; }
            public int UserId { get; set; }
            public string? Title { get; set; }
        }

    }
}
