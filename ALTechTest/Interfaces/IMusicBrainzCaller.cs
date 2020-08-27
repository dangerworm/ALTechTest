using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ALTechTest.Classes.MusicBrainz;

namespace ALTechTest.Interfaces
{
    public interface IMusicBrainzCaller
    {
        public Task<Artist> GetArtistById(Guid musicBrainzId);
        public Task<IEnumerable<Artist>> GetArtists(string query);

        public Task<IEnumerable<Recording>> GetRecordingsByArtistId(Guid artistMusicBrainzId);
        public Task<IEnumerable<Work>> GetWorksByArtistId(Guid artistMusicBrainzId);
    }
}