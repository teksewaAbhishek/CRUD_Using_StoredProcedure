using Microsoft.EntityFrameworkCore;

namespace AspNetCoreApi6.Models
{
    public class Movie2Context : DbContext
    {
        public Movie2Context(DbContextOptions<Movie2Context> options) : base(options)
        {
        }

        public DbSet<Movie2> Movie2s { get; set; } = null!;
    }
}
