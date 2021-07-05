using MusicBox.Model;
using System.Threading.Tasks;

namespace MusicBox.Persistence.Interfaces
{
    public interface IArtistRepository
    {
        Task<Artist> FindByNameAsync(string name);
        Task<Artist> FindByIdAsync(short id);
        Task AddAsync(Artist artist);
        void Update(Artist artist);
        void Remove(Artist artist);
    }
}
