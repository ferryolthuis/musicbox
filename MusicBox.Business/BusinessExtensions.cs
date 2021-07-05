using Microsoft.Extensions.DependencyInjection;
using MusicBox.Business.Interfaces;
using MusicBox.Business.Services;

namespace MusicBox.Business
{
    public static class BusinessExtensions
    {
        public static IServiceCollection AddDomain(this IServiceCollection services)
        {
            services.AddScoped<IArtistService, ArtistService>();
            services.AddScoped<ISongService, SongService>();

            return services;
        }
    }
}
