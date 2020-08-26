using System;
using System.Linq;
using System.Xml.Schema;

namespace ALTechTest.Models
{
    public class Work
    {
        public Work(Classes.MusicBrainz.Work work)
        {
            Id = work.id;
            Title = work.title;
            Type = work.type;
            Disambiguation = work.disambiguation;
        }

        public string Disambiguation { get; set; }

        public Guid Id { get; set; }
        public string Lyrics { get; set; }

        public int NumberOfWords { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }

        public void ProcessLyricsData()
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
    }
}