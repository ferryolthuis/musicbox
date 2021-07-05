using Microsoft.EntityFrameworkCore;
using MusicBox.Model;

namespace PersonalFinance.Persistence.Contexts
{
    internal class MusicBoxDbContext : DbContext
    {
        public MusicBoxDbContext(DbContextOptions<MusicBoxDbContext> options) : base(options)
        {
        }

        public DbSet<Artist> Artists { get; set; }
        public DbSet<Song> Songs { get; set; }       
    }
}