using System;

namespace ALTechTest.ParsingObjects.MusicBrainz
{
    public class Area
    {
        public string disambiguation { get; set; }
        public Guid id { get; set; }
        public string[] iso31661codes { get; set; }
        public LifeSpan lifespan { get; set; }
        public string name { get; set; }
        public string sortname { get; set; }
        public string type { get; set; }
        public Guid typeid { get; set; }
    }
}