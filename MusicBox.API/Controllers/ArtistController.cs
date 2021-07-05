using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MusicBox.API.Extensions;
using MusicBox.API.Resources.Command;
using MusicBox.API.Resources.Query;
using MusicBox.Business.Interfaces;
using MusicBox.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MusicBox.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ArtistController : ControllerBase
    {
        private readonly IArtistService _artistService;
        private readonly IMapper _mapper;

        public ArtistController(IArtistService artistService, IMapper mapper)
        {
            _artistService = artistService;
            _mapper = mapper;
        }

        [HttpGet("{name}")]
        public async Task<ActionResult<ArtistResource>> Get(string name)
        {
            var artist = await _artistService.Get(name);

            if (!artist.Success) return NotFound(artist.Message);

            var response = _mapper.Map<Artist, ArtistResource>(artist.Item);
            return response;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SaveArtistResource resource)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState.GetErrorMessages());

            var artist = _mapper.Map<SaveArtistResource, Artist>(resource);
            var result = await _artistService.Create(artist);

            if (!result.Success) return BadRequest(result.Message);

            var response = _mapper.Map<Artist, ArtistResource>(result.Item);
            return Ok(response);
        }

        [HttpPut("id")]
        public async Task<IActionResult> Modify(short id, [FromBody] SaveArtistResource resource)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState.GetErrorMessages());

            var artist = _mapper.Map<SaveArtistResource, Artist>(resource);

            var result = await _artistService.Modify(id, artist);

            if (!result.Success) return BadRequest(result.Message);

            var response = _mapper.Map<Artist, ArtistResource>(result.Item);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(short id)
        {
            var result = await _artistService.Delete(id);

            if (!result.Success)
                return BadRequest(result.Message);

            var response = _mapper.Map<Artist, ArtistResource>(result.Item);
            return Ok(response);
        }

        [HttpPost("Import")]
        public async Task<IActionResult> Import([FromBody] List<SaveArtistResource> resources)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState.GetErrorMessages());

            //Normaal gesproken zou ik hier de import parallilizeren. Echter gaat de in-memory waar ik om convencience redenen voor gekozen heb
            //daarop onderuit. Met een postgres database kunnen hier losse Tasks van gemaakt waar vervolgens op gewacht wordt om het wat te laten performen.

            //Daarnaast zou ik dit normaal niet in een API oplossen, maar er een background proces van maken die dit middels bijvoorbeeld hangfire op de achtergrond uitvoert.
            //Daar heb ik nu niet voor gekozen vanwege de tijdsbeperking en ik dit niet de meest nuttige gespreksstof vond.
            foreach(var resource in resources)
            {
                var artist = _mapper.Map<SaveArtistResource, Artist>(resource);
                await _artistService.Create(artist);
            }

            return Ok();
        }    
    }
}
