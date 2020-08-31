using ALTechTest.ParsingObjects.MusicBrainz;

namespace ALTechTest.Classes.MusicBrainz
{
    public class RecordingQueryResult
    {
        public string error { get; set; }
        public string help { get; set; }
        public int recordingcount { get; set; }
        public int recordingoffset { get; set; }
        public Recording[] recordings { get; set; }
    }
}