using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MusicBox.Persistence.Interfaces;
using MusicBox.Persistence.Repositories;
using PersonalFinance.Persistence.Contexts;

namespace MusicBox.Persistence.Extensions
{
    public static class PersistenceExtensions
    {
        public static IServiceCollection AddInMemoryPersistence(this IServiceCollection services)
        {
            services.AddDbContext<MusicBoxDbContext>(options =>
            {
                options.UseInMemoryDatabase("supermarket-api-in-memory");
            });

            ConfigureRepositories(services);

            return services;
        }

        private static void ConfigureRepositories(IServiceCollection services)
        {
            services.AddScoped<IArtistRepository, ArtistRepository>();
            services.AddScoped<ISongRepository, SongRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
