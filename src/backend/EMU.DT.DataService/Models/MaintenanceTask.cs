namespace EMU.DT.DataService.Models
{
    public class MaintenanceTask
    {
        public int Id { get; set; }
        public int VehicleId { get; set; }
        public int ProcessCardId { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string WorkshopLocation { get; set; }
        public string ResponsiblePerson { get; set; }
        public string Notes { get; set; }

        public Vehicle Vehicle { get; set; }
        public ProcessCard ProcessCard { get; set; }
        public ICollection<MaintenanceStep> Steps { get; set; }
    }
}