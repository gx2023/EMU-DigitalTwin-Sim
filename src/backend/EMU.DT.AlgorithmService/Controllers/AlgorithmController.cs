using Microsoft.AspNetCore.Mvc;
using EMU.DT.Algorithm.PHM;
using EMU.DT.Algorithm.PathPlanning;
using EMU.DT.Algorithm.Scheduling;
using EMU.DT.Algorithm.DataGeneration;
using EMU.DT.Algorithm.Vision;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EMU.DT.AlgorithmService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlgorithmController : ControllerBase
    {
        private readonly PHMPredictor _phmPredictor;
        private readonly PathPlanner _pathPlanner;
        private readonly SchedulingOptimizer _schedulingOptimizer;
        private readonly DataGenerator _dataGenerator;
        private readonly VisualInspection _visualInspection;

        public AlgorithmController(
            PHMPredictor phmPredictor,
            PathPlanner pathPlanner,
            SchedulingOptimizer schedulingOptimizer,
            DataGenerator dataGenerator,
            VisualInspection visualInspection)
        {
            _phmPredictor = phmPredictor;
            _pathPlanner = pathPlanner;
            _schedulingOptimizer = schedulingOptimizer;
            _dataGenerator = dataGenerator;
            _visualInspection = visualInspection;
        }

        // PHM算法接口
        [HttpPost("phm/predict")]
        public async Task<IActionResult> PredictHealth([FromBody] PHMRequest request)
        {
            var sensorData = request.SensorData.Select(d => new SensorData
            {
                SensorId = d.SensorId,
                SensorType = d.SensorType,
                Value = d.Value,
                UpperThreshold = d.UpperThreshold,
                LowerThreshold = d.LowerThreshold,
                Timestamp = d.Timestamp
            }).ToList();

            var prediction = await _phmPredictor.PredictHealthAsync(request.DeviceId, sensorData);
            return Ok(prediction);
        }

        [HttpPost("phm/anomalies")]
        public async Task<IActionResult> DetectAnomalies([FromBody] AnomalyRequest request)
        {
            var sensorData = request.SensorData.Select(d => new SensorData
            {
                SensorId = d.SensorId,
                SensorType = d.SensorType,
                Value = d.Value,
                UpperThreshold = d.UpperThreshold,
                LowerThreshold = d.LowerThreshold,
                Timestamp = d.Timestamp
            }).ToList();

            var anomalies = await _phmPredictor.DetectAnomaliesAsync(sensorData);
            return Ok(anomalies);
        }

        [HttpPost("phm/diagnose")]
        public async Task<IActionResult> DiagnoseFault([FromBody] DiagnosisRequest request)
        {
            var sensorData = request.SensorData.Select(d => new SensorData
            {
                SensorId = d.SensorId,
                SensorType = d.SensorType,
                Value = d.Value,
                UpperThreshold = d.UpperThreshold,
                LowerThreshold = d.LowerThreshold,
                Timestamp = d.Timestamp
            }).ToList();

            var diagnosis = await _phmPredictor.DiagnoseFaultAsync(request.DeviceId, sensorData);
            return Ok(diagnosis);
        }

        // 路径规划算法接口
        [HttpPost("path/planning")]
        public async Task<IActionResult> PlanPath([FromBody] PathRequest request)
        {
            var start = new Node { Id = request.Start.Id, X = request.Start.X, Y = request.Start.Y };
            var goal = new Node { Id = request.Goal.Id, X = request.Goal.X, Y = request.Goal.Y };

            var edges = request.Edges.Select(e => new Edge
            {
                Start = new Node { Id = e.Start.Id, X = e.Start.X, Y = e.Start.Y },
                End = new Node { Id = e.End.Id, X = e.End.X, Y = e.End.Y },
                Weight = e.Weight
            }).ToList();

            var obstacles = request.Obstacles.Select(o => new Obstacle
            {
                Id = o.Id,
                Points = o.Points.Select(p => new Point { X = p.X, Y = p.Y }).ToList()
            }).ToList();

            var path = await Task.Run(() => _pathPlanner.PlanPath(start, goal, edges, obstacles));
            return Ok(path);
        }

        // 调度优化算法接口
        [HttpPost("scheduling/optimize")]
        public async Task<IActionResult> OptimizeSchedule([FromBody] ScheduleRequest request)
        {
            var tasks = request.Tasks.Select(t => new MaintenanceTask
            {
                Id = t.Id,
                Type = t.Type,
                Priority = t.Priority,
                DueDate = t.DueDate,
                EstimatedDuration = t.EstimatedDuration,
                RequiredBayType = t.RequiredBayType,
                RequiredSkills = t.RequiredSkills
            }).ToList();

            var bays = request.Bays.Select(b => new MaintenanceBay
            {
                Id = b.Id,
                Type = b.Type,
                IsAvailable = b.IsAvailable
            }).ToList();

            var workers = request.Workers.Select(w => new Worker
            {
                Id = w.Id,
                Name = w.Name,
                Skills = w.Skills
            }).ToList();

            var schedule = await Task.Run(() => _schedulingOptimizer.OptimizeMaintenanceSchedule(tasks, bays, workers));
            return Ok(schedule);
        }

        // 数据生成算法接口
        [HttpPost("data/device")]
        public async Task<IActionResult> GenerateDeviceData([FromBody] DeviceDataRequest request)
        {
            var data = await Task.Run(() => _dataGenerator.GenerateDeviceData(request.DeviceCount, request.DataPoints));
            return Ok(data);
        }

        [HttpPost("data/vehicle")]
        public async Task<IActionResult> GenerateVehicleData([FromBody] VehicleDataRequest request)
        {
            var data = await Task.Run(() => _dataGenerator.GenerateVehicleData(request.VehicleCount, request.DataPoints));
            return Ok(data);
        }

        // 视觉检测算法接口
        [HttpPost("vision/detect")]
        public async Task<IActionResult> DetectDefects([FromBody] VisionRequest request)
        {
            var result = await Task.Run(() => _visualInspection.DetectDefects(request.ImagePath, request.InspectionType));
            return Ok(result);
        }

        [HttpPost("vision/measure")]
        public async Task<IActionResult> MeasureWear([FromBody] MeasurementRequest request)
        {
            var measurement = await Task.Run(() => _visualInspection.MeasureWear(request.ImagePath, request.MeasurementType));
            return Ok(new { measurement });
        }

        [HttpPost("vision/analyze")]
        public async Task<IActionResult> AnalyzeImage([FromBody] AnalysisRequest request)
        {
            var result = await Task.Run(() => _visualInspection.AnalyzeImage(request.ImagePath, request.AnalysisType));
            return Ok(result);
        }
    }

    // 请求和响应模型
    public class PHMRequest
    {
        public int DeviceId { get; set; }
        public List<SensorDataDto> SensorData { get; set; }
    }

    public class SensorDataDto
    {
        public int SensorId { get; set; }
        public string SensorType { get; set; }
        public double Value { get; set; }
        public double UpperThreshold { get; set; }
        public double LowerThreshold { get; set; }
        public System.DateTime Timestamp { get; set; }
    }

    public class AnomalyRequest
    {
        public List<SensorDataDto> SensorData { get; set; }
    }

    public class DiagnosisRequest
    {
        public int DeviceId { get; set; }
        public List<SensorDataDto> SensorData { get; set; }
    }

    public class PathRequest
    {
        public NodeDto Start { get; set; }
        public NodeDto Goal { get; set; }
        public List<EdgeDto> Edges { get; set; }
        public List<ObstacleDto> Obstacles { get; set; }
    }

    public class NodeDto
    {
        public int Id { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
    }

    public class EdgeDto
    {
        public NodeDto Start { get; set; }
        public NodeDto End { get; set; }
        public double Weight { get; set; }
    }

    public class ObstacleDto
    {
        public int Id { get; set; }
        public List<PointDto> Points { get; set; }
    }

    public class PointDto
    {
        public double X { get; set; }
        public double Y { get; set; }
    }

    public class ScheduleRequest
    {
        public List<MaintenanceTaskDto> Tasks { get; set; }
        public List<MaintenanceBayDto> Bays { get; set; }
        public List<WorkerDto> Workers { get; set; }
    }

    public class MaintenanceTaskDto
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public int Priority { get; set; }
        public System.DateTime DueDate { get; set; }
        public double EstimatedDuration { get; set; }
        public string RequiredBayType { get; set; }
        public List<string> RequiredSkills { get; set; }
    }

    public class MaintenanceBayDto
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public bool IsAvailable { get; set; }
    }

    public class WorkerDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<string> Skills { get; set; }
    }

    public class DeviceDataRequest
    {
        public int DeviceCount { get; set; }
        public int DataPoints { get; set; }
    }

    public class VehicleDataRequest
    {
        public int VehicleCount { get; set; }
        public int DataPoints { get; set; }
    }

    public class VisionRequest
    {
        public string ImagePath { get; set; }
        public string InspectionType { get; set; }
    }

    public class MeasurementRequest
    {
        public string ImagePath { get; set; }
        public string MeasurementType { get; set; }
    }

    public class AnalysisRequest
    {
        public string ImagePath { get; set; }
        public string AnalysisType { get; set; }
    }
}