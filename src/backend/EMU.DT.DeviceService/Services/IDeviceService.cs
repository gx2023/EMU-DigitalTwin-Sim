using System.Collections.Generic;

namespace EMU.DT.DeviceService
{
    public interface IDeviceService
    {
        List<Device> GetAllDevices();
        Device GetDeviceById(int deviceId);
        Device CreateDevice(Device device);
        Device UpdateDevice(Device device);
        bool DeleteDevice(int deviceId);
        List<DeviceStatus> GetDeviceStatus(int deviceId);
        DeviceStatus UpdateDeviceStatus(DeviceStatus status);
    }

    public class Device
    {
        public int DeviceId { get; set; }
        public string DeviceName { get; set; }
        public string DeviceType { get; set; }
        public string Location { get; set; }
        public string Status { get; set; }
        public string Model { get; set; }
        public string Manufacturer { get; set; }
        public string SerialNumber { get; set; }
        public System.DateTime InstallationDate { get; set; }
        public System.DateTime LastMaintenanceDate { get; set; }
        public List<string> Tags { get; set; }
    }

    public class DeviceStatus
    {
        public int StatusId { get; set; }
        public int DeviceId { get; set; }
        public string Status { get; set; }
        public double Temperature { get; set; }
        public double Vibration { get; set; }
        public double Pressure { get; set; }
        public double Humidity { get; set; }
        public double Power { get; set; }
        public System.DateTime Timestamp { get; set; }
        public List<string> Alerts { get; set; }
    }
}