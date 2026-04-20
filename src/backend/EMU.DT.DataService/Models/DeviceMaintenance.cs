namespace EMU.DT.DataService.Models
{
    public class DeviceMaintenance
    {
        public int Id { get; set; }
        public int DeviceId { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Status { get; set; }
        public string Technician { get; set; }
        public string Notes { get; set; }

        public Device Device { get; set; }
    }
}