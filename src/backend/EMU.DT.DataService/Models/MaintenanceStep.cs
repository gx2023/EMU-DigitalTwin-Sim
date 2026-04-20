namespace EMU.DT.DataService.Models
{
    public class MaintenanceStep
    {
        public int Id { get; set; }
        public int MaintenanceTaskId { get; set; }
        public int StepNumber { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string Operator { get; set; }
        public string Notes { get; set; }

        public MaintenanceTask MaintenanceTask { get; set; }
        public ICollection<QualityCheck> QualityChecks { get; set; }
    }
}