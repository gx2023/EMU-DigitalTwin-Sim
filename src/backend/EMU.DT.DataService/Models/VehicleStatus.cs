namespace EMU.DT.DataService.Models
{
    public class VehicleStatus
    {
        public int Id { get; set; }
        public int VehicleId { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
        public DateTime Timestamp { get; set; }
        public string Operator { get; set; }

        public Vehicle Vehicle { get; set; }
    }
}