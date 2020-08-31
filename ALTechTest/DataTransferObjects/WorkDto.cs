using System;
using System.Collections.Generic;
using System.Linq;
using ALTechTest.ParsingObjects.MusicBrainz;

namespace ALTechTest.DataTransferObjects
{
    public class WorkDto
    {
        public WorkDto()
        {
        }

        public WorkDto(Work work)
        {
            Id = work.id;
            Title = work.title;
            Type = work.type;
            Disambiguation = work.disambiguation;

            Relationships = work.relations?
                .Where(x => x != null)
                .Select(x => new RelationshipDto(x));
        }

        public string Disambiguation { get; set; }
        public Guid Id { get; set; }
        public IEnumerable<RelationshipDto> Relationships { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
    }
}