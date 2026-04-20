namespace EMU.DT.DataService.Models
{
    public class Device
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Location { get; set; }
        public string Model { get; set; }
        public string SerialNumber { get; set; }
        public DateTime InstallDate { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }

        public ICollection<DeviceStatus> Statuses { get; set; }
        public ICollection<DeviceMaintenance> Maintenances { get; set; }
    }
}