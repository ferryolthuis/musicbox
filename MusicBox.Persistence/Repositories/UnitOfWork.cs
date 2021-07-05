using MusicBox.Persistence.Interfaces;
using PersonalFinance.Persistence.Contexts;
using System.Threading.Tasks;

namespace MusicBox.Persistence.Repositories
{
    internal class UnitOfWork : IUnitOfWork
    {
        private readonly MusicBoxDbContext _context;

        public UnitOfWork(MusicBoxDbContext context)
        {
            _context = context;
        }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
