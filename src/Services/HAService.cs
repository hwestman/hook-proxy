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
    public async Task PostToHA(Device device) {
        if (device == null) {
            return;
        }
        await PostEntity("Temperature", device.EntityName, device.Temperature?.Celsius);
        await PostEntity("SignalStrength", device.EntityName, device.Cellular?.SignalStrength);
        await PostEntity("DBM", device.EntityName, device.Cellular?.Dbm);
        await PostEntity("Humidity", device.EntityName, device.Humidity?.Percentage);
        await PostEntity("Light", device.EntityName, device.Light?.Lux);
        await PostEntity("Battery", device.EntityName,  device.Battery?.Percentage);
        await PostEntity("Location", device.EntityName, $"{device.Location?.Latitude},{device.Location?.Longitude}");
        await PostEntity("Address", device.EntityName, device.Location?.FormattedAddress);
    }

    private async Task PostEntity(string name, string entityId, object value) {

        using StringContent jsonContent = new(
                JsonSerializer.Serialize(new
                {
                    state = value
                }),
                Encoding.UTF8,
                "application/json");

        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _config["HAToken"]);
        var result = await _client.PostAsync($"{_config["HABaseURL"]}/api/states/sensor.{entityId}_{name}", jsonContent);
    }
}