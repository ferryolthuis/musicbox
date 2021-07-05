using MusicBox.Business.Communication;
using MusicBox.Model;
using System.Threading.Tasks;

namespace MusicBox.Business.Interfaces
{
    public interface IArtistService
    {
        Task<ServiceResponse<Artist>> Get(string name);
        Task<ServiceResponse<Artist>> Create(Artist artist);
        Task<ServiceResponse<Artist>> Modify(short id, Artist artist);
        Task<ServiceResponse<Artist>> Delete(short id);
    }
}
