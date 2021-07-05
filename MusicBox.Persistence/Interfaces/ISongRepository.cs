using MusicBox.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MusicBox.Persistence.Interfaces
{
    public interface ISongRepository
    {
        Task<IEnumerable<Song>> FindByGenreAsync(string name);
        Task<Song> FindByIdAsync(short id);
        Task AddAsync(Song artist);
        void Update(Song artist);
        void Remove(Song artist);
    }
}
