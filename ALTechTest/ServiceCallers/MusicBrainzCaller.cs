using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ALTechTest.Classes.MusicBrainz;
using ALTechTest.Interfaces;
using Newtonsoft.Json;

namespace ALTechTest.ServiceCallers
{
    public class MusicBrainzCaller : MusicBrainzCallerBase, IMusicBrainzCaller
    {
        public async Task<IEnumerable<Artist>> GetArtists(string query)
        {
            var parameters = new[] {JsonFormat, $"query={query}"};
            var requestUri = $"{BaseAddress}{ArtistEntityString}?{string.Join('&', parameters)}";

            using var httpClient = GetHttpClient();
            using var response = await httpClient.GetAsync(requestUri);
            var apiResponseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ArtistQueryResult>(apiResponseString);

            return result.artists;
        }

        public async Task<Artist> GetArtistById(Guid musicBrainzId)
        {
            var requestUri = $"{BaseAddress}{ArtistEntityString}/{musicBrainzId}?{JsonFormat}";

            using var httpClient = GetHttpClient();
            using var response = await httpClient.GetAsync(requestUri);
            var apiResponseString = await response.Content.ReadAsStringAsync();
            var artist = JsonConvert.DeserializeObject<Artist>(apiResponseString);

            return artist;
        }

        public async Task<IEnumerable<Work>> GetWorksByArtistId(Guid artistMusicBrainzId)
        {
            var parameters = new[] {JsonFormat, $"{ArtistEntityString}={artistMusicBrainzId}", Limit1000, Offset};
            var requestUri = $"{BaseAddress}{WorkEntityString}?{string.Join('&', parameters)}";

            using var httpClient = GetHttpClient();
            using var response = await httpClient.GetAsync(requestUri);
            var apiResponseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<WorkQueryResult>(apiResponseString);

            return result.works;
        }
    }
}