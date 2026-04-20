using System.Collections.Generic;
using System.Linq;

namespace EMU.DT.SimulationService
{
    public class VehicleServiceImpl : IVehicleService
    {
        private List<Vehicle> _vehicles;
        private List<VehicleStatus> _vehicleStatuses;
        private List<MaintenanceTask> _maintenanceTasks;

        public VehicleServiceImpl()
        {
            _vehicles = new List<Vehicle>
            {
                new Vehicle
                {
                    VehicleId = 1,
                    VehicleNumber = "CR400AF-2048",
                    VehicleModel = "CR400AF",
                    VehicleType = "高速动车组",
                    Status = "检修中",
                    Location = "检修库1-1",
                    Mileage = 125000,
                    LastMaintenanceDate = System.DateTime.Now.AddMonths(-1),
                    NextMaintenanceDate = System.DateTime.Now.AddMonths(5),
                    Tags = new List<string> { "一级修", "日常维护" }
                },
                new Vehicle
                {
                    VehicleId = 2,
                    VehicleNumber = "CRH380B-1024",
                    VehicleModel = "CRH380B",
                    VehicleType = "高速动车组",
                    Status = "待检修",
                    Location = "存车线2",
                    Mileage = 210000,
                    LastMaintenanceDate = System.DateTime.Now.AddMonths(-3),
                    NextMaintenanceDate = System.DateTime.Now.AddMonths(3),
                    Tags = new List<string> { "二级修", "专项检修" }
                },
                new Vehicle
                {
                    VehicleId = 3,
                    VehicleNumber = "CR400BF-3072",
                    VehicleModel = "CR400BF",
                    VehicleType = "高速动车组",
                    Status = "运行中",
                    Location = "正线",
                    Mileage = 85000,
                    LastMaintenanceDate = System.DateTime.Now.AddMonths(-2),
                    NextMaintenanceDate = System.DateTime.Now.AddMonths(4),
                    Tags = new List<string> { "一级修", "日常维护" }
                }
            };

            _vehicleStatuses = new List<VehicleStatus>();
            foreach (var vehicle in _vehicles)
            {
                _vehicleStatuses.Add(new VehicleStatus
                {
                    StatusId = vehicle.VehicleId,
                    VehicleId = vehicle.VehicleId,
                    Status = vehicle.Status,
                    Speed = vehicle.Status == "运行中" ? 300 : 0,
                    Temperature = 25 + new System.Random().Next(10),
                    Humidity = 45 + new System.Random().Next(15),
                    BatteryLevel = 85 + new System.Random().Next(15),
                    Timestamp = System.DateTime.Now,
                    Alerts = new List<string>()
                });
            }

            _maintenanceTasks = new List<MaintenanceTask>
            {
                new MaintenanceTask
                {
                    TaskId = 1,
                    VehicleId = 1,
                    TaskType = "一级修",
                    Status = "进行中",
                    ScheduledDate = System.DateTime.Now.AddDays(-1),
                    CompletionDate = System.DateTime.Now.AddDays(1),
                    AssignedTo = "张师傅",
                    Priority = "高",
                    Notes = "常规日常检修"
                },
                new MaintenanceTask
                {
                    TaskId = 2,
                    VehicleId = 2,
                    TaskType = "二级修",
                    Status = "待开始",
                    ScheduledDate = System.DateTime.Now,
                    CompletionDate = System.DateTime.Now.AddDays(3),
                    AssignedTo = "李师傅",
                    Priority = "中",
                    Notes = "专项检修"
                },
                new MaintenanceTask
                {
                    TaskId = 3,
                    VehicleId = 3,
                    TaskType = "转向架检修",
                    Status = "计划中",
                    ScheduledDate = System.DateTime.Now.AddDays(5),
                    CompletionDate = System.DateTime.Now.AddDays(7),
                    AssignedTo = "王师傅",
                    Priority = "高",
                    Notes = "转向架专项检修"
                }
            };
        }

        public List<Vehicle> GetAllVehicles()
        {
            return _vehicles;
        }

        public Vehicle GetVehicleById(int vehicleId)
        {
            return _vehicles.FirstOrDefault(v => v.VehicleId == vehicleId);
        }

        public Vehicle CreateVehicle(Vehicle vehicle)
        {
            vehicle.VehicleId = _vehicles.Max(v => v.VehicleId) + 1;
            _vehicles.Add(vehicle);
            return vehicle;
        }

        public Vehicle UpdateVehicle(Vehicle vehicle)
        {
            var existingVehicle = _vehicles.FirstOrDefault(v => v.VehicleId == vehicle.VehicleId);
            if (existingVehicle != null)
            {
                existingVehicle.VehicleNumber = vehicle.VehicleNumber;
                existingVehicle.VehicleModel = vehicle.VehicleModel;
                existingVehicle.VehicleType = vehicle.VehicleType;
                existingVehicle.Status = vehicle.Status;
                existingVehicle.Location = vehicle.Location;
                existingVehicle.Mileage = vehicle.Mileage;
                existingVehicle.LastMaintenanceDate = vehicle.LastMaintenanceDate;
                existingVehicle.NextMaintenanceDate = vehicle.NextMaintenanceDate;
                existingVehicle.Tags = vehicle.Tags;
            }
            return existingVehicle;
        }

        public bool DeleteVehicle(int vehicleId)
        {
            var vehicle = _vehicles.FirstOrDefault(v => v.VehicleId == vehicleId);
            if (vehicle != null)
            {
                _vehicles.Remove(vehicle);
                _vehicleStatuses.RemoveAll(s => s.VehicleId == vehicleId);
                _maintenanceTasks.RemoveAll(t => t.VehicleId == vehicleId);
                return true;
            }
            return false;
        }

        public List<VehicleStatus> GetVehicleStatus(int vehicleId)
        {
            return _vehicleStatuses.Where(s => s.VehicleId == vehicleId).ToList();
        }

        public VehicleStatus UpdateVehicleStatus(VehicleStatus status)
        {
            var existingStatus = _vehicleStatuses.FirstOrDefault(s => s.StatusId == status.StatusId);
            if (existingStatus != null)
            {
                existingStatus.Status = status.Status;
                existingStatus.Speed = status.Speed;
                existingStatus.Temperature = status.Temperature;
                existingStatus.Humidity = status.Humidity;
                existingStatus.BatteryLevel = status.BatteryLevel;
                existingStatus.Timestamp = status.Timestamp;
                existingStatus.Alerts = status.Alerts;
            }
            else
            {
                status.StatusId = _vehicleStatuses.Max(s => s.StatusId) + 1;
                _vehicleStatuses.Add(status);
            }
            return status;
        }

        public List<MaintenanceTask> GetVehicleMaintenanceTasks(int vehicleId)
        {
            return _maintenanceTasks.Where(t => t.VehicleId == vehicleId).ToList();
        }
    }
}