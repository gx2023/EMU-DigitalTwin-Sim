
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EMU.DT.Shared.Enums;

namespace EMU.DT.Shared.Models;

[Table("depot_info")]
public class DepotInfo : BaseEntity
{
    [Column("name")]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [Column("type")]
    public WorkshopType Type { get; set; }

    [Column("address")]
    [MaxLength(200)]
    public string? Address { get; set; }

    [Column("contact_person")]
    [MaxLength(50)]
    public string? ContactPerson { get; set; }

    [Column("contact_phone")]
    [MaxLength(20)]
    public string? ContactPhone { get; set; }

    [Column("description")]
    [MaxLength(1000)]
    public string? Description { get; set; }

    [Column("model_path")]
    [MaxLength(500)]
    public string? ModelPath { get; set; }
}
