using JsonPlaceholderImporter.Context;
using JsonPlaceholderImporter.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Security.Policy;
using System.Text.Json;

namespace JsonPlaceholderImporter.Controllers.Imports
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodosImportController : ControllerBase
    {

        public AppDbContext _context;

        public TodosImportController(AppDbContext context   )
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Import()
        {

            const string url = "https://jsonplaceholder.typicode.com/todos";

            using var http = new HttpClient();
            var json = await http.GetStringAsync(url);

            var todos = JsonSerializer.Deserialize<List<ApiTodos>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            int added = 0;

            foreach(var todo in todos)
            {

                var title = todo.Title.Trim() ?? "";

                var entity = new Todo
                {
                    UserId = todo.UserId,
                    Title = title,
                    Completed = todo.Completed
                };

                _context.Todos.Add(entity);
                added++;
            }

            await _context.SaveChangesAsync();
            return Ok(new { added });

        }

        internal class ApiTodos 
        {
            public int Id { get; set; }
            public int UserId { get; set; }
            public string? Title { get; set; }
            public bool Completed { get; set; }
        }

    }
}
