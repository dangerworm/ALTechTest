using System;

namespace ALTechTest.Classes.MusicBrainz
{
    public class Artist
    {
        public Alias[] aliases { get; set; }
        public Area area { get; set; }
        public Area beginarea { get; set; }
        public string country { get; set; }
        public string disambiguation { get; set; }
        public string gender { get; set; }
        public Guid id { get; set; }
        public string[] isnis { get; set; }
        public LifeSpan lifespan { get; set; }
        public string name { get; set; }
        public int score { get; set; }
        public string sortname { get; set; }
        public Tag[] tags { get; set; }
        public string type { get; set; }
        public Guid typeid { get; set; }
    }
}