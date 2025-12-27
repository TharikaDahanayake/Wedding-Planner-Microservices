namespace GuestService.Services
{

    public interface IEventClient_D_BIT_23_0030 { Task<bool> EventExistsAsync(int id); }
    public class EventClient_D_BIT_23_0030 : IEventClient_D_BIT_23_0030
    {
        private readonly HttpClient _http;
        public EventClient_D_BIT_23_0030(HttpClient http) { _http = http; }
        public async Task<bool> EventExistsAsync(int id)
        {
            var resp = await _http.GetAsync($"/events/{id}");
            return resp.IsSuccessStatusCode;
        }
    }
}
