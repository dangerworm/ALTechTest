using System;
using ALTechTest.ParsingObjects.MusicBrainz;

namespace ALTechTest.DataTransferObjects
{
    public class ArtistDto
    {
        public ArtistDto(Artist artist)
        {
            Id = artist.id;
            Name = artist.name;
            Type = artist.type;
            Disambiguation = artist.disambiguation;
        }

        public string Disambiguation { get; set; }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
    }
}