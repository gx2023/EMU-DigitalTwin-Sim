namespace EMU.DT.DataService.Models
{
    public class ProcessStep
    {
        public int Id { get; set; }
        public int ProcessCardId { get; set; }
        public int StepNumber { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Duration { get; set; } // 预计持续时间（分钟）
        public string Tools { get; set; }
        public string Materials { get; set; }
        public string QualityStandards { get; set; }
        public string SafetyRequirements { get; set; }

        public ProcessCard ProcessCard { get; set; }
    }
}