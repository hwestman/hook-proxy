using System.Text.Json.Serialization;

public class Sensor : Entity{
    [JsonPropertyName("unit_of_measurement")]
    public string? UnitOfMeasurement;
    [JsonPropertyName("device_class")]
    public string? DeviceClass;
    [JsonPropertyName("state_class")]
    public string? StateClass;
}