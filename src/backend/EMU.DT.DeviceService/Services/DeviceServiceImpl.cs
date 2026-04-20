using System.Collections.Generic;
using System.Linq;

namespace EMU.DT.DeviceService
{
    public class DeviceServiceImpl : IDeviceService
    {
        private List<Device> _devices;
        private List<DeviceStatus> _deviceStatuses;

        public DeviceServiceImpl()
        {
            _devices = new List<Device>
            {
                new Device
                {
                    DeviceId = 1,
                    DeviceName = "不落轮镟床",
                    DeviceType = "机加工设备",
                    Location = "检修库1",
                    Status = "运行中",
                    Model = "BRM-100",
                    Manufacturer = "中国中车",
                    SerialNumber = "BRM2025001",
                    InstallationDate = System.DateTime.Now.AddYears(-2),
                    LastMaintenanceDate = System.DateTime.Now.AddMonths(-1),
                    Tags = new List<string> { "关键设备", "一级维护" }
                },
                new Device
                {
                    DeviceId = 2,
                    DeviceName = "洗车机",
                    DeviceType = "清洗设备",
                    Location = "洗车线",
                    Status = "运行中",
                    Model = "CWJ-200",
                    Manufacturer = "中国中车",
                    SerialNumber = "CWJ2025002",
                    InstallationDate = System.DateTime.Now.AddYears(-1),
                    LastMaintenanceDate = System.DateTime.Now.AddMonths(-2),
                    Tags = new List<string> { "二级维护" }
                },
                new Device
                {
                    DeviceId = 3,
                    DeviceName = "地沟设备",
                    DeviceType = "检查设备",
                    Location = "检修库2",
                    Status = "警告",
                    Model = "DG-300",
                    Manufacturer = "中国中车",
                    SerialNumber = "DG2025003",
                    InstallationDate = System.DateTime.Now.AddYears(-3),
                    LastMaintenanceDate = System.DateTime.Now.AddMonths(-3),
                    Tags = new List<string> { "关键设备", "一级维护" }
                },
                new Device
                {
                    DeviceId = 4,
                    DeviceName = "立体库",
                    DeviceType = "存储设备",
                    Location = "仓库区",
                    Status = "运行中",
                    Model = "AS/RS-500",
                    Manufacturer = "中国中车",
                    SerialNumber = "ASRS2025004",
                    InstallationDate = System.DateTime.Now.AddYears(-1),
                    LastMaintenanceDate = System.DateTime.Now.AddMonths(-1),
                    Tags = new List<string> { "二级维护" }
                }
            };

            _deviceStatuses = new List<DeviceStatus>();
            foreach (var device in _devices)
            {
                _deviceStatuses.Add(new DeviceStatus
                {
                    StatusId = device.DeviceId,
                    DeviceId = device.DeviceId,
                    Status = device.Status,
                    Temperature = 45 + new System.Random().NextDouble() * 10,
                    Vibration = 2.5 + new System.Random().NextDouble() * 1.5,
                    Pressure = 3.0 + new System.Random().NextDouble() * 0.5,
                    Humidity = 45 + new System.Random().NextDouble() * 10,
                    Power = 10 + new System.Random().NextDouble() * 5,
                    Timestamp = System.DateTime.Now,
                    Alerts = device.Status == "警告" ? new List<string> { "振动值偏高" } : new List<string>()
                });
            }
        }

        public List<Device> GetAllDevices()
        {
            return _devices;
        }

        public Device GetDeviceById(int deviceId)
        {
            return _devices.FirstOrDefault(d => d.DeviceId == deviceId);
        }

        public Device CreateDevice(Device device)
        {
            device.DeviceId = _devices.Max(d => d.DeviceId) + 1;
            _devices.Add(device);
            return device;
        }

        public Device UpdateDevice(Device device)
        {
            var existingDevice = _devices.FirstOrDefault(d => d.DeviceId == device.DeviceId);
            if (existingDevice != null)
            {
                existingDevice.DeviceName = device.DeviceName;
                existingDevice.DeviceType = device.DeviceType;
                existingDevice.Location = device.Location;
                existingDevice.Status = device.Status;
                existingDevice.Model = device.Model;
                existingDevice.Manufacturer = device.Manufacturer;
                existingDevice.SerialNumber = device.SerialNumber;
                existingDevice.InstallationDate = device.InstallationDate;
                existingDevice.LastMaintenanceDate = device.LastMaintenanceDate;
                existingDevice.Tags = device.Tags;
            }
            return existingDevice;
        }

        public bool DeleteDevice(int deviceId)
        {
            var device = _devices.FirstOrDefault(d => d.DeviceId == deviceId);
            if (device != null)
            {
                _devices.Remove(device);
                _deviceStatuses.RemoveAll(s => s.DeviceId == deviceId);
                return true;
            }
            return false;
        }

        public List<DeviceStatus> GetDeviceStatus(int deviceId)
        {
            return _deviceStatuses.Where(s => s.DeviceId == deviceId).ToList();
        }

        public DeviceStatus UpdateDeviceStatus(DeviceStatus status)
        {
            var existingStatus = _deviceStatuses.FirstOrDefault(s => s.StatusId == status.StatusId);
            if (existingStatus != null)
            {
                existingStatus.Status = status.Status;
                existingStatus.Temperature = status.Temperature;
                existingStatus.Vibration = status.Vibration;
                existingStatus.Pressure = status.Pressure;
                existingStatus.Humidity = status.Humidity;
                existingStatus.Power = status.Power;
                existingStatus.Timestamp = status.Timestamp;
                existingStatus.Alerts = status.Alerts;
            }
            else
            {
                status.StatusId = _deviceStatuses.Max(s => s.StatusId) + 1;
                _deviceStatuses.Add(status);
            }
            return status;
        }
    }
}