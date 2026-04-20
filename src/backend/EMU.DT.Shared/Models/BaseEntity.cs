
namespace EMU.DT.Shared.Models;

public abstract class BaseEntity
{
    /// &lt;summary&gt;
    /// 主键ID
    /// &lt;/summary&gt;
    public long Id { get; set; }
    
    /// &lt;summary&gt;
    /// 创建时间
    /// &lt;/summary&gt;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    /// &lt;summary&gt;
    /// 更新时间
    /// &lt;/summary&gt;
    public DateTime? UpdatedAt { get; set; }
    
    /// &lt;summary&gt;
    /// 是否删除（软删除）
    /// &lt;/summary&gt;
    public bool IsDeleted { get; set; } = false;
    
    /// &lt;summary&gt;
    /// 删除时间
    /// &lt;/summary&gt;
    public DateTime? DeletedAt { get; set; }
}
