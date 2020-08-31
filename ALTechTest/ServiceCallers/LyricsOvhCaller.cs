using System.Threading.Tasks;
using ALTechTest.DataTransferObjects;
using ALTechTest.Interfaces;
using ALTechTest.ParsingObjects.LyricsOvh;
using Newtonsoft.Json;

namespace ALTechTest.ServiceCallers
{
    public class LyricsOvhCaller : LyricsOvhCallerBase, ILyricsOvhCaller
    {
        public async Task<LyricsDto> GetLyrics(string artist, string title)
        {
            var requestUri = $"{BaseAddress}/{artist}/{title}";

            using var httpClient = GetHttpClient();
            using var response = await httpClient.GetAsync(requestUri);
            var lyricsResponseString = await response.Content.ReadAsStringAsync();

            try
            {
                var result = JsonConvert.DeserializeObject<LyricsQueryResult>(lyricsResponseString);
                return new LyricsDto(result);
            }
            catch (JsonReaderException)
            {
                return null;
            }
        }
    }
}