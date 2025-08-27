using JsonPlaceholderImporter.Context;
using JsonPlaceholderImporter.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace JsonPlaceholderImporter.Controllers.Imports
{
    [Route("api/import/comments")]
    [ApiController]
    public class CommentsImportController : ControllerBase
    {

        public AppDbContext _context;

        public CommentsImportController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Import()
        {

            const string url = "https://jsonplaceholder.typicode.com/comments";

            using var http = new HttpClient();
            var json = await http.GetStringAsync(url);

            var comments = JsonSerializer.Deserialize<List<ApiComment>>(json, new JsonSerializerOptions {
                PropertyNameCaseInsensitive = true
            }) ?? new List<ApiComment>();

            int added = 0;

            foreach(var comment in comments)
            {

                var name = comment.Name.Trim() ?? "";
                var email = comment.Email.Trim().ToLower() ?? "";
                var body = comment.Body.Trim() ?? "";

                var entity = new Comment
                {
                    PostId = comment.PostId,
                    Name = name,
                    Email = email,
                    Body = body
                };

                _context.Comments.Add(entity);
                added++;
            }

            await _context.SaveChangesAsync();
            return Ok(new { added });
        }


        internal class ApiComment
        { 
            public int Id { get; set; }
            public int PostId { get; set; }
            public string? Name { get; set; }
            public string? Email { get; set; }
            public string? Body { get; set; }
        }


    }
}
