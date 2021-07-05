using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MusicBox.API.Extensions;
using MusicBox.API.Resources.Command;
using MusicBox.API.Resources.Query;
using MusicBox.Business.Interfaces;
using MusicBox.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicBox.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SongController : ControllerBase
    {
        private readonly ISongService _songService;
        private readonly IMapper _mapper;

        public SongController(ISongService songService, IMapper mapper)
        {
            _songService = songService;
            _mapper = mapper;
        }

        [HttpGet("{genre}")]
        public async Task<IEnumerable<SongResource>> Get(string genre)
        {
            var songs = await _songService.Search(genre);
            var response = _mapper.Map<IEnumerable<Song>, IEnumerable<SongResource>>(songs.Item);
            return response;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SaveSongResource resource)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState.GetErrorMessages());

            var song = _mapper.Map<SaveSongResource, Song>(resource);
            var result = await _songService.Create(song, resource.Artist);

            if (!result.Success) return BadRequest(result.Message);

            var response = _mapper.Map<Song, SongResource>(result.Item);
            return Ok(response);
        }

        [HttpPut("id")]
        public async Task<IActionResult> Modify(short id, [FromBody] SaveSongResource resource)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState.GetErrorMessages());

            var song = _mapper.Map<SaveSongResource, Song>(resource);

            var result = await _songService.Modify(id, song, resource.Artist);

            if (!result.Success) return BadRequest(result.Message);

            var response = _mapper.Map<Song, SongResource>(result.Item);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(short id)
        {
            var result = await _songService.Delete(id);

            if (!result.Success)
                return BadRequest(result.Message);

            var response = _mapper.Map<Song, SongResource>(result.Item);
            return Ok(response);
        }

        [HttpPost("Import")]
        public async Task<IActionResult> Import([FromBody] List<SaveSongResource> resources)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrorMessages());
            }
            //Normaal gesproken zou ik hier de import parallelizeren. Echter gaat de in-memory waar ik om convencience redenen voor gekozen heb
            //daarop onderuit. Met een postgres database kunnen hier losse Tasks van gemaakt waar vervolgens op gewacht wordt om het wat te laten performen.

            //Daarnaast zou ik dit normaal niet in een API oplossen, maar er een background proces van maken die dit middels bijvoorbeeld hangfire op de achtergrond uitvoert.
            //Daar heb ik nu niet voor gekozen vanwege de tijdsbeperking en ik dit niet de meest nuttige gespreksstof vond.

            var interestingSongs = resources.Where(s => s.Genre.ToLower().Contains("metal") && s.Year < 2016);
            var errors = new List<string>();
            foreach(var resource in interestingSongs)
            {
                var song = _mapper.Map<SaveSongResource, Song>(resource);
                var result = await _songService.Create(song, resource.Artist);
                if(!result.Success)
                {
                    errors.Add(result.Message);
                }
            }

            if(errors.Any())
            {
                return BadRequest(string.Join(", ", errors.ToArray()));
            }
            return Ok();
        }    
    }
}