using Microsoft.EntityFrameworkCore;
using MusicBox.Model;
using MusicBox.Persistence.Interfaces;
using PersonalFinance.Persistence.Contexts;
using System;
using System.Threading.Tasks;

namespace MusicBox.Persistence.Repositories
{
    internal class ArtistRepository : IArtistRepository
    {
        private readonly MusicBoxDbContext _context;

        public ArtistRepository(MusicBoxDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Artist artist)
        {
            await _context.Artists.AddAsync(artist);
        }

        public async Task<Artist> FindByNameAsync(string name)
        {
            return await _context.Artists
                                 .FirstOrDefaultAsync(a => string.Equals(a.Name, name, StringComparison.OrdinalIgnoreCase));
        }

        public async Task<Artist> FindByIdAsync(short id)
        {
            return await _context.Artists
                                 .FirstOrDefaultAsync(a => a.Id == id);
        }

        public void Update(Artist artist)
        {
            _context.Artists.Update(artist);
        }

        public void Remove(Artist artist)
        {
            _context.Artists.Remove(artist);
        }
    }
}
