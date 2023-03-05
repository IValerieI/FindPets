using FindPets.Context.Entities;
using Microsoft.EntityFrameworkCore;

namespace FindPets.Context
{
    public class MainDbContext : DbContext
    {
        public DbSet<Animal> Animals { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Request> Requests { get; set; }

        public MainDbContext(DbContextOptions<MainDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<Animal>().ToTable("animals");
            modelBuilder.Entity<Animal>().Property(x => x.Kind).IsRequired();
            modelBuilder.Entity<Animal>().Property(x => x.Kind).HasMaxLength(50);

            modelBuilder.Entity<Animal>().Property(x => x.Breed).IsRequired();
            modelBuilder.Entity<Animal>().Property(x => x.Breed).HasMaxLength(50);

            modelBuilder.Entity<Animal>().Property(x => x.Description).IsRequired();
            modelBuilder.Entity<Animal>().Property(x => x.Description).HasMaxLength(2000);

            modelBuilder.Entity<Animal>().HasMany(x => x.Comments).WithOne();


            modelBuilder.Entity<Comment>().ToTable("comments");
            modelBuilder.Entity<Comment>().Property(x => x.Name).IsRequired();
            modelBuilder.Entity<Comment>().Property(x => x.Name).HasMaxLength(50);

            modelBuilder.Entity<Comment>().Property(x => x.Text).IsRequired();
            modelBuilder.Entity<Comment>().Property(x => x.Text).HasMaxLength(500);

            //modelBuilder.Entity<Comment>().HasOne(x => x.Animal).WithMany(x => x.Comments).HasForeignKey(x => x.AnimalId).OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<Request>().ToTable("requests");
            modelBuilder.Entity<Request>().Property(x => x.Name).IsRequired();
            modelBuilder.Entity<Request>().Property(x => x.Name).HasMaxLength(50);

            modelBuilder.Entity<Request>().Property(x => x.Phone).IsRequired();
            modelBuilder.Entity<Request>().Property(x => x.Phone).HasMaxLength(20);

            modelBuilder.Entity<Request>().HasOne(x => x.Animal).WithMany(x => x.Requests).HasForeignKey(x => x.AnimalId).OnDelete(DeleteBehavior.Restrict);

        }
    }
}
