using System.Net.Http;
using System.Threading.Tasks;
using ALTechTest.Interfaces;

namespace ALTechTest.ServiceCallers
{
    public class ServiceCaller : IServiceCaller
    {
        protected const string UserAgent = "ALTechTest/1.0.0.0 (dangerworm@gmail.com)";

        public async Task<string> GetApiResponseString(string requestUri)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("User-Agent", UserAgent);

            using var response = await httpClient.GetAsync(requestUri);
            return await response.Content.ReadAsStringAsync();
        }
    }
}