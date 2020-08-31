using System;

namespace ALTechTest.ParsingObjects.MusicBrainz
{
    public class Release
    {
        public string barcode { get; set; }
        public string country { get; set; }
        public string date { get; set; }
        public string disambiguation { get; set; }
        public Guid id { get; set; }
        public Medium[] media { get; set; }
        public string packaging { get; set; }
        public Guid packagingid { get; set; }
        public string quality { get; set; }
        public ReleaseEvents[] releaseevents { get; set; }
        public string status { get; set; }
        public Guid statusid { get; set; }
        public TextRepresentation textrepresentation { get; set; }
        public string title { get; set; }
    }
}