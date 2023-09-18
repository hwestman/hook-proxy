
using System.Text.Json.Serialization;

[JsonDerivedType(typeof(DeviceTracker))]
[JsonDerivedType(typeof(Sensor))]
public abstract class Entity {
    [JsonIgnore]
    public string? Name;
    [JsonIgnore]
    public string? State;
    [JsonIgnore]
    public string? EntityId;
    [JsonIgnore]
    public string? DeviceType;
}