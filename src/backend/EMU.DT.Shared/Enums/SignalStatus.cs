
namespace EMU.DT.Shared.Enums;

public enum SignalStatus
{
    /// &lt;summary&gt;
    /// 红灯 - 禁止通行
    /// &lt;/summary&gt;
    Red = 1,
    
    /// &lt;summary&gt;
    /// 黄灯 - 注意
    /// &lt;/summary&gt;
    Yellow = 2,
    
    /// &lt;summary&gt;
    /// 绿灯 - 允许通行
    /// &lt;/summary&gt;
    Green = 3,
    
    /// &lt;summary&gt;
    /// 双黄灯 - 侧线准备
    /// &lt;/summary&gt;
    DoubleYellow = 4,
    
    /// &lt;summary&gt;
    /// 白灯 - 调车允许
    /// &lt;/summary&gt;
    White = 5,
    
    /// &lt;summary&gt;
    /// 蓝灯 - 调车禁止
    /// &lt;/summary&gt;
    Blue = 6,
    
    /// &lt;summary&gt;
    /// 熄灯 - 关闭
    /// &lt;/summary&gt;
    Off = 7
}
