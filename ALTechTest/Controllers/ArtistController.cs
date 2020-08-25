using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using ALTechTest.Helpers;
using ALTechTest.Interfaces;
using ALTechTest.ViewModels.Artist;
using Microsoft.AspNetCore.Mvc;

namespace ALTechTest.Controllers
{
    public class ArtistController : Controller
    {
        private readonly IMusicBrainzCaller _musicBrainzCaller;
        private readonly ILyricsOvhCaller _lyricsOvhCaller;

        public ArtistController(IMusicBrainzCaller musicBrainzCaller, ILyricsOvhCaller lyricsOvhCaller)
        {
            Verify.NotNull(musicBrainzCaller, nameof(musicBrainzCaller));
            Verify.NotNull(lyricsOvhCaller, nameof(lyricsOvhCaller));

            _musicBrainzCaller = musicBrainzCaller;
            _lyricsOvhCaller = lyricsOvhCaller;
        }

        [HttpGet]
        public async Task<IActionResult> Index(Guid id)
        {
            var artist = await _musicBrainzCaller.GetArtistById(id);
            var works = (await _musicBrainzCaller.GetWorksByArtistId(id)).ToArray();
            var lyrics = await GetLyrics(artist, works);

            return View(new ArtistViewModel(artist, works, lyrics));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Search(string query)
        {
            var artists = await _musicBrainzCaller.GetArtists(query);

            return View(new ArtistsViewModel(artists));
        }

        private async Task<ConcurrentDictionary<Guid, string>> GetLyrics(Classes.MusicBrainz.Artist artist, Classes.MusicBrainz.Work[] works)
        {
            var lyricsDictionary = new ConcurrentDictionary<Guid, string>();

            var tasks = works.Select(async work =>
            {
                var lyrics = await _lyricsOvhCaller.GetLyrics(artist.name, work.title);
                lyricsDictionary.TryAdd(work.id, lyrics);
            });

            await Task.WhenAll(tasks);

            return lyricsDictionary;
        }
    }
}