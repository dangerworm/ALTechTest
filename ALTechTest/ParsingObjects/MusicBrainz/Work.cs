﻿using System;

namespace ALTechTest.ParsingObjects.MusicBrainz
{
    public class Work
    {
        public Attribute[] attributes { get; set; }
        public string disambiguation { get; set; }
        public Guid id { get; set; }
        public string[] iswcs { get; set; }
        public string language { get; set; }
        public string[] languages { get; set; }
        public Relationship[] relations { get; set; }
        public string title { get; set; }
        public string type { get; set; }
        public Guid typeid { get; set; }
    }
}