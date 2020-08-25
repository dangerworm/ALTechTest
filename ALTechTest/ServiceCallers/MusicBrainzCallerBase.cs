using System;
using System.Net.Http;

namespace ALTechTest.ServiceCallers
{
    public class MusicBrainzCallerBase
    {
        protected const string BaseAddress = "http://musicbrainz.org/ws/2/";
        protected const string ArtistEntityString = "artist";
        protected const string WorkEntityString = "work";
        protected const string JsonFormat = "fmt=json";
        protected const string Limit1000 = "limit=1000";
        protected const string Offset = "offset=0";
        protected const string UserAgent = "ALTechTest/0.0.0.1 (dangerworm@gmail.com)";

        protected HttpClient GetHttpClient()
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("User-Agent", UserAgent);

            return client;
        }
    }
}