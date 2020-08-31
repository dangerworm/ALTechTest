namespace ALTechTest.ParsingObjects.MusicBrainz
{
    public class WorkQueryResult
    {
        public string error { get; set; }
        public string help { get; set; }
        public int workcount { get; set; }
        public int workoffset { get; set; }
        public Work[] works { get; set; }
    }
}