using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

public class HAService : IHAService {
    
    private readonly HttpClient _client;
    private readonly IConfiguration _config;
    public HAService(IConfiguration config,HttpClient httpClient) {
        _config = config;
        _client = httpClient;
        
    }
    public async Task PostToHA(List<Entity> entities) {
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _config["HAToken"]);
        foreach (var entity in entities) { 
            
            using StringContent jsonContent = new(
                JsonSerializer.Serialize(new
                {
                    state = entity.Value
                }),
                Encoding.UTF8,
                "application/json");

            var result = await _client.PostAsync($"{_config["HABaseURL"]}/api/states/sensor.{entity.EntityId}_{entity.Name}", jsonContent);

        }
    }
}