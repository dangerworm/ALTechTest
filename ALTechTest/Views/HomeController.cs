using ALTechTest.Helpers;
using ALTechTest.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ALTechTest.Views
{
    public class HomeController : Controller
    {
        private readonly IMusicBrainzCaller _musicBrainzCaller;

        public HomeController(IMusicBrainzCaller musicBrainzCaller)
        {
            Verify.NotNull(musicBrainzCaller, nameof(musicBrainzCaller));

            _musicBrainzCaller = musicBrainzCaller;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}