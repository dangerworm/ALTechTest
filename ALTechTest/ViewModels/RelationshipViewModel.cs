using ALTechTest.DataTransferObjects;

namespace ALTechTest.ViewModels
{
    public class RelationshipViewModel
    {
        public RelationshipViewModel(RelationshipDto relationship)
        {
            if (relationship.Recording != null) Recording = new RecordingViewModel(relationship.Recording);

            if (relationship.Work != null) Work = new WorkViewModel(relationship.Work);
        }

        public RecordingViewModel Recording { get; set; }
        public WorkViewModel Work { get; set; }
    }
}