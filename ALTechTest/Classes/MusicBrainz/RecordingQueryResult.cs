namespace ALTechTest.Classes.MusicBrainz
{
    public class RecordingQueryResult
    {
        public int recordingcount { get; set; }
        public int recordingoffset { get; set; }
        public Recording[] recordings { get; set; }
    }
}