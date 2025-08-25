using Microsoft.EntityFrameworkCore;
using JsonPlaceholderImporter.Models;

namespace JsonPlaceholderImporter.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Address> Adresses { get; set; } = null!;
        public DbSet<Geolocation> Geolocations { get; set; } = null!;
        public DbSet<Company> Companies { get; set; } = null!;
        public DbSet<Post> Posts { get; set; } = null!;
        public DbSet<Comment> Comments { get; set; } = null!;
        public DbSet<Album> Albums { get; set; } = null!;
        public DbSet<Photo> Photos { get; set; } = null!;
        public DbSet<Todo> Todos {  get; set; } = null!;

    }
}
