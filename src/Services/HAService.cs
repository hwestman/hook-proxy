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
        await PostEntity("Temperature", device.EntityName, device.Temperature?.Celsius,"temperature");
        await PostEntity("SignalStrength", device.EntityName, device.Cellular?.SignalStrength,"enum");
        await PostEntity("DBM", device.EntityName, device.Cellular?.Dbm,"signal_strength");
        await PostEntity("Humidity", device.EntityName, device.Humidity?.Percentage, "humidity");
        await PostEntity("Light", device.EntityName, device.Light?.Lux,"illuminance");
        await PostEntity("Battery", device.EntityName,  device.Battery?.Percentage,"battery");
        await PostEntity("Location", device.EntityName, $"{device.Location?.Latitude},{device.Location?.Longitude}","enum");
        await PostEntity("Address", device.EntityName, device.Location?.FormattedAddress,"enum");
    }

    private async Task PostEntity(string name, string entityId, object value, string deviceClass) {

        using StringContent jsonContent = new(
                JsonSerializer.Serialize(new
                {
                    state = value,
                    attributes = new {
                        device_class = deviceClass
                    }
                }),
                Encoding.UTF8,
                "application/json");

        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _config["HAToken"]);
        var result = await _client.PostAsync($"{_config["HABaseURL"]}/api/states/sensor.{entityId}_{name}", jsonContent);
    }
}