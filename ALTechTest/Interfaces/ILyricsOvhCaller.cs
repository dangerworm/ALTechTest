using System.Threading.Tasks;

namespace ALTechTest.Interfaces
{
    public interface ILyricsOvhCaller
    {
        public Task<string> GetLyrics(string artist, string title);
    }
}