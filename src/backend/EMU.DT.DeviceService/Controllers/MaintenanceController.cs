using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace EMU.DT.DeviceService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MaintenanceController : ControllerBase
    {
        private readonly IMaintenanceService _maintenanceService;

        public MaintenanceController(IMaintenanceService maintenanceService)
        {
            _maintenanceService = maintenanceService;
        }

        [HttpGet("tasks")]
        public IActionResult GetAllMaintenanceTasks()
        {
            var tasks = _maintenanceService.GetAllMaintenanceTasks();
            return Ok(tasks);
        }

        [HttpGet("tasks/{taskId}")]
        public IActionResult GetMaintenanceTaskById(int taskId)
        {
            var task = _maintenanceService.GetMaintenanceTaskById(taskId);
            if (task == null)
            {
                return NotFound();
            }
            return Ok(task);
        }

        [HttpPost("tasks")]
        public IActionResult CreateMaintenanceTask([FromBody] MaintenanceTask task)
        {
            var createdTask = _maintenanceService.CreateMaintenanceTask(task);
            return CreatedAtAction(nameof(GetMaintenanceTaskById), new { taskId = createdTask.TaskId }, createdTask);
        }

        [HttpPut("tasks/{taskId}")]
        public IActionResult UpdateMaintenanceTask(int taskId, [FromBody] MaintenanceTask task)
        {
            task.TaskId = taskId;
            var updatedTask = _maintenanceService.UpdateMaintenanceTask(task);
            if (updatedTask == null)
            {
                return NotFound();
            }
            return Ok(updatedTask);
        }

        [HttpDelete("tasks/{taskId}")]
        public IActionResult DeleteMaintenanceTask(int taskId)
        {
            var result = _maintenanceService.DeleteMaintenanceTask(taskId);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpGet("records/{deviceId}")]
        public IActionResult GetMaintenanceRecords(int deviceId)
        {
            var records = _maintenanceService.GetMaintenanceRecords(deviceId);
            return Ok(records);
        }

        [HttpPost("records")]
        public IActionResult CreateMaintenanceRecord([FromBody] MaintenanceRecord record)
        {
            var createdRecord = _maintenanceService.CreateMaintenanceRecord(record);
            return Ok(createdRecord);
        }

        [HttpGet("plans")]
        public IActionResult GetMaintenancePlans()
        {
            var plans = _maintenanceService.GetMaintenancePlans();
            return Ok(plans);
        }

        [HttpPost("plans")]
        public IActionResult CreateMaintenancePlan([FromBody] MaintenancePlan plan)
        {
            var createdPlan = _maintenanceService.CreateMaintenancePlan(plan);
            return Ok(createdPlan);
        }
    }
}