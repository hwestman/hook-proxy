public static class DeviceExtensions{
    public static List<Entity>ToEntities(this Device device){
        List<Entity> entities = new()
        {
            new Sensor()
            {
                DeviceType = "sensor",
                EntityId = device.EntityName,
                Name = "Temperature",
                State = device.Temperature?.Celsius.ToString(),
                DeviceClass = "temperature",
                UnitOfMeasurement = "C",
                StateClass = "measurement"
            },
            
            new Sensor(){
                DeviceType = "sensor",
                EntityId = device.EntityName,
                Name = "Humidity",
                State = device.Humidity?.Percentage.ToString(),
                DeviceClass = "humidity",
                UnitOfMeasurement = "%",
                StateClass = "measurement"
            },
            new Sensor(){
                DeviceType = "sensor",
                EntityId = device.EntityName,
                Name = "Light",
                State = device.Light?.Lux.ToString(),
                DeviceClass = "illuminance",
                UnitOfMeasurement = "lx",
                StateClass = "measurement"
            },
            new Sensor()
            {
                DeviceType = "sensor",
                EntityId = device.EntityName,
                Name = "SignalStrength",
                State = device.Cellular?.SignalStrength,
                DeviceClass = "signal_strength",
                UnitOfMeasurement = "dBm",
                StateClass = "measurement"
            },
            new Sensor()
            {
                DeviceType = "sensor",
                EntityId = device.EntityName,
                Name = "DBM",
                State = device.Cellular?.Dbm.ToString(),
                DeviceClass = "signal_strength",
                UnitOfMeasurement = "dBm",
                StateClass = "measurement"
            },
            new DeviceTracker(){
                DeviceType = "device_tracker",
                EntityId = device.EntityName,
                Name = "Location",
                State = $"{device.Location?.Latitude},{device.Location?.Longitude}",
                SourceType = "gps",
                BatteryLevel = (int)(device.Battery?.Percentage ?? 0),
                LocationName = device.Location?.FormattedAddress,
                Latitude = (float)(device.Location?.Latitude ?? 0),
                Longitude = (float)(device.Location?.Longitude ?? 0),
            },
        };
        return entities;
    }
}