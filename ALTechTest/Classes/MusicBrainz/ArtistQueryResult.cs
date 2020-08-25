using System;

namespace ALTechTest.Classes.MusicBrainz
{
    public class ArtistQueryResult
    {
        public Artist[] artists { get; set; }
        public int count { get; set; }
        public DateTime created { get; set; }
        public int offset { get; set; }
    }
}