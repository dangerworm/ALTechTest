using ALTechTest.ParsingObjects.MusicBrainz;

namespace ALTechTest.DataTransferObjects
{
    public class RelationshipDto
    {
        public RelationshipDto(Relationship relationship)
        {
            if (relationship.recording != null) Recording = new RecordingDto(relationship.recording);

            if (relationship.work != null) Work = new WorkDto(relationship.work);
        }

        public RecordingDto Recording { get; set; }
        public WorkDto Work { get; set; }
    }
}