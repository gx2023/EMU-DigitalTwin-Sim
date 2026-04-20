
namespace EMU.DT.Shared.Utils;

/// &lt;summary&gt;
/// 日期时间工具类
/// &lt;/summary&gt;
public static class DateTimeHelper
{
    /// &lt;summary&gt;
    /// 获取Unix时间戳（秒）
    /// &lt;/summary&gt;
    public static long GetUnixTimestamp() =&gt; (long)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds;
    
    /// &lt;summary&gt;
    /// 获取Unix时间戳（毫秒）
    /// &lt;/summary&gt;
    public static long GetUnixTimestampMs() =&gt; (long)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalMilliseconds;
    
    /// &lt;summary&gt;
    /// 转换为Unix时间戳（秒）
    /// &lt;/summary&gt;
    public static long ToUnixTimestamp(this DateTime dateTime) =&gt; (long)(dateTime.ToUniversalTime() - new DateTime(1970, 1, 1)).TotalSeconds;
    
    /// &lt;summary&gt;
    /// 转换为Unix时间戳（毫秒）
    /// &lt;/summary&gt;
    public static long ToUnixTimestampMs(this DateTime dateTime) =&gt; (long)(dateTime.ToUniversalTime() - new DateTime(1970, 1, 1)).TotalMilliseconds;
    
    /// &lt;summary&gt;
    /// 从Unix时间戳转换（秒）
    /// &lt;/summary&gt;
    public static DateTime FromUnixTimestamp(long timestamp) =&gt; new DateTime(1970, 1, 1).AddSeconds(timestamp);
    
    /// &lt;summary&gt;
    /// 从Unix时间戳转换（毫秒）
    /// &lt;/summary&gt;
    public static DateTime FromUnixTimestampMs(long timestampMs) =&gt; new DateTime(1970, 1, 1).AddMilliseconds(timestampMs);
    
    /// &lt;summary&gt;
    /// 获取日期的开始时间
    /// &lt;/summary&gt;
    public static DateTime StartOfDay(this DateTime date) =&gt; new(date.Year, date.Month, date.Day, 0, 0, 0);
    
    /// &lt;summary&gt;
    /// 获取日期的结束时间
    /// &lt;/summary&gt;
    public static DateTime EndOfDay(this DateTime date) =&gt; new(date.Year, date.Month, date.Day, 23, 59, 59, 999);
    
    /// &lt;summary&gt;
    /// 获取月份的开始时间
    /// &lt;/summary&gt;
    public static DateTime StartOfMonth(this DateTime date) =&gt; new(date.Year, date.Month, 1);
    
    /// &lt;summary&gt;
    /// 获取月份的结束时间
    /// &lt;/summary&gt;
    public static DateTime EndOfMonth(this DateTime date) =&gt; new(date.Year, date.Month, DateTime.DaysInMonth(date.Year, date.Month), 23, 59, 59, 999);
}
