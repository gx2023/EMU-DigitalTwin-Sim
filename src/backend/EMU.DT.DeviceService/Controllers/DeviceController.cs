using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace EMU.DT.DeviceService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DeviceController : ControllerBase
    {
        private readonly IDeviceService _deviceService;

        public DeviceController(IDeviceService deviceService)
        {
            _deviceService = deviceService;
        }

        [HttpGet]
        public IActionResult GetAllDevices()
        {
            var devices = _deviceService.GetAllDevices();
            return Ok(devices);
        }

        [HttpGet("{deviceId}")]
        public IActionResult GetDeviceById(int deviceId)
        {
            var device = _deviceService.GetDeviceById(deviceId);
            if (device == null)
            {
                return NotFound();
            }
            return Ok(device);
        }

        [HttpPost]
        public IActionResult CreateDevice([FromBody] Device device)
        {
            var createdDevice = _deviceService.CreateDevice(device);
            return CreatedAtAction(nameof(GetDeviceById), new { deviceId = createdDevice.DeviceId }, createdDevice);
        }

        [HttpPut("{deviceId}")]
        public IActionResult UpdateDevice(int deviceId, [FromBody] Device device)
        {
            device.DeviceId = deviceId;
            var updatedDevice = _deviceService.UpdateDevice(device);
            if (updatedDevice == null)
            {
                return NotFound();
            }
            return Ok(updatedDevice);
        }

        [HttpDelete("{deviceId}")]
        public IActionResult DeleteDevice(int deviceId)
        {
            var result = _deviceService.DeleteDevice(deviceId);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpGet("{deviceId}/status")]
        public IActionResult GetDeviceStatus(int deviceId)
        {
            var statuses = _deviceService.GetDeviceStatus(deviceId);
            return Ok(statuses);
        }

        [HttpPost("status")]
        public IActionResult UpdateDeviceStatus([FromBody] DeviceStatus status)
        {
            var updatedStatus = _deviceService.UpdateDeviceStatus(status);
            return Ok(updatedStatus);
        }
    }
}