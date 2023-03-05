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
        await PostEntity("Temperature", device.EntityName, device.Temperature?.Celsius,"temperature", "C");
        await PostEntity("SignalStrength", device.EntityName, device.Cellular?.SignalStrength,"enum");
        await PostEntity("DBM", device.EntityName, device.Cellular?.Dbm,"signal_strength","DBM");
        await PostEntity("Humidity", device.EntityName, device.Humidity?.Percentage, "humidity","%");
        await PostEntity("Light", device.EntityName, device.Light?.Lux,"illuminance", "lx");
        await PostEntity("Battery", device.EntityName,  device.Battery?.Percentage,"battery","%");
        await PostEntity("Location", device.EntityName, $"{device.Location?.Latitude},{device.Location?.Longitude}","enum");
        await PostEntity("Address", device.EntityName, device.Location?.FormattedAddress,"enum");
    }

    private async Task PostEntity(string name, string entityId, object value, string deviceClass, string? unitOfMeasurement = null) {

        using StringContent jsonContent = new(
                JsonSerializer.Serialize(new
                {
                    state = value,
                    attributes = new {
                        device_class = deviceClass,
                        unit_of_measurement = unitOfMeasurement
                    }
                }),
                Encoding.UTF8,
                "application/json");

        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _config["HAToken"]);
        var result = await _client.PostAsync($"{_config["HABaseURL"]}/api/states/sensor.solo_{entityId}_{name}", jsonContent);
    }
}