using System.Threading.Tasks;
using ALTechTest.Interfaces;

namespace ALTechTest.ServiceCallers
{
    public class LyricsOvhCaller : LyricsOvhCallerBase, ILyricsOvhCaller
    {
        public async Task<string> GetLyrics(string artist, string title)
        {
            var requestUri = $"{BaseAddress}/{artist}/{title}";

            using var httpClient = GetHttpClient();
            using var response = await httpClient.GetAsync(requestUri);
            var result = await response.Content.ReadAsStringAsync();

            return result;
        }
    }
}
