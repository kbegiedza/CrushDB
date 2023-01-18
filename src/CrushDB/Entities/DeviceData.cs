namespace CrushDB.Entities
{
    public class DeviceData
    {
        public Guid Id { get; set; }

        public Guid StationId { get; set; }

        public DateTimeOffset Timestamp { get; set; }
    }
}