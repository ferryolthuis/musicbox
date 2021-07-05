using MusicBox.Business.Communication;
using MusicBox.Business.Interfaces;
using MusicBox.Model;
using MusicBox.Persistence.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MusicBox.Business.Services
{
    internal class SongService : ISongService
    {
        private readonly ISongRepository _songRepository;
        private readonly IArtistRepository _artistRepository;
        private readonly IUnitOfWork _unitOfWork;

        public SongService(ISongRepository songRepository, IArtistRepository artistRepository, IUnitOfWork unitOfWork)
        {
            _songRepository = songRepository;
            _unitOfWork = unitOfWork;
            _artistRepository = artistRepository;
        }

        public async Task<ServiceResponse<IEnumerable<Song>>> Search(string genre)
        {
            var item = await _songRepository.FindByGenreAsync(genre);

            if (item == null) return new ServiceResponse<IEnumerable<Song>>($"No songs of genre {genre} could be found.");

            return new ServiceResponse<IEnumerable<Song>>(item);
        }

        public async Task<ServiceResponse<Song>> Create(Song song, string artistName)
        {
            var artist = await _artistRepository.FindByNameAsync(artistName);

            if (artist == null) return new ServiceResponse<Song>($"The specified artist {artistName} could be found. Add the artist first before adding the song");

            try
            {
                song.Artist = artist;
                await _songRepository.AddAsync(song);
                await _unitOfWork.CompleteAsync();

                return new ServiceResponse<Song>(song);
            }
            catch (Exception)
            {
                return new ServiceResponse<Song>($"An unknown error occurred while saving the Song.");
            }
        }

        public async Task<ServiceResponse<Song>> Modify(short id, Song song, string artistName)
        {
            var artist = await _artistRepository.FindByNameAsync(artistName);
            if (artist == null) return new ServiceResponse<Song>($"The specified artist {artistName} could be found. Add the artist first before changing the song");

            var existingItem = await _songRepository.FindByIdAsync(id);

            if (existingItem == null) return new ServiceResponse<Song>($"A song with id {id} could not be found.");
            existingItem.UpdateProperties(song);
            
            try
            {
                song.Artist = artist;
                _songRepository.Update(existingItem);
                await _unitOfWork.CompleteAsync();

                return new ServiceResponse<Song>(existingItem);
            }
            catch (Exception)
            {
                return new ServiceResponse<Song>($"An unknown error occurred while updating the Song with id {id}");
            }
        }

        public async Task<ServiceResponse<Song>> Delete(short id)
        {
            var existingItem = await _songRepository.FindByIdAsync(id);

            if (existingItem == null) return new ServiceResponse<Song>($"A song with id {id} could not be found.");

            try
            {
                _songRepository.Remove(existingItem);
                await _unitOfWork.CompleteAsync();

                return new ServiceResponse<Song>(existingItem);
            }
            catch (Exception)
            {
                return new ServiceResponse<Song>($"An unknown error occurred while deleting the Song with id {id}");
            }
        }
    }
}
