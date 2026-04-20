
using EMU.DT.Shared.Enums;

namespace EMU.DT.Shared.DTOs;

public class EmuDto
{
    public long Id { get; set; }
    public EmuType EmuType { get; set; }
    public string EmuSeries { get; set; } = string.Empty;
    public string TrainSetNo { get; set; } = string.Empty;
    public DateTime ManufacturingDate { get; set; }
    public long? DepotId { get; set; }
    public EmuStatus Status { get; set; }
    public decimal? Mileage { get; set; }
    public string? Remark { get; set; }
    public DateTime CreatedAt { get; set; }
}
