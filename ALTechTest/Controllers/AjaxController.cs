using System.Linq;
using System.Threading.Tasks;
using ALTechTest.Helpers;
using ALTechTest.Interfaces;
using ALTechTest.Models;
using Microsoft.AspNetCore.Mvc;

namespace ALTechTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AjaxController : ControllerBase
    {
        private readonly IMusicBrainzCaller _musicBrainzCaller;

        public AjaxController(IMusicBrainzCaller musicBrainzCaller)
        {
            Verify.NotNull(musicBrainzCaller, nameof(musicBrainzCaller));

            _musicBrainzCaller = musicBrainzCaller;
        }

        public async Task<IActionResult> GetArtists(string query)
        {
            var artists = await _musicBrainzCaller.GetArtists(query);

            return new JsonResult(artists.Select(x => new Artist(x)));
        }
    }
}