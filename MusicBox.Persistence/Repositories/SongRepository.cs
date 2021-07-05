using Microsoft.EntityFrameworkCore;
using MusicBox.Model;
using MusicBox.Persistence.Interfaces;
using PersonalFinance.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicBox.Persistence.Repositories
{
    internal class SongRepository : ISongRepository
    {
        private readonly MusicBoxDbContext _context;

        public SongRepository(MusicBoxDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Song song)
        {
            await _context.Songs.AddAsync(song);
        }

        public async Task<IEnumerable<Song>> FindByGenreAsync(string genre)
        {
            var songs = _context.Songs.AsQueryable();
            return await songs.Where(s => string.Equals(s.Genre, genre, StringComparison.OrdinalIgnoreCase)).ToListAsync();
        }

        public async Task<Song> FindByIdAsync(short id)
        {
            return await _context.Songs
                                 .FirstOrDefaultAsync(a => a.Id == id);
        }

        public void Update(Song song)
        {
            _context.Songs.Update(song);
        }

        public void Remove(Song song)
        {
            _context.Songs.Remove(song);
        }
    }
}
