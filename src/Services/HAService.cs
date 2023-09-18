using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

public class HAService : IHAService {
    
    private readonly HttpClient _client;
    private readonly IConfiguration _config;
    public HAService(IConfiguration config,HttpClient httpClient) {
        _config = config;
        _client = httpClient;
        
    }
    public async Task PostToHA(List<Entity> entities) {

        if (entities == null) {
            return;
        }
        foreach(var entity in entities){
            await PostEntity(entity);
        }
    }

    private async Task PostEntity(Entity entity){

        using StringContent jsonContent = new(  
                JsonSerializer.Serialize(new {
                    state = entity.State,
                    attributes = entity 
                },
                new JsonSerializerOptions()
                {
                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                    WriteIndented = true,
                    IncludeFields = true
                }),
                Encoding.UTF8,
                "application/json");
        var res = await jsonContent.ReadAsStringAsync();
        Console.WriteLine($"Posting string: {res}");

        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _config["HAToken"]);
        await _client.PostAsync($"{_config["HABaseURL"]}/api/states/{entity.DeviceType}.solo_{entity.EntityId}_{entity.Name}", jsonContent);
    }
    
}