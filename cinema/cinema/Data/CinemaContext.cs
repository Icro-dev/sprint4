using Microsoft.EntityFrameworkCore;
using cinema.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace cinema.Data
{
    public class CinemaContext : IdentityDbContext
    {
        public CinemaContext(DbContextOptions<CinemaContext> options)
            : base(options)
        {
            
        }
        public DbSet<Theatre>? Theatres { get; set; }

    }
}