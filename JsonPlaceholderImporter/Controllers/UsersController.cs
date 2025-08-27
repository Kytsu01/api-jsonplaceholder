using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JsonPlaceholderImporter.Context;
using JsonPlaceholderImporter.Models;
using JsonPlaceholderImporter.Dtos;
using Microsoft.JSInterop.Infrastructure;

namespace JsonPlaceholderImporter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UsersController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users
                .Include(u => u.Address).ThenInclude(a => a.Geo)
                .Include(u => u.Company).Include(u => u.Posts)
                .Include(u => u.Albums).ThenInclude(a => a.Photos)
                .Include(u => u.Todos).ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.Users.Include(u => u.Company)
                       .Include(u => u.Address).ThenInclude(a => a.Geo)
                       .Include(u => u.Albums).ThenInclude(a => a.Photos)
                       .Include(u => u.Posts).Include(u => u.Todos)
                       .FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        [HttpGet("post-count")]
        public async Task<IActionResult> GetUsersWithPostCount()
        {
            var sql = @"SELECT u.Id AS UserId, U.Name, COUNT(p.Id) AS PostCount
                        FROM Users u LEFT JOIN Posts p ON p.UserId = u.Id
                        GROUP BY u.Id, u.Name ORDER BY PostCount DESC, u.Name";

            var data = await _context.UserPostCounts.FromSqlRaw(sql).ToListAsync();

            return Ok(data);
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> PostUser([FromBody] UserCreateDto dto)
        {
            var email = (dto.Email ?? "").Trim().ToLower();
            var name = (dto.Name ?? "").Trim();
            var username = (dto.Username ?? "").Trim();
            var phone = (dto.Phone ?? "").Trim();
            var website = (dto.Website ?? "").Trim().ToLower();

            var street = (dto.Address.Street ?? "").Trim();
            var city = (dto.Address.City ?? "").Trim();
            var zipCode = (dto.Address.ZipCode ?? "").Trim();

            var lat = (dto.Address.Geo.Lat ?? "").Trim();
            var lng = (dto.Address.Geo.Lng ?? "").Trim();

            var cmpName = (dto.Company.Name ?? "").Trim();
            var catchPhrase = (dto.Company.CatchPhrase ?? "").Trim();
            var bs = (dto.Company.Bs ?? "").Trim();

            var user = new User 
            {
                Name = name,
                UserName = username,
                Email = email,
                Phone = phone,
                Website = website
            };

            if (dto.Address != null)
            {
                user.Address = new Address
                {
                    Street = street,
                    City = city,
                    ZipCode = zipCode,
                    Geo = dto.Address.Geo is null ? null : new Geolocation
                    {
                        Lat = lat,
                        Lng = lng
                    }
                };
            }

            if(dto.Company != null)
            {
                user.Company = new Company 
                {
                    Name = cmpName,
                    CatchPhrase = catchPhrase,
                    Bs = bs
                };

            }

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users
                .Include(u => u.Address).ThenInclude(a => a.Geo)
                .Include(u => u.Company).Include(u => u.Posts)
                .Include(u => u.Albums).ThenInclude(u => u.Photos)
                .Include(u => u.Todos).FirstOrDefaultAsync(u => u.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            var address = await _context.Adresses.FindAsync(user.Address.Id);
            var geo = await _context.Geolocations.FindAsync(address.Geo.Id);
            var company = await _context.Companies.FindAsync(user.Company.Id);

            _context.Users.Remove(user);

            try
            {
                if (geo != null)
                {
                    _context.Geolocations.Remove(geo);
                }

                if (address != null)
                {
                    _context.Adresses.Remove(address);
                }

                if (company != null)
                {
                    _context.Companies.Remove(company);
                }
            } catch (Exception ex)
            {
                Console.WriteLine($"Falha ao remover as entidades relacionada ao usuario: {ex}");
            }

            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
