using EMU.DT.DataService.Models;
using EMU.DT.DataService.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EMU.DT.DataService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DeviceController : ControllerBase
    {
        private readonly IDeviceService _deviceService;

        public DeviceController(IDeviceService deviceService)
        {
            _deviceService = deviceService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Device>>> GetDevices()
        {
            var devices = await _deviceService.GetAllDevicesAsync();
            return Ok(devices);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Device>> GetDevice(int id)
        {
            var device = await _deviceService.GetDeviceByIdAsync(id);
            if (device == null)
            {
                return NotFound();
            }
            return Ok(device);
        }

        [HttpPost]
        public async Task<ActionResult<Device>> CreateDevice([FromBody] Device device)
        {
            var createdDevice = await _deviceService.CreateDeviceAsync(device);
            return CreatedAtAction(nameof(GetDevice), new { id = createdDevice.Id }, createdDevice);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Device>> UpdateDevice(int id, [FromBody] Device device)
        {
            if (id != device.Id)
            {
                return BadRequest();
            }

            var updatedDevice = await _deviceService.UpdateDeviceAsync(device);
            return Ok(updatedDevice);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteDevice(int id)
        {
            var result = await _deviceService.DeleteDeviceAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpGet("{id}/statuses")]
        public async Task<ActionResult<List<DeviceStatus>>> GetDeviceStatuses(int id)
        {
            var statuses = await _deviceService.GetDeviceStatusesAsync(id);
            return Ok(statuses);
        }

        [HttpPost("{id}/statuses")]
        public async Task<ActionResult<DeviceStatus>> AddDeviceStatus(int id, [FromBody] DeviceStatus status)
        {
            if (id != status.DeviceId)
            {
                return BadRequest();
            }

            var addedStatus = await _deviceService.AddDeviceStatusAsync(status);
            return CreatedAtAction(nameof(GetDeviceStatuses), new { id = id }, addedStatus);
        }

        [HttpGet("{id}/maintenances")]
        public async Task<ActionResult<List<DeviceMaintenance>>> GetDeviceMaintenances(int id)
        {
            var maintenances = await _deviceService.GetDeviceMaintenancesAsync(id);
            return Ok(maintenances);
        }

        [HttpPost("{id}/maintenances")]
        public async Task<ActionResult<DeviceMaintenance>> CreateDeviceMaintenance(int id, [FromBody] DeviceMaintenance maintenance)
        {
            if (id != maintenance.DeviceId)
            {
                return BadRequest();
            }

            var createdMaintenance = await _deviceService.CreateDeviceMaintenanceAsync(maintenance);
            return CreatedAtAction(nameof(GetDeviceMaintenances), new { id = id }, createdMaintenance);
        }

        [HttpPut("maintenances/{id}")]
        public async Task<ActionResult<DeviceMaintenance>> UpdateDeviceMaintenance(int id, [FromBody] DeviceMaintenance maintenance)
        {
            if (id != maintenance.Id)
            {
                return BadRequest();
            }

            var updatedMaintenance = await _deviceService.UpdateDeviceMaintenanceAsync(maintenance);
            return Ok(updatedMaintenance);
        }
    }
}