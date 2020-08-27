using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ALTechTest.Classes.MusicBrainz;

namespace ALTechTest.ViewModels.Artist
{
    public class ArtistViewModel
    {
        private const int ShortWorkThreshold = 1; // Longest non-song we'll see is a single word: "Instrumental"

        public ArtistViewModel(Classes.MusicBrainz.Artist artist, Recording[] recordings, Work[] works,
            ConcurrentDictionary<Guid, string> lyricsDictionary)
        {
            Artist = new Models.Artist(artist);
            Recordings = recordings.ToDictionary(x => x.id, y => new Models.Recording(y));
            Works = works.ToDictionary(x => x.id, y => new Models.Work(y));

            RecordingWorks = recordings.Where(x => x.relations.Any())
                .ToDictionary(x => x.id, y => y.relations.FirstOrDefault()?.work.id);
            WorkRecordings = works.Where(x => x.relations.Any()).ToDictionary(x => x.id,
                y => y.relations.Select(z => new Models.Recording(z.recording)));

            ProcessRecordingWorkRelationships();

            foreach (var lyrics in lyricsDictionary)
                Works[lyrics.Key].Lyrics = lyrics.Value;

            ProcessLyricsData();
            RemoveWorksWithTooFewWords(ShortWorkThreshold);
            ProcessStats();
        }

        public Models.Artist Artist { get; set; }
        public IDictionary<Guid, Models.Recording> Recordings { get; set; }

        public IDictionary<Guid, Guid?> RecordingWorks { get; set; }
        public IDictionary<Guid, IEnumerable<Models.Recording>> WorkRecordings { get; set; }
        public IDictionary<Guid, Models.Work> Works { get; set; }

        private void ProcessLyricsData()
        {
            var parallelLoopResult = Parallel.ForEach(Works,
                work =>
                {
                    work.Value.ProcessLyricsAndRecordingData(WorkRecordings.ContainsKey(work.Key)
                        ? WorkRecordings[work.Key].ToArray()
                        : null);
                });

            while (!parallelLoopResult.IsCompleted)
            {
            }
        }

        private void ProcessRecordingWorkRelationships()
        {
        }

        private void ProcessStats()
        {
            Total = Works.Values.Sum(x => x.NumberOfWords);
            Average = Works.Values.Average(x => x.NumberOfWords);
            WordsPerSecond = Works.Values.Average(x => x.WordsPerSecond);
            Min = Works.Values.Min(x => x.NumberOfWords);
            Max = Works.Values.Max(x => x.NumberOfWords);
            Variance = Works.Values.Average(x => Math.Pow(x.NumberOfWords - Average, 2));
            StandardDeviation = Math.Sqrt(Variance);
        }

        private void RemoveWorksWithTooFewWords(int threshold)
        {
            var workIdsWhereNotLongEnough = Works.Where(x => x.Value.NumberOfWords <= threshold);
            foreach (var workId in workIdsWhereNotLongEnough) Works.Remove(workId);
        }

        #region LyricData

        public int Total { get; set; }
        public double Average { get; set; }
        public double? WordsPerSecond { get; set; }
        public int Min { get; set; }
        public int Max { get; set; }
        public double Variance { get; set; }
        public double StandardDeviation { get; set; }

        #endregion LyricData
    }
}