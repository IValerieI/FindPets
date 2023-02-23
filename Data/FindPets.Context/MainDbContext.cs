using FindPets.Context.Entities;
using Microsoft.EntityFrameworkCore;

namespace FindPets.Context
{
    public class MainDbContext : DbContext
    {
        public DbSet<Animal> Animals { get; set; }

        public MainDbContext(DbContextOptions<MainDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
