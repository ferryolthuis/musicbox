using System.Threading.Tasks;

namespace MusicBox.Persistence.Interfaces
{
    public interface IUnitOfWork
    {
        Task CompleteAsync();
    }
}
