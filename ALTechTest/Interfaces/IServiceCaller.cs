using System.Threading.Tasks;

namespace ALTechTest.Interfaces
{
    public interface IServiceCaller
    {
        public Task<string> GetApiResponseString(string requestUri);
    }
}