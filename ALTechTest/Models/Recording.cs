using System;

namespace ALTechTest.Models
{
    public class Recording
    {
        public Recording()
        {
        }

        public Recording(Classes.MusicBrainz.Recording recording)
        {
            Id = recording.id;
            Title = recording.title;
            Length = recording.length;
            Disambiguation = recording.disambiguation;
        }

        public string Disambiguation { get; set; }

        public Guid Id { get; set; }
        public int? Length { get; set; }
        public string Title { get; set; }
    }
}