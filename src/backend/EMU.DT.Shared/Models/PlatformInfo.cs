
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EMU.DT.Shared.Enums;

namespace EMU.DT.Shared.Models;

[Table("platform_info")]
public class PlatformInfo : BaseEntity
{
    [Column("name")]
    [MaxLength(50)]
    public string Name { get; set; } = string.Empty;

    [Column("depot_id")]
    public long? DepotId { get; set; }

    [Column("track_id")]
    public long? TrackId { get; set; }

    [Column("workshop_type")]
    public WorkshopType WorkshopType { get; set; }

    [Column("status")]
    public PlatformStatus Status { get; set; } = PlatformStatus.Idle;

    [Column("length")]
    [Precision(10, 2)]
    public decimal Length { get; set; }

    [Column("width")]
    [Precision(10, 2)]
    public decimal Width { get; set; }

    [Column("position_x")]
    [Precision(10, 2)]
    public decimal PositionX { get; set; }

    [Column("position_y")]
    [Precision(10, 2)]
    public decimal PositionY { get; set; }

    [Column("position_z")]
    [Precision(10, 2)]
    public decimal PositionZ { get; set; }

    [Column("model_path")]
    [MaxLength(500)]
    public string? ModelPath { get; set; }

    [Column("remark")]
    [MaxLength(500)]
    public string? Remark { get; set; }
}
