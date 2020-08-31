using System;
using System.Collections.Generic;
using System.Linq;
using ALTechTest.ParsingObjects.MusicBrainz;

namespace ALTechTest.DataTransferObjects
{
    public class RecordingDto
    {
        public RecordingDto(Recording recording)
        {
            Id = recording.id;
            Title = recording.title;
            Length = recording.length;
            Disambiguation = recording.disambiguation;

            Relationships = recording.relations?
                .Where(x => x != null)
                .Select(x => new RelationshipDto(x));
        }

        public string Disambiguation { get; set; }
        public Guid Id { get; set; }
        public int? Length { get; set; }
        public IEnumerable<RelationshipDto> Relationships { get; set; }
        public string Title { get; set; }
    }
}