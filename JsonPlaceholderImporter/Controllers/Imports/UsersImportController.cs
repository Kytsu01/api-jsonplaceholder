using Azure.Identity;
using JsonPlaceholderImporter.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using JsonPlaceholderImporter.Models;
using System.Security.Policy;
using System.Runtime;


namespace JsonPlaceholderImporter.Controllers.Imports
{
    [Route("api/import/users")]
    [ApiController]
    public class UsersImportController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UsersImportController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Import()
        {
            const string url = "https://jsonplaceholder.typicode.com/users";

            using var http = new HttpClient();
            var json = await http.GetStringAsync(url);

            var users = JsonSerializer.Deserialize<List<ApiUser>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }) ?? new List<ApiUser>();

            int added = 0;

            foreach (var user in users) 
            {
                var entity = new User
                {
                    Name = user.Name ?? string.Empty,
                    UserName = user.Username ?? string.Empty,
                    Email = user.Email ?? string.Empty,
                    Phone = user.Phone ?? string.Empty,
                    Website = user.Website ?? string.Empty
                };

                if(user.Address != null)
                {
                    var addr = new Address
                    {
                        Street = user.Address.Street ?? string.Empty,
                        City = user.Address.City ?? string.Empty,
                        ZipCode = user.Address.Zipcode ?? string.Empty
                    };

                    if(user.Address.Geo != null)
                    {
                        addr.Geo = new Geolocation 
                        { 
                            Lat = user.Address.Geo.Lat ?? string.Empty,
                            Lng = user.Address.Geo.Lng ?? string.Empty                        
                        };

                    }

                    entity.Address = addr;
                }

                if(user.Company != null)
                {
                    var cmp = new Company
                    {
                        Name = user.Company.Name ?? string.Empty,
                        CatchPhrase = user.Company.Catchphrase ?? string.Empty,
                        Bs = user.Company.Bs ?? string.Empty
                    };

                    entity.Company = cmp;

                }

                _context.Users.Add(entity);
                added++;
            }

            await _context.SaveChangesAsync();
            return Ok(new { added });

        }
    }

    internal class ApiUser
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Website { get; set; }
        public ApiAddress? Address { get; set; }
        public ApiCompany? Company { get; set; }
    }

    internal class ApiGeo
    {
        public string? Lat {  get; set; }
        public string? Lng { get; set; }
    }

    internal class ApiAddress
    {
        public string? Street { get; set; }
        public string? City { get; set; }
        public string? Zipcode { get; set; }
        public ApiGeo? Geo { get; set; }
    }

    internal class ApiCompany
    {
        public string? Name { get; set; }
        public string? Catchphrase { get; set; }
        public string? Bs {  get; set; }
    }
}
