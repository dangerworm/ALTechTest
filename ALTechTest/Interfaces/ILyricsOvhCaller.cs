using ALTechTest.DataTransferObjects;
using System.Threading.Tasks;

namespace ALTechTest.Interfaces
{
    public interface ILyricsOvhCaller
    {
        public Task<LyricsDto> GetLyrics(string artist, string title);
    }
}