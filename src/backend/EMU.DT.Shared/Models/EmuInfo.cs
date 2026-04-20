
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EMU.DT.Shared.Enums;

namespace EMU.DT.Shared.Models;

[Table("emu_info")]
public class EmuInfo : BaseEntity
{
    [Column("emu_type")]
    public EmuType EmuType { get; set; }

    [Column("emu_series")]
    [MaxLength(50)]
    public string EmuSeries { get; set; } = string.Empty;

    [Column("train_set_no")]
    [MaxLength(20)]
    public string TrainSetNo { get; set; } = string.Empty;

    [Column("manufacturing_date")]
    public DateTime ManufacturingDate { get; set; }

    [Column("depot_id")]
    public long? DepotId { get; set; }

    [Column("status")]
    public EmuStatus Status { get; set; } = EmuStatus.Operating;

    [Column("mileage")]
    [Precision(12, 2)]
    public decimal? Mileage { get; set; }

    [Column("remark")]
    [MaxLength(500)]
    public string? Remark { get; set; }
}
