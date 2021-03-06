﻿using System;

namespace ALTechTest.ParsingObjects.MusicBrainz
{
    public class ArtistQueryResult
    {
        public Artist[] artists { get; set; }
        public int count { get; set; }
        public DateTime created { get; set; }
        public string error { get; set; }
        public string help { get; set; }
        public int offset { get; set; }
    }
}