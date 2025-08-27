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
using System.Reflection;
using Microsoft.AspNetCore.Routing.Constraints;

namespace JsonPlaceholderImporter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PhotosController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Photos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Photo>>> GetPhotos(
            [FromQuery] int page = 1, [FromQuery] int pageSize = 20)
        {

            var q = _context.Photos.AsNoTracking();

            var total = await q.CountAsync();

            var items = await q.OrderBy(p => p.Id).Skip((page - 1) * pageSize)
                        .Take(pageSize).Select(p => new { p.Id, p.Title, p.Url, p.ThumbnailUrl, p.AlbumId })
                        .ToListAsync();

            return Ok(new { total, page, pageSize, items });
        }

        // GET: api/Photos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Photo>> GetPhoto(int id)
        {
            var photo = await _context.Photos.FindAsync(id);

            if (photo == null)
            {
                return NotFound();
            }

            return photo;
        }

        // PUT: api/Photos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPhoto(int id, Photo photo)
        {
            if (id != photo.Id)
            {
                return BadRequest();
            }

            _context.Entry(photo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PhotoExists(id))
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

        // POST: api/Photos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Photo>> PostPhoto(PhotoCreateDto dtoPhoto)
        {

            var title = (dtoPhoto.Title ?? "").Trim();
            var url = (dtoPhoto.Url ?? "").Trim().ToLower();
            var thumbnailUrl = (dtoPhoto.ThumbnailUrl ?? "").Trim().ToLower();

            var photo = new Photo
            {
                AlbumId = dtoPhoto.AlbumId,
                Title = title,
                Url = url,
                ThumbnailUrl = thumbnailUrl
            };

            _context.Photos.Add(photo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPhoto", new { id = photo.Id }, photo);
        }

        // DELETE: api/Photos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePhoto(int id)
        {
            var photo = await _context.Photos.FindAsync(id);
            if (photo == null)
            {
                return NotFound();
            }

            _context.Photos.Remove(photo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PhotoExists(int id)
        {
            return _context.Photos.Any(e => e.Id == id);
        }
    }
}
