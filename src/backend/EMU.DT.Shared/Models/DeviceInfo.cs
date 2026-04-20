
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EMU.DT.Shared.Enums;

namespace EMU.DT.Shared.Models;

[Table("device_info")]
public class DeviceInfo : BaseEntity
{
    [Column("name")]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [Column("device_type")]
    public DeviceType DeviceType { get; set; } = DeviceType.Other;

    [Column("model")]
    [MaxLength(100)]
    public string? Model { get; set; }

    [Column("device_no")]
    [MaxLength(50)]
    public string? DeviceNo { get; set; }

    [Column("depot_id")]
    public long? DepotId { get; set; }

    [Column("platform_id")]
    public long? PlatformId { get; set; }

    [Column("status")]
    public DeviceStatus Status { get; set; } = DeviceStatus.Normal;

    [Column("manufacturer")]
    [MaxLength(100)]
    public string? Manufacturer { get; set; }

    [Column("manufacturing_date")]
    public DateTime? ManufacturingDate { get; set; }

    [Column("installation_date")]
    public DateTime? InstallationDate { get; set; }

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
