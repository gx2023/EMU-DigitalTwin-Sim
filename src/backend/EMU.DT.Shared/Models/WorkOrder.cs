
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EMU.DT.Shared.Enums;

namespace EMU.DT.Shared.Models;

[Table("work_order")]
public class WorkOrder : BaseEntity
{
    [Column("order_no")]
    [MaxLength(50)]
    public string OrderNo { get; set; } = string.Empty;

    [Column("emu_id")]
    public long? EmuId { get; set; }

    [Column("maintenance_level")]
    public MaintenanceLevel MaintenanceLevel { get; set; }

    [Column("depot_id")]
    public long? DepotId { get; set; }

    [Column("platform_id")]
    public long? PlatformId { get; set; }

    [Column("status")]
    public WorkOrderStatus Status { get; set; } = WorkOrderStatus.PendingAssignment;

    [Column("planned_start_time")]
    public DateTime? PlannedStartTime { get; set; }

    [Column("planned_end_time")]
    public DateTime? PlannedEndTime { get; set; }

    [Column("actual_start_time")]
    public DateTime? ActualStartTime { get; set; }

    [Column("actual_end_time")]
    public DateTime? ActualEndTime { get; set; }

    [Column("manager_id")]
    public long? ManagerId { get; set; }

    [Column("description")]
    [MaxLength(1000)]
    public string? Description { get; set; }

    [Column("remark")]
    [MaxLength(500)]
    public string? Remark { get; set; }
}
