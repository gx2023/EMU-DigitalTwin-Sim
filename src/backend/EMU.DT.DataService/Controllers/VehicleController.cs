using EMU.DT.DataService.Models;
using EMU.DT.DataService.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EMU.DT.DataService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class VehicleController : ControllerBase
    {
        private readonly IVehicleService _vehicleService;

        public VehicleController(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Vehicle>>> GetVehicles()
        {
            var vehicles = await _vehicleService.GetAllVehiclesAsync();
            return Ok(vehicles);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Vehicle>> GetVehicle(int id)
        {
            var vehicle = await _vehicleService.GetVehicleByIdAsync(id);
            if (vehicle == null)
            {
                return NotFound();
            }
            return Ok(vehicle);
        }

        [HttpPost]
        public async Task<ActionResult<Vehicle>> CreateVehicle([FromBody] Vehicle vehicle)
        {
            var createdVehicle = await _vehicleService.CreateVehicleAsync(vehicle);
            return CreatedAtAction(nameof(GetVehicle), new { id = createdVehicle.Id }, createdVehicle);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Vehicle>> UpdateVehicle(int id, [FromBody] Vehicle vehicle)
        {
            if (id != vehicle.Id)
            {
                return BadRequest();
            }

            var updatedVehicle = await _vehicleService.UpdateVehicleAsync(vehicle);
            return Ok(updatedVehicle);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteVehicle(int id)
        {
            var result = await _vehicleService.DeleteVehicleAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpGet("{id}/statuses")]
        public async Task<ActionResult<List<VehicleStatus>>> GetVehicleStatuses(int id)
        {
            var statuses = await _vehicleService.GetVehicleStatusesAsync(id);
            return Ok(statuses);
        }

        [HttpPost("{id}/statuses")]
        public async Task<ActionResult<VehicleStatus>> AddVehicleStatus(int id, [FromBody] VehicleStatus status)
        {
            if (id != status.VehicleId)
            {
                return BadRequest();
            }

            var addedStatus = await _vehicleService.AddVehicleStatusAsync(status);
            return CreatedAtAction(nameof(GetVehicleStatuses), new { id = id }, addedStatus);
        }
    }
}