using System;

namespace ALTechTest.Classes.MusicBrainz
{
    public class Medium
    {
        public string format { get; set; }
        public Guid formatid { get; set; }
        public int position { get; set; }
        public string title { get; set; }
        public int trackcount { get; set; }
    }
}