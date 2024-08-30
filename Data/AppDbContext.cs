using Microsoft.EntityFrameworkCore;
using VVCRUD_IT_CDN_API_NET8.Models.Entities;

namespace VVCRUD_IT_CDN_API_NET8.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Professional> Professionals { get; set; }
    }
}
