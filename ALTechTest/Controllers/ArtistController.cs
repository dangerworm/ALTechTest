using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ALTechTest.DataTransferObjects;
using ALTechTest.Helpers;
using ALTechTest.Interfaces;
using ALTechTest.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ALTechTest.Controllers
{
    public class ArtistController : Controller
    {
        private readonly ILyricsOvhCaller _lyricsOvhCaller;
        private readonly IMusicBrainzCaller _musicBrainzCaller;

        public ArtistController(IMusicBrainzCaller musicBrainzCaller, ILyricsOvhCaller lyricsOvhCaller)
        {
            Verify.NotNull(musicBrainzCaller, nameof(musicBrainzCaller));
            Verify.NotNull(lyricsOvhCaller, nameof(lyricsOvhCaller));

            _musicBrainzCaller = musicBrainzCaller;
            _lyricsOvhCaller = lyricsOvhCaller;
        }

        private async Task<ConcurrentDictionary<Guid, string>> GetLyrics(ArtistDto artist, IEnumerable<WorkDto> works)
        {
            var lyricsDictionary = new ConcurrentDictionary<Guid, string>();

            var tasks = works.Select(async work =>
            {
                var lyricsResult = await _lyricsOvhCaller.GetLyrics(artist.Name, work.Title);
                lyricsDictionary.TryAdd(work.Id, lyricsResult?.Lyrics ?? "");
            });

            await Task.WhenAll(tasks);

            return lyricsDictionary;
        }

        [HttpGet]
        public async Task<IActionResult> Index(Guid id)
        {
            var artist = await _musicBrainzCaller.GetArtistById(id);
            var recordingsTask = _musicBrainzCaller.GetRecordingsByArtistId(id);
            var worksTask = _musicBrainzCaller.GetWorksByArtistId(id);

            Task.WaitAll(recordingsTask, worksTask);

            var recordings = recordingsTask.Result.ToArray();
            var works = worksTask.Result.ToArray();

            var lyrics = await GetLyrics(artist, works);

            return View(new ArtistViewModel(artist, recordings, works, lyrics));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Search(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
                return RedirectToAction("Index", "Home");

            var artists = (await _musicBrainzCaller.GetArtists(query)).ToArray();
            if (artists.Length == 1) return RedirectToAction("Index", new {artists.First().Id});

            return View(new ArtistsViewModel(artists));
        }
    }
}