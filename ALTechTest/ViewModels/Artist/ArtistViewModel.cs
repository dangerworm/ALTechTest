using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ALTechTest.Models;

namespace ALTechTest.ViewModels.Artist
{
    public class ArtistViewModel
    {
        private const int ShortWorkThreshold = 20;

        public ArtistViewModel(Classes.MusicBrainz.Artist artist, IEnumerable<Classes.MusicBrainz.Work> works, ConcurrentDictionary<Guid, string> lyricsDictionary)
        {
            Artist = new Models.Artist(artist);
            Works = works.ToDictionary(x => x.id, y => new Models.Work(y));

            foreach (var lyrics in lyricsDictionary)
            {
                Works[lyrics.Key].Lyrics = lyrics.Value;
            }

            ProcessLyricsData();
            RemoveWorksWithTooFewWords(ShortWorkThreshold);
            ProcessStats();
        }

        public Models.Artist Artist { get; set; }
        public IDictionary<Guid, Models.Work> Works { get; set; }

        #region LyricData
        
        public int Total { get; set; }
        public double Average { get; set; }
        public int Min { get; set; }
        public int Max { get; set; }
        public double Variance { get; set; }
        public double StandardDeviation { get; set; }

        #endregion LyricData

        private void ProcessLyricsData()
        {
            var parallelLoopResult = Parallel.ForEach(Works, work => { work.Value.ProcessLyricsData(); });

            while (!parallelLoopResult.IsCompleted)
            {
            }
        }

        private void RemoveWorksWithTooFewWords(int threshold)
        {
            var workIdsWhereNotLongEnough = Works.Where(x => x.Value.NumberOfWords < threshold);
            foreach (var workId in workIdsWhereNotLongEnough)
            {
                Works.Remove(workId);
            }
        }

        private void ProcessStats()
        {
            Total = Works.Values.Sum(x => x.NumberOfWords);
            Average = Works.Values.Average(x => x.NumberOfWords);
            Min = Works.Values.Min(x => x.NumberOfWords);
            Max = Works.Values.Max(x => x.NumberOfWords);
            Variance = Works.Values.Average(x => Math.Pow(x.NumberOfWords - Average, 2));
            StandardDeviation = Math.Sqrt(Variance);
        }
    }
}