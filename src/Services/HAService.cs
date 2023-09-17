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
    public async Task PostToHA(Device device) {
        if (device == null) {
            return;
        }
        await PostEntity("Temperature", device.EntityName, device.Temperature?.Celsius,"temperature","sensor", "C","measurement");
        await PostEntity("SignalStrength", device.EntityName, device.Cellular?.SignalStrength,"enum","sensor");
        await PostEntity("DBM", device.EntityName, device.Cellular?.Dbm,"signal_strength","sensor","dBm","measurement");
        await PostEntity("Humidity", device.EntityName, device.Humidity?.Percentage, "sensor","humidity","%","measurement");
        await PostEntity("Light", device.EntityName, device.Light?.Lux,"illuminance", "sensor","lx","measurement");
        await PostEntity("Battery", device.EntityName,  device.Battery?.Percentage,"battery","sensor","%","measurement");
        await PostEntity("Location", device.EntityName, $"{device.Location?.Latitude},{device.Location?.Longitude}",null,"device_tracker");
        await PostEntity("Address", device.EntityName, device.Location?.FormattedAddress,"enum","sensor");
    }

    private async Task PostEntity(string name, string entityId, object value, string? deviceClass,string deviceType, string? unitOfMeasurement = null, string? stateClass = null) {

        using StringContent jsonContent = new(
                JsonSerializer.Serialize(new
                {
                    state = value,
                    attributes = new {
                        device_class = deviceClass,
                        state_class = stateClass,
                        unit_of_measurement = unitOfMeasurement
                    }
                },new JsonSerializerOptions()
                {
                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
                }),
                Encoding.UTF8,
                "application/json");

        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _config["HAToken"]);
        var result = await _client.PostAsync($"{_config["HABaseURL"]}/api/states/{deviceType}.solo_{entityId}_{name}", jsonContent);
    }
}