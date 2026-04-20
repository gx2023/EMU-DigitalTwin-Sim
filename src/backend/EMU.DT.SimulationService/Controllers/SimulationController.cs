using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using EMU.DT.SimulationService;

namespace EMU.DT.SimulationService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SimulationController : ControllerBase
    {
        private readonly ISimulationService _simulationService;
        private readonly ISegmentFactoryService _segmentFactoryService;
        private readonly IVehicleService _vehicleService;

        public SimulationController(
            ISimulationService simulationService,
            ISegmentFactoryService segmentFactoryService,
            IVehicleService vehicleService)
        {
            _simulationService = simulationService;
            _segmentFactoryService = segmentFactoryService;
            _vehicleService = vehicleService;
        }

        // 仿真管理接口
        [HttpGet("simulations")]
        public IActionResult GetAllSimulations()
        {
            var simulations = _simulationService.GetAllSimulations();
            return Ok(simulations);
        }

        [HttpGet("simulations/{id}")]
        public IActionResult GetSimulationById(int id)
        {
            var simulation = _simulationService.GetSimulationById(id);
            return Ok(simulation);
        }

        [HttpPost("simulations")]
        public IActionResult CreateSimulation([FromBody] Simulation simulation)
        {
            var createdSimulation = _simulationService.CreateSimulation(simulation);
            return Ok(createdSimulation);
        }

        [HttpPut("simulations/{id}")]
        public IActionResult UpdateSimulation(int id, [FromBody] Simulation simulation)
        {
            simulation.SimulationId = id;
            var updatedSimulation = _simulationService.UpdateSimulation(simulation);
            return Ok(updatedSimulation);
        }

        [HttpDelete("simulations/{id}")]
        public IActionResult DeleteSimulation(int id)
        {
            var result = _simulationService.DeleteSimulation(id);
            return Ok(new { success = result });
        }

        [HttpPost("simulations/{id}/start")]
        public IActionResult StartSimulation(int id)
        {
            var result = _simulationService.StartSimulation(id);
            return Ok(new { success = result });
        }

        [HttpPost("simulations/{id}/stop")]
        public IActionResult StopSimulation(int id)
        {
            var result = _simulationService.StopSimulation(id);
            return Ok(new { success = result });
        }

        [HttpPost("simulations/{id}/pause")]
        public IActionResult PauseSimulation(int id)
        {
            var result = _simulationService.PauseSimulation(id);
            return Ok(new { success = result });
        }

        [HttpPost("simulations/{id}/resume")]
        public IActionResult ResumeSimulation(int id)
        {
            var result = _simulationService.ResumeSimulation(id);
            return Ok(new { success = result });
        }

        // 段厂管理接口
        [HttpGet("segment-factories")]
        public IActionResult GetAllSegmentFactories()
        {
            var segmentFactories = _segmentFactoryService.GetAllSegmentFactories();
            return Ok(segmentFactories);
        }

        [HttpGet("segment-factories/{id}")]
        public IActionResult GetSegmentFactoryById(int id)
        {
            var segmentFactory = _segmentFactoryService.GetSegmentFactoryById(id);
            return Ok(segmentFactory);
        }

        [HttpPost("segment-factories")]
        public IActionResult CreateSegmentFactory([FromBody] SegmentFactory segmentFactory)
        {
            var createdSegmentFactory = _segmentFactoryService.CreateSegmentFactory(segmentFactory);
            return Ok(createdSegmentFactory);
        }

        [HttpPut("segment-factories/{id}")]
        public IActionResult UpdateSegmentFactory(int id, [FromBody] SegmentFactory segmentFactory)
        {
            segmentFactory.SegmentFactoryId = id;
            var updatedSegmentFactory = _segmentFactoryService.UpdateSegmentFactory(segmentFactory);
            return Ok(updatedSegmentFactory);
        }

        [HttpDelete("segment-factories/{id}")]
        public IActionResult DeleteSegmentFactory(int id)
        {
            var result = _segmentFactoryService.DeleteSegmentFactory(id);
            return Ok(new { success = result });
        }

        // 车辆管理接口
        [HttpGet("vehicles")]
        public IActionResult GetAllVehicles()
        {
            var vehicles = _vehicleService.GetAllVehicles();
            return Ok(vehicles);
        }

        [HttpGet("vehicles/{id}")]
        public IActionResult GetVehicleById(int id)
        {
            var vehicle = _vehicleService.GetVehicleById(id);
            return Ok(vehicle);
        }

        [HttpPost("vehicles")]
        public IActionResult CreateVehicle([FromBody] Vehicle vehicle)
        {
            var createdVehicle = _vehicleService.CreateVehicle(vehicle);
            return Ok(createdVehicle);
        }

        [HttpPut("vehicles/{id}")]
        public IActionResult UpdateVehicle(int id, [FromBody] Vehicle vehicle)
        {
            vehicle.VehicleId = id;
            var updatedVehicle = _vehicleService.UpdateVehicle(vehicle);
            return Ok(updatedVehicle);
        }

        [HttpDelete("vehicles/{id}")]
        public IActionResult DeleteVehicle(int id)
        {
            var result = _vehicleService.DeleteVehicle(id);
            return Ok(new { success = result });
        }

        [HttpGet("vehicles/{id}/status")]
        public IActionResult GetVehicleStatus(int id)
        {
            var statuses = _vehicleService.GetVehicleStatus(id);
            return Ok(statuses);
        }

        [HttpPost("vehicles/{id}/status")]
        public IActionResult UpdateVehicleStatus(int id, [FromBody] VehicleStatus status)
        {
            status.VehicleId = id;
            var updatedStatus = _vehicleService.UpdateVehicleStatus(status);
            return Ok(updatedStatus);
        }

        [HttpGet("vehicles/{id}/maintenance-tasks")]
        public IActionResult GetVehicleMaintenanceTasks(int id)
        {
            var tasks = _vehicleService.GetVehicleMaintenanceTasks(id);
            return Ok(tasks);
        }
    }
}