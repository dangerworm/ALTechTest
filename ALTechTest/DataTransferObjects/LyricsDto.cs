using ALTechTest.ParsingObjects.LyricsOvh;

namespace ALTechTest.DataTransferObjects
{
    public class LyricsDto
    {
        public LyricsDto(LyricsQueryResult result)
        {
            Error = result.error;
            Lyrics = result.lyrics;
        }

        public string Error { get; set; }
        public string Lyrics { get; set; }
    }
}