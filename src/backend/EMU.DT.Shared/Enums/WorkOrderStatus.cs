
namespace EMU.DT.Shared.Enums;

public enum WorkOrderStatus
{
    /// &lt;summary&gt;
    /// 待分配
    /// &lt;/summary&gt;
    PendingAssignment = 1,
    
    /// &lt;summary&gt;
    /// 待执行
    /// &lt;/summary&gt;
    PendingExecution = 2,
    
    /// &lt;summary&gt;
    /// 执行中
    /// &lt;/summary&gt;
    InProgress = 3,
    
    /// &lt;summary&gt;
    /// 暂停
    /// &lt;/summary&gt;
    Paused = 4,
    
    /// &lt;summary&gt;
    /// 已完成
    /// &lt;/summary&gt;
    Completed = 5,
    
    /// &lt;summary&gt;
    /// 已取消
    /// &lt;/summary&gt;
    Cancelled = 6,
    
    /// &lt;summary&gt;
    /// 异常
    /// &lt;/summary&gt;
    Exception = 7
}
