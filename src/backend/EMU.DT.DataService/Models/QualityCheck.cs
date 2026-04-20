namespace EMU.DT.DataService.Models
{
    public class QualityCheck
    {
        public int Id { get; set; }
        public int MaintenanceStepId { get; set; }
        public string CheckItem { get; set; }
        public string ExpectedValue { get; set; }
        public string ActualValue { get; set; }
        public string Result { get; set; } // 合格/不合格
        public DateTime CheckTime { get; set; }
        public string Inspector { get; set; }
        public string Notes { get; set; }

        public MaintenanceStep MaintenanceStep { get; set; }
    }
}