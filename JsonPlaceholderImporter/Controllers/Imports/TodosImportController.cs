using JsonPlaceholderImporter.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;

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

        internal class ApiTodos 
        {
            public int Id { get; set; }
            public int UserId { get; set; }
            public string? Title { get; set; }
            public bool Completed { get; set; }
        }

    }
}
