namespace EMU.DT.DataService.Models
{
    public class DeviceStatus
    {
        public int Id { get; set; }
        public int DeviceId { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
        public DateTime Timestamp { get; set; }
        public string Operator { get; set; }

        public Device Device { get; set; }
    }
}