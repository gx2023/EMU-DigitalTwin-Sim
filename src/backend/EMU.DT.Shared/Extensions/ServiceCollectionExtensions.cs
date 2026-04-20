
using Microsoft.Extensions.DependencyInjection;

namespace EMU.DT.Shared.Extensions;

public static class ServiceCollectionExtensions
{
    /// &lt;summary&gt;
    /// 添加共享服务
    /// &lt;/summary&gt;
    public static IServiceCollection AddSharedServices(this IServiceCollection services)
    {
        // 初始化ID生成器
        Utils.IdGenerator.Initialize(1, 1);
        
        return services;
    }
}
