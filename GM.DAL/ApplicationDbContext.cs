using GM.DAL.Entity;
using Microsoft.EntityFrameworkCore;

namespace GM.DAL
{
    public partial class ApplicationDbContext : DbContext
    {
        public DbSet<Server> Servers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Server>().ToTable("Servers");
        }
    }
}