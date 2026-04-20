using System;
using System.Collections.Generic;
using System.Linq;

namespace EMU.DT.Algorithm.DataGeneration
{
    public class DataGenerator
    {
        private Random _random;

        public DataGenerator()
        {
            _random = new Random();
        }

        public List<DeviceData> GenerateDeviceData(int deviceCount, int dataPoints)
        {
            var deviceData = new List<DeviceData>();

            for (int i = 1; i <= deviceCount; i++)
            {
                var device = new DeviceData
                {
                    DeviceId = i,
                    DeviceName = $"设备{i}",
                    DeviceType = GetRandomDeviceType(),
                    StatusData = GenerateStatusData(dataPoints)
                };

                deviceData.Add(device);
            }

            return deviceData;
        }

        public List<VehicleData> GenerateVehicleData(int vehicleCount, int dataPoints)
        {
            var vehicleData = new List<VehicleData>();

            for (int i = 1; i <= vehicleCount; i++)
            {
                var vehicle = new VehicleData
                {
                    VehicleId = i,
                    VehicleNumber = $"CRH{GetRandomVehicleType()}-{i:0000}",
                    VehicleModel = GetRandomVehicleModel(),
                    StatusData = GenerateVehicleStatusData(dataPoints),
                    MaintenanceData = GenerateMaintenanceData(i, dataPoints)
                };

                vehicleData.Add(vehicle);
            }

            return vehicleData;
        }

        private List<StatusData> GenerateStatusData(int dataPoints)
        {
            var statusData = new List<StatusData>();
            var baseValue = _random.Next(50, 100);

            for (int i = 0; i < dataPoints; i++)
            {
                var timestamp = DateTime.Now.AddMinutes(-i * 10);
                var value = baseValue + (_random.NextDouble() * 10 - 5); // 添加随机波动
                var status = value > 90 ? "正常" : value > 70 ? "警告" : "异常";

                statusData.Add(new StatusData
                {
                    Timestamp = timestamp,
                    Value = Math.Round(value, 2),
                    Status = status
                });
            }

            return statusData;
        }

        private List<VehicleStatusData> GenerateVehicleStatusData(int dataPoints)
        {
            var statusData = new List<VehicleStatusData>();

            for (int i = 0; i < dataPoints; i++)
            {
                var timestamp = DateTime.Now.AddMinutes(-i * 10);

                statusData.Add(new VehicleStatusData
                {
                    Timestamp = timestamp,
                    Speed = _random.Next(0, 350),
                    Temperature = _random.Next(10, 40),
                    Humidity = _random.Next(30, 80),
                    BatteryLevel = _random.Next(70, 100),
                    Status = GetRandomVehicleStatus()
                });
            }

            return statusData;
        }

        private List<MaintenanceData> GenerateMaintenanceData(int vehicleId, int dataPoints)
        {
            var maintenanceData = new List<MaintenanceData>();

            for (int i = 0; i < dataPoints; i++)
            {
                var timestamp = DateTime.Now.AddDays(-i * 7);

                maintenanceData.Add(new MaintenanceData
                {
                    MaintenanceId = i + 1,
                    VehicleId = vehicleId,
                    MaintenanceType = GetRandomMaintenanceType(),
                    StartTime = timestamp,
                    EndTime = timestamp.AddHours(_random.Next(2, 8)),
                    Result = GetRandomMaintenanceResult(),
                    WorkerId = _random.Next(1, 10)
                });
            }

            return maintenanceData;
        }

        private string GetRandomDeviceType()
        {
            var types = new[] { "不落轮镟床", "洗车机", "地沟设备", "立体库", "空压机", "起重机" };
            return types[_random.Next(types.Length)];
        }

        private string GetRandomVehicleType()
        {
            var types = new[] { "380A", "380B", "400AF", "400BF" };
            return types[_random.Next(types.Length)];
        }

        private string GetRandomVehicleModel()
        {
            var models = new[] { "CRH380A", "CRH380B", "CR400AF", "CR400BF" };
            return models[_random.Next(models.Length)];
        }

        private string GetRandomVehicleStatus()
        {
            var statuses = new[] { "运行中", "静止", "检修中", "故障" };
            return statuses[_random.Next(statuses.Length)];
        }

        private string GetRandomMaintenanceType()
        {
            var types = new[] { "一级修", "二级修", "临修" };
            return types[_random.Next(types.Length)];
        }

        private string GetRandomMaintenanceResult()
        {
            var results = new[] { "合格", "不合格", "需返工" };
            return results[_random.Next(results.Length)];
        }
    }

    public class DeviceData
    {
        public int DeviceId { get; set; }
        public string DeviceName { get; set; }
        public string DeviceType { get; set; }
        public List<StatusData> StatusData { get; set; }
    }

    public class StatusData
    {
        public DateTime Timestamp { get; set; }
        public double Value { get; set; }
        public string Status { get; set; }
    }

    public class VehicleData
    {
        public int VehicleId { get; set; }
        public string VehicleNumber { get; set; }
        public string VehicleModel { get; set; }
        public List<VehicleStatusData> StatusData { get; set; }
        public List<MaintenanceData> MaintenanceData { get; set; }
    }

    public class VehicleStatusData
    {
        public DateTime Timestamp { get; set; }
        public int Speed { get; set; }
        public int Temperature { get; set; }
        public int Humidity { get; set; }
        public int BatteryLevel { get; set; }
        public string Status { get; set; }
    }

    public class MaintenanceData
    {
        public int MaintenanceId { get; set; }
        public int VehicleId { get; set; }
        public string MaintenanceType { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Result { get; set; }
        public int WorkerId { get; set; }
    }
}