using System.Threading.Tasks;
using ALTechTest.Classes.LyricsOvh;
using ALTechTest.Interfaces;
using Newtonsoft.Json;

namespace ALTechTest.ServiceCallers
{
    public class LyricsOvhCaller : LyricsOvhCallerBase, ILyricsOvhCaller
    {
        public async Task<string> GetLyrics(string artist, string title)
        {
            var requestUri = $"{BaseAddress}/{artist}/{title}";

            using var httpClient = GetHttpClient();
            using var response = await httpClient.GetAsync(requestUri);
            var lyricsResponseString = await response.Content.ReadAsStringAsync();

            try
            {
                var result = JsonConvert.DeserializeObject<LyricsQueryResult>(lyricsResponseString);
                return result.lyrics;
            }
            catch (JsonReaderException exception)
            {
                return string.Empty;
            }
        }
    }
}