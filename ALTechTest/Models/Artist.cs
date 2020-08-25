using System;

namespace ALTechTest.Models
{
    public class Artist
    {
        public Artist(Classes.MusicBrainz.Artist artist)
        {
            Id = artist.id;
            Name = artist.name;
            Type = artist.type;
            Disambiguation = artist.disambiguation;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Disambiguation { get; set; }
    }
}