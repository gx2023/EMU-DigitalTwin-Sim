using EMU.DT.DataService.Models;
using EMU.DT.DataService.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EMU.DT.DataService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MaintenanceController : ControllerBase
    {
        private readonly IMaintenanceService _maintenanceService;

        public MaintenanceController(IMaintenanceService maintenanceService)
        {
            _maintenanceService = maintenanceService;
        }

        [HttpGet]
        public async Task<ActionResult<List<MaintenanceTask>>> GetMaintenanceTasks()
        {
            var tasks = await _maintenanceService.GetAllMaintenanceTasksAsync();
            return Ok(tasks);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MaintenanceTask>> GetMaintenanceTask(int id)
        {
            var task = await _maintenanceService.GetMaintenanceTaskByIdAsync(id);
            if (task == null)
            {
                return NotFound();
            }
            return Ok(task);
        }

        [HttpPost]
        public async Task<ActionResult<MaintenanceTask>> CreateMaintenanceTask([FromBody] MaintenanceTask task)
        {
            var createdTask = await _maintenanceService.CreateMaintenanceTaskAsync(task);
            return CreatedAtAction(nameof(GetMaintenanceTask), new { id = createdTask.Id }, createdTask);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<MaintenanceTask>> UpdateMaintenanceTask(int id, [FromBody] MaintenanceTask task)
        {
            if (id != task.Id)
            {
                return BadRequest();
            }

            var updatedTask = await _maintenanceService.UpdateMaintenanceTaskAsync(task);
            return Ok(updatedTask);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteMaintenanceTask(int id)
        {
            var result = await _maintenanceService.DeleteMaintenanceTaskAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpGet("{id}/steps")]
        public async Task<ActionResult<List<MaintenanceStep>>> GetMaintenanceSteps(int id)
        {
            var steps = await _maintenanceService.GetMaintenanceStepsAsync(id);
            return Ok(steps);
        }

        [HttpPost("{id}/steps")]
        public async Task<ActionResult<MaintenanceStep>> AddMaintenanceStep(int id, [FromBody] MaintenanceStep step)
        {
            if (id != step.MaintenanceTaskId)
            {
                return BadRequest();
            }

            var addedStep = await _maintenanceService.AddMaintenanceStepAsync(step);
            return CreatedAtAction(nameof(GetMaintenanceSteps), new { id = id }, addedStep);
        }

        [HttpPut("steps/{id}")]
        public async Task<ActionResult<MaintenanceStep>> UpdateMaintenanceStep(int id, [FromBody] MaintenanceStep step)
        {
            if (id != step.Id)
            {
                return BadRequest();
            }

            var updatedStep = await _maintenanceService.UpdateMaintenanceStepAsync(step);
            return Ok(updatedStep);
        }
    }
}