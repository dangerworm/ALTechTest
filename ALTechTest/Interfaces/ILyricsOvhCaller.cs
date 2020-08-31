using System.Threading.Tasks;
using ALTechTest.DataTransferObjects;

namespace ALTechTest.Interfaces
{
    public interface ILyricsOvhCaller
    {
        public Task<LyricsDto> GetLyrics(string artist, string title);
    }
}