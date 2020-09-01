using System.Threading.Tasks;
using ALTechTest.DataTransferObjects;
using ALTechTest.Helpers;
using ALTechTest.Interfaces;
using ALTechTest.ParsingObjects.LyricsOvh;
using Newtonsoft.Json;

namespace ALTechTest.ServiceCallers
{
    public class LyricsOvhCaller : ILyricsOvhCaller
    {
        private const string BaseAddress = "https://api.lyrics.ovh/v1/";

        private readonly IServiceCaller _serviceCaller;

        public LyricsOvhCaller(IServiceCaller serviceCaller)
        {
            Verify.NotNull(serviceCaller, nameof(serviceCaller));

            _serviceCaller = serviceCaller;
        }

        public async Task<LyricsDto> GetLyrics(string artist, string title)
        {
            try
            {
                var requestUri = $"{BaseAddress}{artist}/{title}";
                var lyricsResponseString = await _serviceCaller.GetApiResponseString(requestUri);

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