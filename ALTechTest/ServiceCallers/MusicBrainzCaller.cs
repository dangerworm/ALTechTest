using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ALTechTest.Classes.MusicBrainz;
using ALTechTest.DataTransferObjects;
using ALTechTest.Interfaces;
using ALTechTest.ParsingObjects.MusicBrainz;
using Newtonsoft.Json;

namespace ALTechTest.ServiceCallers
{
    public class MusicBrainzCaller : MusicBrainzCallerBase, IMusicBrainzCaller
    {
        private const int ArtistSearchScoreThreshold = 50;

        public async Task<IEnumerable<ArtistDto>> GetArtists(string query)
        {
            var parameters = new[] {FormatJson, $"query={query}"};
            var requestUri = $"{BaseAddress}{ArtistEntityString}?{string.Join('&', parameters)}";

            using var httpClient = GetHttpClient();
            using var response = await httpClient.GetAsync(requestUri);
            var apiResponseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ArtistQueryResult>(apiResponseString);

            var matchingArtists = result.artists.Where(x => x.score > ArtistSearchScoreThreshold);
            return matchingArtists.Select(x => new ArtistDto(x));
        }

        public async Task<ArtistDto> GetArtistById(Guid musicBrainzId)
        {
            var requestUri = $"{BaseAddress}{ArtistEntityString}/{musicBrainzId}?{FormatJson}";

            using var httpClient = GetHttpClient();
            using var response = await httpClient.GetAsync(requestUri);
            var apiResponseString = await response.Content.ReadAsStringAsync();
            var artist = JsonConvert.DeserializeObject<Artist>(apiResponseString);

            return new ArtistDto(artist);
        }

        public async Task<IEnumerable<WorkDto>> GetWorksByArtistId(Guid artistMusicBrainzId)
        {
            var parameters = new[]
            {
                $"{ArtistEntityString}={artistMusicBrainzId}", IncludeRecordingRelationships, FormatJson, Limit1000,
                Offset
            };
            var requestUri = $"{BaseAddress}{WorkEntityString}?{string.Join('&', parameters)}";

            var apiResponseString = await GetApiResponseString(requestUri);
            var result = JsonConvert.DeserializeObject<WorkQueryResult>(apiResponseString);

            return result.works.Select(x => new WorkDto(x));
        }

        public async Task<IEnumerable<RecordingDto>> GetRecordingsByArtistId(Guid artistMusicBrainzId)
        {
            var parameters = new[]
            {
                $"{ArtistEntityString}={artistMusicBrainzId}", FormatJson, IncludeWorkRelationships, Limit1000, Offset
            };
            var requestUri = $"{BaseAddress}{RecordingEntityString}?{string.Join('&', parameters)}";

            var apiResponseString = await GetApiResponseString(requestUri);
            var result = JsonConvert.DeserializeObject<RecordingQueryResult>(apiResponseString);

            return result.recordings.Select(x => new RecordingDto(x));
        }

        private async Task<string> GetApiResponseString(string requestUri)
        {
            using var httpClient = GetHttpClient();
            using var response = await httpClient.GetAsync(requestUri);
            return await response.Content.ReadAsStringAsync();
        }
    }
}