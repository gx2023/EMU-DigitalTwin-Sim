using EMU.DT.DataService.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EMU.DT.DataService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DataAnalysisController : ControllerBase
    {
        private readonly IDataAnalysisService _dataAnalysisService;

        public DataAnalysisController(IDataAnalysisService dataAnalysisService)
        {
            _dataAnalysisService = dataAnalysisService;
        }

        [HttpGet("maintenance-volume")]
        public async Task<ActionResult<object>> GetMaintenanceVolumeStatistics([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            var result = await _dataAnalysisService.GetMaintenanceVolumeStatisticsAsync(startDate, endDate);
            return Ok(result);
        }

        [HttpGet("quality-trend")]
        public async Task<ActionResult<object>> GetQualityTrendAnalysis([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            var result = await _dataAnalysisService.GetQualityTrendAnalysisAsync(startDate, endDate);
            return Ok(result);
        }

        [HttpGet("fault-distribution")]
        public async Task<ActionResult<object>> GetFaultDistributionAnalysis([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            var result = await _dataAnalysisService.GetFaultDistributionAnalysisAsync(startDate, endDate);
            return Ok(result);
        }

        [HttpGet("device-utilization")]
        public async Task<ActionResult<object>> GetDeviceUtilizationAnalysis([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            var result = await _dataAnalysisService.GetDeviceUtilizationAnalysisAsync(startDate, endDate);
            return Ok(result);
        }

        [HttpGet("maintenance-time")]
        public async Task<ActionResult<object>> GetMaintenanceTimeAnalysis([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            var result = await _dataAnalysisService.GetMaintenanceTimeAnalysisAsync(startDate, endDate);
            return Ok(result);
        }

        [HttpGet("worker-performance")]
        public async Task<ActionResult<object>> GetWorkerPerformanceAnalysis([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            var result = await _dataAnalysisService.GetWorkerPerformanceAnalysisAsync(startDate, endDate);
            return Ok(result);
        }
    }
}