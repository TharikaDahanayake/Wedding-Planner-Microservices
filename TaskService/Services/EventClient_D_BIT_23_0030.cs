using System.Net.Http;
using System.Threading.Tasks;

namespace TaskService.Services
{
    public class EventClient_D_BIT_23_0030 : IEventClient_D_BIT_23_0030
    {
        private readonly HttpClient _httpClient;

        public EventClient_D_BIT_23_0030(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> EventExistsAsync(int id)
        {
            var response = await _httpClient.GetAsync($"/events/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
