using JsonPlaceholderImporter.Context;
using JsonPlaceholderImporter.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.Language.Intermediate;
using System.Security.Policy;
using System.Text.Json;

namespace JsonPlaceholderImporter.Controllers.Imports
{
    [Route("api/import/posts")]
    [ApiController]
    public class PostsImporterController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PostsImporterController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Import()
        {
            const string url = "https://jsonplaceholder.typicode.com/posts";
            using var http = new HttpClient();
            var json = await http.GetStringAsync(url);

            var posts = JsonSerializer.Deserialize<List<ApiPosts>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }) ?? new List<ApiPosts>();

            int added = 0;

            foreach (var post in posts)
            {

                var title = post.Title.Trim() ?? "";
                var body = post.Body.Trim() ?? "";


                var entity = new Post
                {
                    Title = title,
                    Body = body,
                    UserId = post.UserId
                };


                _context.Posts.Add(entity);
                added++;
            }

            await _context.SaveChangesAsync();
            return Ok(new { added });

        }
    }

    internal class ApiPosts() 
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Body { get; set; }
        public int UserId { get; set; }
    }


}
