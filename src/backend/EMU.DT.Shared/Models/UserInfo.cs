
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EMU.DT.Shared.Enums;

namespace EMU.DT.Shared.Models;

[Table("user_info")]
public class UserInfo : BaseEntity
{
    [Column("username")]
    [MaxLength(50)]
    public string Username { get; set; } = string.Empty;

    [Column("password_hash")]
    [MaxLength(256)]
    public string PasswordHash { get; set; } = string.Empty;

    [Column("email")]
    [MaxLength(100)]
    public string? Email { get; set; }

    [Column("real_name")]
    [MaxLength(50)]
    public string? RealName { get; set; }

    [Column("phone_number")]
    [MaxLength(20)]
    public string? PhoneNumber { get; set; }

    [Column("role")]
    public RoleType Role { get; set; } = RoleType.Guest;

    [Column("is_active")]
    public bool IsActive { get; set; } = true;
}
