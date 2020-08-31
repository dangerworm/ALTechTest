using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ALTechTest.DataTransferObjects;

namespace ALTechTest.ViewModels
{
    public class ArtistViewModel
    {
        private const int ShortWorkThreshold = 1; // Longest non-song we'll see is a single word: "Instrumental"

        public ArtistViewModel(ArtistDto artist, RecordingDto[] recordings, WorkDto[] works,
            ConcurrentDictionary<Guid, string> lyricsDictionary)
        {
            Artist = artist;
            Recordings = recordings.Select(x => new RecordingViewModel(x)).ToDictionary(x => x.Id);
            Works = works.Select(x => new WorkViewModel(x)).ToDictionary(x => x.Id);

            RecordingWorks = recordings.Where(x => x.Relationships.Any())
                .ToDictionary(x => x.Id, y => y.Relationships.FirstOrDefault()?.Work.Id);
            WorkRecordings = works.Where(x => x.Relationships.Any()).ToDictionary(x => x.Id,
                y => y.Relationships.Select(z => z.Recording));

            foreach (var lyrics in lyricsDictionary)
                Works[lyrics.Key].Lyrics = lyrics.Value;

            ProcessLyricsAndRecordingData();
            RemoveWorksWithTooFewWords(ShortWorkThreshold);
            ProcessStats();
        }

        public ArtistDto Artist { get; set; }
        public IDictionary<Guid, RecordingViewModel> Recordings { get; set; }

        public IDictionary<Guid, Guid?> RecordingWorks { get; set; }
        public IDictionary<Guid, IEnumerable<RecordingDto>> WorkRecordings { get; set; }
        public IDictionary<Guid, WorkViewModel> Works { get; set; }

        private void ProcessLyricsAndRecordingData()
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

        private void ProcessStats()
        {
            if (!Works.Values.Any())
            {
                Total = 0;
                Average = 0;
                WordsPerSecond = 0;
                Min = 0;
                Max = 0;
                Variance = 0;
                StandardDeviation = 0;

                return;
            }

            Total = Works.Values.Sum(x => x.NumberOfWords);
            Average = Works.Values.Any() ? Works.Values.Average(x => x.NumberOfWords) : 0;
            WordsPerSecond = Works.Values.Average(x => x.WordsPerSecond);
            Min = Works.Values.Min(x => x.NumberOfWords);
            Max = Works.Values.Max(x => x.NumberOfWords);
            Variance = Works.Values.Average(x => Math.Pow(x.NumberOfWords - Average, 2));
            StandardDeviation = Math.Sqrt(Variance);
        }

        #endregion LyricData
    }
}