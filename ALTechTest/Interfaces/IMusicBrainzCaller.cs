using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ALTechTest.DataTransferObjects;

namespace ALTechTest.Interfaces
{
    public interface IMusicBrainzCaller
    {
        public Task<ArtistDto> GetArtistById(Guid musicBrainzId);
        public Task<IEnumerable<ArtistDto>> GetArtists(string query);

        public Task<IEnumerable<RecordingDto>> GetRecordingsByArtistId(Guid artistMusicBrainzId);
        public Task<IEnumerable<WorkDto>> GetWorksByArtistId(Guid artistMusicBrainzId);
    }
}