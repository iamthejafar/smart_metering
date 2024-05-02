namespace smart_metering.Models
{


    public class EnergyData
    {
        public int Id { get; set; }
        public required string MeterId { get; set; }
        public required string DataUnit { get; set; }
        public double Value { get; set; }
        public required string Timestamp { get; set; }
    }

    public class PowerData
    {
        public int Id { get; set; }
        public required string MeterId { get; set; }
        public required string DataUnit { get; set; }
        public double Value { get; set; }
        public required string Timestamp { get; set; }
    }

    public class DeviceData
    {
        public required string MeterId { get; set; }
        public required string DeviceId { get; set; }
    }

    public class FlowtemperatureData
    {
        public int Id { get; set; }
        public required string MeterId { get; set; }
        public required string DataUnit { get; set; }
        public double Value { get; set; }
        public required string Timestamp { get; set; }
    }

    public class ReturntemperatureData
    {
        public int Id { get; set; }
        public required string MeterId { get; set; }
        public required string DataUnit { get; set; }
        public double Value { get; set; }
        public required string Timestamp { get; set; }
    }

    public class VolumeData
    {
        public int Id { get; set; }
        public required string MeterId { get; set; }
        public required string DataUnit { get; set; }
        public required double Value { get; set; }
        public required string Timestamp { get; set; }
    }

    public class VolumeflowData
    {
        public int Id { get; set; }
        public required string MeterId { get; set; }
        public required string DataUnit { get; set; }
        public required double Value { get; set; }
        public required string Timestamp { get; set; }
    }
}


