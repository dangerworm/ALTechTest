using System.Net.Http;

namespace ALTechTest.ServiceCallers
{
    public class MusicBrainzCallerBase
    {
        protected const string BaseAddress = "http://musicbrainz.org/ws/2/";
        protected const string FormatJson = "fmt=json";
        protected const string IncludeRecordingRelationships = "inc=recording-rels";
        protected const string IncludeWorkRelationships = "inc=work-rels";
        protected const string Limit1000 = "limit=1000";
        protected const string Offset = "offset=0";
        protected const string UserAgent = "ALTechTest/0.0.0.1 (dangerworm@gmail.com)";

        protected const string ArtistEntityString = "artist";
        protected const string RecordingEntityString = "recording";
        protected const string WorkEntityString = "work";

        protected HttpClient GetHttpClient()
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("User-Agent", UserAgent);

            return client;
        }
    }
}