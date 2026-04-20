
namespace EMU.DT.Shared.Utils;

/// &lt;summary&gt;
/// ID生成器
/// &lt;/summary&gt;
public static class IdGenerator
{
    private static readonly object _lock = new();
    private static long _lastTimestamp = -1L;
    private static long _sequence = 0L;
    
    // 常量定义
    private const long _twepoch = 1609459200000L; // 2021-01-01 00:00:00 UTC
    private const long _workerIdBits = 5L;
    private const long _datacenterIdBits = 5L;
    private const long _maxWorkerId = -1L ^ (-1L &lt;&lt; (int)_workerIdBits);
    private const long _maxDatacenterId = -1L ^ (-1L &lt;&lt; (int)_datacenterIdBits);
    private const long _sequenceBits = 12L;
    private const long _workerIdShift = _sequenceBits;
    private const long _datacenterIdShift = _sequenceBits + _workerIdBits;
    private const long _timestampShift = _sequenceBits + _workerIdBits + _datacenterIdBits;
    private const long _sequenceMask = -1L ^ (-1L &lt;&lt; (int)_sequenceBits);
    
    private static long _workerId = 1L;
    private static long _datacenterId = 1L;
    
    /// &lt;summary&gt;
    /// 初始化ID生成器
    /// &lt;/summary&gt;
    public static void Initialize(long workerId, long datacenterId)
    {
        if (workerId &gt; _maxWorkerId || workerId &lt; 0)
            throw new ArgumentException($"Worker ID must be between 0 and {_maxWorkerId}");
        
        if (datacenterId &gt; _maxDatacenterId || datacenterId &lt; 0)
            throw new ArgumentException($"Datacenter ID must be between 0 and {_maxDatacenterId}");
        
        _workerId = workerId;
        _datacenterId = datacenterId;
    }
    
    /// &lt;summary&gt;
    /// 生成唯一ID
    /// &lt;/summary&gt;
    public static long NextId()
    {
        lock (_lock)
        {
            var timestamp = TimeGen();
            
            if (timestamp &lt; _lastTimestamp)
                throw new Exception($"Clock moved backwards. Refusing to generate ID for {_lastTimestamp - timestamp} milliseconds");
            
            if (_lastTimestamp == timestamp)
            {
                _sequence = (_sequence + 1) &amp; _sequenceMask;
                if (_sequence == 0)
                    timestamp = TilNextMillis(_lastTimestamp);
            }
            else
            {
                _sequence = 0L;
            }
            
            _lastTimestamp = timestamp;
            
            return ((timestamp - _twepoch) &lt;&lt; (int)_timestampShift) |
                    (_datacenterId &lt;&lt; (int)_datacenterIdShift) |
                    (_workerId &lt;&lt; (int)_workerIdShift) |
                    _sequence;
        }
    }
    
    /// &lt;summary&gt;
    /// 生成字符串ID
    /// &lt;/summary&gt;
    public static string NextStringId() =&gt; NextId().ToString();
    
    /// &lt;summary&gt;
    /// 生成GUID字符串
    /// &lt;/summary&gt;
    public static string NewGuid() =&gt; Guid.NewGuid().ToString("N");
    
    private static long TilNextMillis(long lastTimestamp)
    {
        var timestamp = TimeGen();
        while (timestamp &lt;= lastTimestamp)
            timestamp = TimeGen();
        return timestamp;
    }
    
    private static long TimeGen() =&gt; DateTime.UtcNow.Ticks / 10000 - 62135596800000L;
}
