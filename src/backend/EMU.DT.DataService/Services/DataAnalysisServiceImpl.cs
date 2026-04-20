using EMU.DT.DataService.Data;
using EMU.DT.DataService.Models;
using Microsoft.EntityFrameworkCore;

namespace EMU.DT.DataService.Services
{
    public class DataAnalysisServiceImpl : IDataAnalysisService
    {
        private readonly DataDbContext _dbContext;

        public DataAnalysisServiceImpl(DataDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<object> GetMaintenanceVolumeStatisticsAsync(DateTime startDate, DateTime endDate)
        {
            var maintenanceTasks = await _dbContext.MaintenanceTasks
                .Where(mt => mt.StartDate >= startDate && mt.StartDate <= endDate)
                .GroupBy(mt => mt.Type)
                .Select(g => new
                {
                    Type = g.Key,
                    Count = g.Count()
                })
                .ToListAsync();

            return new
            {
                startDate,
                endDate,
                data = maintenanceTasks
            };
        }

        public async Task<object> GetQualityTrendAnalysisAsync(DateTime startDate, DateTime endDate)
        {
            var qualityChecks = await _dbContext.QualityChecks
                .Where(qc => qc.CheckTime >= startDate && qc.CheckTime <= endDate)
                .GroupBy(qc => new { qc.Result, Month = qc.CheckTime.Month })
                .Select(g => new
                {
                    Result = g.Key.Result,
                    Month = g.Key.Month,
                    Count = g.Count()
                })
                .ToListAsync();

            return new
            {
                startDate,
                endDate,
                data = qualityChecks
            };
        }

        public async Task<object> GetFaultDistributionAnalysisAsync(DateTime startDate, DateTime endDate)
        {
            // 这里简化处理，实际应该从故障记录中分析
            var faultDistribution = new List<object>
            {
                new { Component = "制动系统", Count = 25 },
                new { Component = "牵引系统", Count = 20 },
                new { Component = "转向架", Count = 15 },
                new { Component = "车门系统", Count = 10 },
                new { Component = "空调系统", Count = 8 },
                new { Component = "其他", Count = 22 }
            };

            return new
            {
                startDate,
                endDate,
                data = faultDistribution
            };
        }

        public async Task<object> GetDeviceUtilizationAnalysisAsync(DateTime startDate, DateTime endDate)
        {
            var devices = await _dbContext.Devices.ToListAsync();
            var deviceUtilization = devices.Select(device => new
            {
                DeviceId = device.Id,
                DeviceName = device.Name,
                UtilizationRate = new Random().Next(70, 100) // 模拟数据
            }).ToList();

            return new
            {
                startDate,
                endDate,
                data = deviceUtilization
            };
        }

        public async Task<object> GetMaintenanceTimeAnalysisAsync(DateTime startDate, DateTime endDate)
        {
            var maintenanceTasks = await _dbContext.MaintenanceTasks
                .Where(mt => mt.StartDate >= startDate && mt.EndDate.HasValue && mt.EndDate <= endDate)
                .Select(mt => new
                {
                    Type = mt.Type,
                    Duration = (mt.EndDate.Value - mt.StartDate).TotalHours
                })
                .GroupBy(mt => mt.Type)
                .Select(g => new
                {
                    Type = g.Key,
                    AverageDuration = g.Average(x => x.Duration),
                    TotalDuration = g.Sum(x => x.Duration)
                })
                .ToListAsync();

            return new
            {
                startDate,
                endDate,
                data = maintenanceTasks
            };
        }

        public async Task<object> GetWorkerPerformanceAnalysisAsync(DateTime startDate, DateTime endDate)
        {
            var maintenanceSteps = await _dbContext.MaintenanceSteps
                .Where(ms => ms.StartTime.HasValue && ms.EndTime.HasValue && ms.StartTime >= startDate && ms.EndTime <= endDate)
                .GroupBy(ms => ms.Operator)
                .Select(g => new
                {
                    Worker = g.Key,
                    CompletedSteps = g.Count(),
                    AverageDuration = g.Average(ms => (ms.EndTime.Value - ms.StartTime.Value).TotalMinutes)
                })
                .ToListAsync();

            return new
            {
                startDate,
                endDate,
                data = maintenanceSteps
            };
        }
    }
}