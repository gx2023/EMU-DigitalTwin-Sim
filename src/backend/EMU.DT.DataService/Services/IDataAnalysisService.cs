namespace EMU.DT.DataService.Services
{
    public interface IDataAnalysisService
    {
        Task<object> GetMaintenanceVolumeStatisticsAsync(DateTime startDate, DateTime endDate);
        Task<object> GetQualityTrendAnalysisAsync(DateTime startDate, DateTime endDate);
        Task<object> GetFaultDistributionAnalysisAsync(DateTime startDate, DateTime endDate);
        Task<object> GetDeviceUtilizationAnalysisAsync(DateTime startDate, DateTime endDate);
        Task<object> GetMaintenanceTimeAnalysisAsync(DateTime startDate, DateTime endDate);
        Task<object> GetWorkerPerformanceAnalysisAsync(DateTime startDate, DateTime endDate);
    }
}