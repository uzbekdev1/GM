using GM.DAL.Entity;
using Microsoft.EntityFrameworkCore;

namespace GM.DAL
{
    public partial class ApplicationDbContext : DbContext
    {

        public DbSet<Server> Servers { get; set; }

        public DbSet<Scoreboard> Scoreboards{ get; set; }

        public DbSet<Player> Players{ get; set; }

        public DbSet<Matche> Matches{ get; set; }

        public DbSet<Map> Maps{ get; set; }

        public DbSet<GameMode> GameModes{ get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Server>().ToTable("Servers");
            modelBuilder.Entity<Scoreboard>().ToTable("Scoreboards");
            modelBuilder.Entity<Player>().ToTable("Players");
            modelBuilder.Entity<Matche>().ToTable("Matches");
            modelBuilder.Entity<Map>().ToTable("Maps");
            modelBuilder.Entity<GameMode>().ToTable("GameModes");
        }
    }
}