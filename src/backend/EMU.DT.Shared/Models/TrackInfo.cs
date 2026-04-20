
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EMU.DT.Shared.Enums;

namespace EMU.DT.Shared.Models;

[Table("track_info")]
public class TrackInfo : BaseEntity
{
    [Column("name")]
    [MaxLength(50)]
    public string Name { get; set; } = string.Empty;

    [Column("track_type")]
    public TrackType TrackType { get; set; } = TrackType.ArrivalDeparture;

    [Column("depot_id")]
    public long? DepotId { get; set; }

    [Column("length")]
    [Precision(10, 2)]
    public decimal Length { get; set; }

    [Column("gradient")]
    [Precision(5, 2)]
    public decimal? Gradient { get; set; }

    [Column("curve_radius")]
    [Precision(8, 2)]
    public decimal? CurveRadius { get; set; }

    [Column("speed_limit")]
    public int? SpeedLimit { get; set; } = 15;

    [Column("capacity")]
    public int? Capacity { get; set; }

    [Column("status")]
    public TrackStatus Status { get; set; } = TrackStatus.Normal;

    [Column("start_x")]
    [Precision(10, 2)]
    public decimal StartX { get; set; }

    [Column("start_y")]
    [Precision(10, 2)]
    public decimal StartY { get; set; }

    [Column("start_z")]
    [Precision(10, 2)]
    public decimal StartZ { get; set; }

    [Column("end_x")]
    [Precision(10, 2)]
    public decimal EndX { get; set; }

    [Column("end_y")]
    [Precision(10, 2)]
    public decimal EndY { get; set; }

    [Column("end_z")]
    [Precision(10, 2)]
    public decimal EndZ { get; set; }

    [Column("remark")]
    [MaxLength(500)]
    public string? Remark { get; set; }
}
