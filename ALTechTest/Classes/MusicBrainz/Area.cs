using System;

namespace ALTechTest.Classes.MusicBrainz
{
    public class Area
    {
        public Guid id { get; set; }
        public LifeSpan lifespan { get; set; }
        public string name { get; set; }
        public string sortname { get; set; }
        public string type { get; set; }
        public Guid typeid { get; set; }
    }
}