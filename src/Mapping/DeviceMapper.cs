
public static class DeviceMapper {

    public static List<Entity> MapDeviceToEntity(Device device) {
        
        var entities = new List<Entity>
        {
            new Entity() { EntityId = device.EntityName, Name = "Temperature", Value = device.Temperature?.Celsius?.ToString() },
            new Entity() { EntityId = device.EntityName, Name = "SignalStrength", Value = device.Cellular?.SignalStrength },
            new Entity() { EntityId = device.EntityName, Name = "DBM", Value = device.Cellular?.Dbm.ToString() },
            new Entity() { EntityId = device.EntityName, Name = "Humidity", Value = device.Humidity?.Percentage.ToString() },
            new Entity() { EntityId = device.EntityName, Name = "Light", Value = device.Light?.Lux.ToString() },
            new Entity() { EntityId = device.EntityName, Name = "Battery", Value = device.Battery?.Percentage.ToString() },
            new Entity() { EntityId = device.EntityName, Name = "Location", Value = $"{device.Location?.Latitude},{device.Location?.Longitude}" },
            new Entity() { EntityId = device.EntityName, Name = "Address", Value = device.Location?.FormattedAddress }
        };
        return entities;
    }
}