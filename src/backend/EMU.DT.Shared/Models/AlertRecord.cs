
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EMU.DT.Shared.Enums;

namespace EMU.DT.Shared.Models;

[Table("alert_record")]
public class AlertRecord : BaseEntity
{
    [Column("alert_level")]
    public AlertLevel AlertLevel { get; set; } = AlertLevel.Warning;

    [Column("alert_type")]
    [MaxLength(50)]
    public string AlertType { get; set; } = string.Empty;

    [Column("device_id")]
    public long? DeviceId { get; set; }

    [Column("emu_id")]
    public long? EmuId { get; set; }

    [Column("depot_id")]
    public long? DepotId { get; set; }

    [Column("title")]
    [MaxLength(200)]
    public string Title { get; set; } = string.Empty;

    [Column("content")]
    [MaxLength(1000)]
    public string? Content { get; set; }

    [Column("is_handled")]
    public bool IsHandled { get; set; } = false;

    [Column("handled_at")]
    public DateTime? HandledAt { get; set; }

    [Column("handled_by")]
    public long? HandledBy { get; set; }

    [Column("handling_remark")]
    [MaxLength(500)]
    public string? HandlingRemark { get; set; }
}
