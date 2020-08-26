using System.Net.Http;

namespace ALTechTest.ServiceCallers
{
    public class LyricsOvhCallerBase
    {
        protected const string BaseAddress = "https://api.lyrics.ovh/v1/";
        protected const string UserAgent = "ALTechTest/0.0.0.1 (dangerworm@gmail.com)";

        protected HttpClient GetHttpClient()
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("User-Agent", UserAgent);

            return client;
        }
    }
}