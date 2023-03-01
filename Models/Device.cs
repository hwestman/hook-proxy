public class Cellular { 
    public string? SignalStrength { get;  set; }
    public double? Dbm { get;  set; }
}

public class Temperature { 
    public double? Celsius { get;  set; }
    public double? Fahrenheit { get;  set; }
}

public class Pressure { 
    public double? Psi { get;  set; }
    public double? Atmospheric { get;  set; }
}
public class Humidity { 
    public double? Percentage { get;  set; }
}

public class Accelerometer { 
    public double? G { get;  set; }
    public double? X { get;  set; }
    public double? Y { get;  set; }
    public double? Z { get;  set; }
}
public class Light { 
    public double? Lux  { get;  set; }
}
public class Battery { 
    public double? Percentage  { get;  set; }
    public bool? IsCharging  { get;  set; }
}
public class Accuracy
{
    public double? Meters  { get;  set; }
}

public class Location { 
    public double? Latitude  { get;  set; }
    public double? Longitude  { get;  set; }
    public string? FormattedAddress  { get;  set; }
    public Accuracy? Accuracy  { get;  set; }
}

public class Device { 
    public string? EntityName { get;  set; }
    public long? EntryTimeEpoch { get;  set; }
    public DateTime? EntryTimeUtc { get;  set; }
    public Cellular? Cellular { get;  set; }
    public Temperature? Temperature { get;  set; }
    public Humidity? Humidity { get;  set; }
    public Accelerometer? Accelerometer { get;  set; }
    public Light? Light { get;  set; }
    public Battery? Battery { get;  set; }
    public Location? Location { get;  set; }
}