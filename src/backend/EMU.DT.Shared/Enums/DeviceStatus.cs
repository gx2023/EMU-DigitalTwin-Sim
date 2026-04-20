
namespace EMU.DT.Shared.Enums;

public enum DeviceStatus
{
    /// &lt;summary&gt;
    /// 正常运行
    /// &lt;/summary&gt;
    Normal = 1,
    
    /// &lt;summary&gt;
    /// 预警
    /// &lt;/summary&gt;
    Warning = 2,
    
    /// &lt;summary&gt;
    /// 故障
    /// &lt;/summary&gt;
    Fault = 3,
    
    /// &lt;summary&gt;
    /// 维护中
    /// &lt;/summary&gt;
    Maintenance = 4,
    
    /// &lt;summary&gt;
    /// 离线
    /// &lt;/summary&gt;
    Offline = 5
}
