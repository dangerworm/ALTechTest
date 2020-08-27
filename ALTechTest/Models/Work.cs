using System;
using System.Collections.Generic;
using System.Linq;

namespace ALTechTest.Models
{
    public class Work
    {
        public Work()
        {
        }

        public Work(Classes.MusicBrainz.Work work)
        {
            Id = work.id;
            Title = work.title;
            Type = work.type;
            Disambiguation = work.disambiguation;
        }

        public TimeSpan? AverageDuration { get; set; }
        public string Disambiguation { get; set; }
        public Guid Id { get; set; }
        public string Lyrics { get; set; }
        public int NumberOfWords { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public double? WordsPerSecond { get; set; }

        private void ProcessLyrics()
        {
            var lines = Lyrics?.Split("\r\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries) ?? new string[0];

            if (lines.Length == 0)
            {
                NumberOfWords = 0;
                return;
            }

            // The lyrics sometimes start with track metadata like the artist, so remove those.
            var linesWithColons = lines.TakeWhile(line => line.Contains(":")).Count();

            Lyrics = string.Join(Environment.NewLine, lines.Skip(linesWithColons));
            NumberOfWords = Lyrics.Split(new char[0], StringSplitOptions.RemoveEmptyEntries).Length;
        }

        public void ProcessLyricsAndRecordingData(Recording[] recordings)
        {
            ProcessLyrics();

            if (recordings != null && recordings.Any())
                ProcessRecordingsData(recordings);
        }

        private void ProcessRecordingsData(IEnumerable<Recording> recordings)
        {
            AverageDuration = TimeSpan.FromMilliseconds(recordings.Average(x => x.Length) ?? 0);
            WordsPerSecond = NumberOfWords / AverageDuration.Value.TotalSeconds;
        }
    }
}