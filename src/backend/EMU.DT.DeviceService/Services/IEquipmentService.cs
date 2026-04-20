using System.Collections.Generic;

namespace EMU.DT.DeviceService
{
    public interface IEquipmentService
    {
        List<Equipment> GetAllEquipment();
        Equipment GetEquipmentById(int equipmentId);
        Equipment CreateEquipment(Equipment equipment);
        Equipment UpdateEquipment(Equipment equipment);
        bool DeleteEquipment(int equipmentId);
        List<EquipmentStatus> GetEquipmentStatus(int equipmentId);
        EquipmentStatus UpdateEquipmentStatus(EquipmentStatus status);
    }

    public class Equipment
    {
        public int EquipmentId { get; set; }
        public string EquipmentName { get; set; }
        public string EquipmentType { get; set; }
        public string Location { get; set; }
        public string Status { get; set; }
        public string Model { get; set; }
        public string Manufacturer { get; set; }
        public string SerialNumber { get; set; }
        public System.DateTime InstallationDate { get; set; }
        public System.DateTime LastCalibrationDate { get; set; }
        public List<string> Certifications { get; set; }
    }

    public class EquipmentStatus
    {
        public int StatusId { get; set; }
        public int EquipmentId { get; set; }
        public string Status { get; set; }
        public double OperationalHours { get; set; }
        public double CalibrationStatus { get; set; }
        public string MaintenanceStatus { get; set; }
        public System.DateTime Timestamp { get; set; }
        public List<string> Issues { get; set; }
    }
}