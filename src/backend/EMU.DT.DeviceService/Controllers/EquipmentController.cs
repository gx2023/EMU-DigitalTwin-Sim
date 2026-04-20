using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace EMU.DT.DeviceService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EquipmentController : ControllerBase
    {
        private readonly IEquipmentService _equipmentService;

        public EquipmentController(IEquipmentService equipmentService)
        {
            _equipmentService = equipmentService;
        }

        [HttpGet]
        public IActionResult GetAllEquipment()
        {
            var equipment = _equipmentService.GetAllEquipment();
            return Ok(equipment);
        }

        [HttpGet("{equipmentId}")]
        public IActionResult GetEquipmentById(int equipmentId)
        {
            var equipment = _equipmentService.GetEquipmentById(equipmentId);
            if (equipment == null)
            {
                return NotFound();
            }
            return Ok(equipment);
        }

        [HttpPost]
        public IActionResult CreateEquipment([FromBody] Equipment equipment)
        {
            var createdEquipment = _equipmentService.CreateEquipment(equipment);
            return CreatedAtAction(nameof(GetEquipmentById), new { equipmentId = createdEquipment.EquipmentId }, createdEquipment);
        }

        [HttpPut("{equipmentId}")]
        public IActionResult UpdateEquipment(int equipmentId, [FromBody] Equipment equipment)
        {
            equipment.EquipmentId = equipmentId;
            var updatedEquipment = _equipmentService.UpdateEquipment(equipment);
            if (updatedEquipment == null)
            {
                return NotFound();
            }
            return Ok(updatedEquipment);
        }

        [HttpDelete("{equipmentId}")]
        public IActionResult DeleteEquipment(int equipmentId)
        {
            var result = _equipmentService.DeleteEquipment(equipmentId);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpGet("{equipmentId}/status")]
        public IActionResult GetEquipmentStatus(int equipmentId)
        {
            var statuses = _equipmentService.GetEquipmentStatus(equipmentId);
            return Ok(statuses);
        }

        [HttpPost("status")]
        public IActionResult UpdateEquipmentStatus([FromBody] EquipmentStatus status)
        {
            var updatedStatus = _equipmentService.UpdateEquipmentStatus(status);
            return Ok(updatedStatus);
        }
    }
}