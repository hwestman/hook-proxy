using System.Text.Json.Serialization;

public class DeviceTracker : Entity{
    [JsonPropertyName("latitude")]
    public float Latitude;
    [JsonPropertyName("longitude")]
    public float Longitude;
    [JsonPropertyName("source_type")]
    public string? SourceType;
    [JsonPropertyName("battery_level")]
    public int BatteryLevel;
    [JsonPropertyName("location_accuracy")]
    public int LocationAccuracy;
    [JsonPropertyName("location_name")]
    public string? LocationName;
}