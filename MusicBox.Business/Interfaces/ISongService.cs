using MusicBox.Business.Communication;
using MusicBox.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MusicBox.Business.Interfaces
{
    public interface ISongService
    {
        Task<ServiceResponse<IEnumerable<Song>>> Search(string genre);
        Task<ServiceResponse<Song>> Create(Song song, string artist);
        Task<ServiceResponse<Song>> Modify(short id, Song song, string artist);
        Task<ServiceResponse<Song>> Delete(short id);
    }
}
