using System;
using System.Collections.Generic;
using System.Linq;
using ALTechTest.DataTransferObjects;

namespace ALTechTest.ViewModels
{
    public class RecordingViewModel
    {
        public RecordingViewModel()
        {
        }

        public RecordingViewModel(RecordingDto recording)
        {
            Id = recording.Id;
            Title = recording.Title;
            Length = recording.Length;
            Disambiguation = recording.Disambiguation;

            Relationships = recording.Relationships.Select(x => new RelationshipViewModel(x));
        }

        public string Disambiguation { get; set; }

        public Guid Id { get; set; }
        public int? Length { get; set; }

        public IEnumerable<RelationshipViewModel> Relationships { get; set; }
        public string Title { get; set; }
    }
}