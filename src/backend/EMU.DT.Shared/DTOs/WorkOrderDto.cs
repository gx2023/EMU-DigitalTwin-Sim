
using EMU.DT.Shared.Enums;

namespace EMU.DT.Shared.DTOs;

public class WorkOrderDto
{
    public long Id { get; set; }
    public string OrderNo { get; set; } = string.Empty;
    public long? EmuId { get; set; }
    public MaintenanceLevel MaintenanceLevel { get; set; }
    public long? DepotId { get; set; }
    public long? PlatformId { get; set; }
    public WorkOrderStatus Status { get; set; }
    public DateTime? PlannedStartTime { get; set; }
    public DateTime? PlannedEndTime { get; set; }
    public DateTime? ActualStartTime { get; set; }
    public DateTime? ActualEndTime { get; set; }
    public long? ManagerId { get; set; }
    public string? Description { get; set; }
    public string? Remark { get; set; }
    public DateTime CreatedAt { get; set; }
}
