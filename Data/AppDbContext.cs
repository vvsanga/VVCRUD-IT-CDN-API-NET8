using Microsoft.EntityFrameworkCore;
using VVCRUD_IT_CDN_API_NET8.Models.Entities;

namespace VVCRUD_IT_CDN_API_NET8.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Professional>()
                .HasMany(p => p.Skillset)
                .WithOne(s => s.Professional)
                .HasForeignKey(s => s.ProfessionalId)
                .OnDelete(DeleteBehavior.Cascade); // Cascade delete
        }

        public DbSet<Professional> Professionals { get; set; }
        public DbSet<Skillset> Skillsets { get; set; }
    }
}
