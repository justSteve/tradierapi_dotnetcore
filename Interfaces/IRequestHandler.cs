
namespace Tradier.Interfaces
{

    public interface IRequestHandler
    {
        Task<string> GetRequest(string endpoint);
    }

    public class RequestHandler : IRequestHandler
    {
        private readonly HttpClient _httpClient;

        public RequestHandler(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GetRequest(string endpoint)
        {
            var response = await _httpClient.GetAsync(endpoint);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
    }
}