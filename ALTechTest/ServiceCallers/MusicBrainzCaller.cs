using ALTechTest.Classes.MusicBrainz;
using ALTechTest.DataTransferObjects;
using ALTechTest.Interfaces;
using ALTechTest.ParsingObjects.MusicBrainz;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using ALTechTest.Helpers;

namespace ALTechTest.ServiceCallers
{
    public class MusicBrainzCaller : IMusicBrainzCaller
    {
        private const string BaseAddress = "http://musicbrainz.org/ws/2/";
        private const string FormatJson = "fmt=json";
        private const string IncludeRecordingRelationships = "inc=recording-rels";
        private const string IncludeWorkRelationships = "inc=work-rels";
        private const string Limit1000 = "limit=1000";
        private const string Offset = "offset=0";

        private const string ArtistEntityString = "artist";
        private const string RecordingEntityString = "recording";
        private const string WorkEntityString = "work";

        private const int ArtistSearchScoreThreshold = 50;
        
        private readonly IServiceCaller _serviceCaller;

        public MusicBrainzCaller(IServiceCaller serviceCaller)
        {
            Verify.NotNull(serviceCaller, nameof(serviceCaller));

            _serviceCaller = serviceCaller;
        }

        public async Task<IEnumerable<ArtistDto>> GetArtists(string query)
        {
            var parameters = new[]
            {
                FormatJson, $"query={HttpUtility.UrlEncode(query)}"
            };
            
            try
            {
                var requestUri = $"{BaseAddress}{ArtistEntityString}?{string.Join('&', parameters)}";
                var apiResponseString = await _serviceCaller.GetApiResponseString(requestUri);
                var result = JsonConvert.DeserializeObject<ArtistQueryResult>(apiResponseString);

                var matchingArtists = result.artists.Where(x => x.score > ArtistSearchScoreThreshold);
                return matchingArtists.Select(x => new ArtistDto(x));
            }
            catch (JsonReaderException)
            {
                return Enumerable.Empty<ArtistDto>();
            }
        }

        public async Task<ArtistDto> GetArtistById(Guid musicBrainzId)
        {
            try
            {
                var requestUri = $"{BaseAddress}{ArtistEntityString}/{musicBrainzId}?{FormatJson}";
                var apiResponseString = await _serviceCaller.GetApiResponseString(requestUri);
                var artist = JsonConvert.DeserializeObject<Artist>(apiResponseString);

                return new ArtistDto(artist);
            }
            catch (JsonReaderException)
            {
                return null;
            }
        }

        public async Task<IEnumerable<WorkDto>> GetWorksByArtistId(Guid artistMusicBrainzId)
        {
            var parameters = new[]
            {
                $"{ArtistEntityString}={artistMusicBrainzId}", IncludeRecordingRelationships, FormatJson, Limit1000,
                Offset
            };

            try
            {
                var requestUri = $"{BaseAddress}{WorkEntityString}?{string.Join('&', parameters)}";
                var apiResponseString = await _serviceCaller.GetApiResponseString(requestUri);
                var result = JsonConvert.DeserializeObject<WorkQueryResult>(apiResponseString);

                return result.works.Select(x => new WorkDto(x));
            }
            catch (JsonReaderException)
            {
                return Enumerable.Empty<WorkDto>();
            }
        }

        public async Task<IEnumerable<RecordingDto>> GetRecordingsByArtistId(Guid artistMusicBrainzId)
        {
            var parameters = new[]
            {
                $"{ArtistEntityString}={artistMusicBrainzId}", FormatJson, IncludeWorkRelationships, Limit1000, Offset
            };

            try
            {
                var requestUri = $"{BaseAddress}{RecordingEntityString}?{string.Join('&', parameters)}";
                var apiResponseString = await _serviceCaller.GetApiResponseString(requestUri);
                var result = JsonConvert.DeserializeObject<RecordingQueryResult>(apiResponseString);

                return result.recordings.Select(x => new RecordingDto(x));
            }
            catch (JsonReaderException)
            {
                return Enumerable.Empty<RecordingDto>();
            }
        }
    }
}