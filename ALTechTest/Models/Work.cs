using System;

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

        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public string Disambiguation { get; set; }
        public string Lyrics { get; set; }

        public int NumberOfWords { get; set; }

        public void ProcessLyricsData()
        {
            NumberOfWords = Lyrics.Split(new char[0], StringSplitOptions.RemoveEmptyEntries).Length;
        }
    }
}
