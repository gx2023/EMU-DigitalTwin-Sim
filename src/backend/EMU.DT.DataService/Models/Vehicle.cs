namespace EMU.DT.DataService.Models
{
    public class Vehicle
    {
        public int Id { get; set; }
        public string Model { get; set; }
        public string Number { get; set; }
        public string Type { get; set; }
        public long TotalMileage { get; set; }
        public DateTime LastMaintenanceDate { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }

        public ICollection<VehicleStatus> Statuses { get; set; }
        public ICollection<MaintenanceTask> MaintenanceTasks { get; set; }
    }
}