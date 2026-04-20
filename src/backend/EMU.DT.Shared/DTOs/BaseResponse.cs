
namespace EMU.DT.Shared.DTOs;

/// &lt;summary&gt;
/// 通用响应
/// &lt;/summary&gt;
public class BaseResponse
{
    /// &lt;summary&gt;
    /// 是否成功
    /// &lt;/summary&gt;
    public bool Success { get; set; } = true;
    
    /// &lt;summary&gt;
    /// 响应码
    /// &lt;/summary&gt;
    public int Code { get; set; } = 200;
    
    /// &lt;summary&gt;
    /// 消息
    /// &lt;/summary&gt;
    public string Message { get; set; } = string.Empty;
}

/// &lt;summary&gt;
/// 带数据的通用响应
/// &lt;/summary&gt;
public class BaseResponse&lt;T&gt; : BaseResponse
{
    /// &lt;summary&gt;
    /// 数据
    /// &lt;/summary&gt;
    public T? Data { get; set; }
}

/// &lt;summary&gt;
/// 分页响应
/// &lt;/summary&gt;
public class PagedResponse&lt;T&gt; : BaseResponse
{
    /// &lt;summary&gt;
    /// 数据列表
    /// &lt;/summary&gt;
    public List&lt;T&gt; Items { get; set; } = [];
    
    /// &lt;summary&gt;
    /// 总数量
    /// &lt;/summary&gt;
    public long TotalCount { get; set; }
    
    /// &lt;summary&gt;
    /// 页码
    /// &lt;/summary&gt;
    public int PageIndex { get; set; }
    
    /// &lt;summary&gt;
    /// 每页数量
    /// &lt;/summary&gt;
    public int PageSize { get; set; }
    
    /// &lt;summary&gt;
    /// 总页数
    /// &lt;/summary&gt;
    public int TotalPages =&gt; (int)Math.Ceiling((double)TotalCount / PageSize);
}
