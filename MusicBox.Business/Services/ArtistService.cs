using MusicBox.Business.Communication;
using MusicBox.Business.Interfaces;
using MusicBox.Model;
using MusicBox.Persistence.Interfaces;
using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace MusicBox.Business.Services
{
    internal class ArtistService : IArtistService
    {
        private readonly IArtistRepository _artistRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ArtistService(IArtistRepository artistRepository, IUnitOfWork unitOfWork)
        {
            _artistRepository = artistRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ServiceResponse<Artist>> Get(string name)
        {
            var item = await _artistRepository.FindByNameAsync(name);

            if (item == null) return new ServiceResponse<Artist>($"An artist with name {name} could not be found.");

            return new ServiceResponse<Artist>(item);
        }

        public async Task<ServiceResponse<Artist>> Create(Artist artist)
        {
            var artistName = artist.Name;
            var existingItem = await _artistRepository.FindByNameAsync(artistName);

            if (existingItem != null) return new ServiceResponse<Artist>($"An artist with name {artistName} already exists.");

            try
            {
                await _artistRepository.AddAsync(artist);
                await _unitOfWork.CompleteAsync();

                return new ServiceResponse<Artist>(artist);
            }
            catch (Exception)
            {
                return new ServiceResponse<Artist>($"An unknown error occurred while saving the Artist.");
            }
        }

        public async Task<ServiceResponse<Artist>> Modify(short id, Artist artist)
        {
            var existingItem = await _artistRepository.FindByIdAsync(id);

            if (existingItem == null) return new ServiceResponse<Artist>($"An artist with id {id} could not be found.");
            existingItem.UpdateProperties(artist);

            try
            {
                _artistRepository.Update(existingItem);
                await _unitOfWork.CompleteAsync();

                return new ServiceResponse<Artist>(existingItem);
            }
            catch (Exception)
            {
                return new ServiceResponse<Artist>($"An unknown error occurred while updating the Artist with id {id}");
            }
        }

        public async Task<ServiceResponse<Artist>> Delete(short id)
        {
            var existingItem = await _artistRepository.FindByIdAsync(id);

            if (existingItem == null) return new ServiceResponse<Artist>($"An artist with id {id} could not be found.");

            try
            {
                _artistRepository.Remove(existingItem);
                await _unitOfWork.CompleteAsync();

                return new ServiceResponse<Artist>(existingItem);
            }
            catch (Exception)
            {
                return new ServiceResponse<Artist>($"An unknown error occurred while deleting the Artist with id {id}");
            }
        }
    }
}
