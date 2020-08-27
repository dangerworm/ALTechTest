using System;

namespace ALTechTest.Classes.MusicBrainz
{
    public class Recording
    {
        public string disambiguation { get; set; }
        public Guid id { get; set; }
        public int? length { get; set; }
        public Relationship[] relations { get; set; }
        public string title { get; set; }
        public bool video { get; set; }
    }
}