using System.Collections.Generic;
using System.Linq;

namespace EMU.DT.DeviceService
{
    public class EquipmentServiceImpl : IEquipmentService
    {
        private List<Equipment> _equipmentList;
        private List<EquipmentStatus> _equipmentStatuses;

        public EquipmentServiceImpl()
        {
            _equipmentList = new List<Equipment>
            {
                new Equipment
                {
                    EquipmentId = 1,
                    EquipmentName = "车轮踏面诊断装置",
                    EquipmentType = "检测设备",
                    Location = "检修库1",
                    Status = "运行中",
                    Model = "WTD-2000",
                    Manufacturer = "中国中车",
                    SerialNumber = "WTD2025001",
                    InstallationDate = System.DateTime.Now.AddYears(-2),
                    LastCalibrationDate = System.DateTime.Now.AddMonths(-1),
                    Certifications = new List<string> { "ISO9001", "铁路专用设备认证" }
                },
                new Equipment
                {
                    EquipmentId = 2,
                    EquipmentName = "架车机",
                    EquipmentType = "起重设备",
                    Location = "检修库2",
                    Status = "运行中",
                    Model = "JC-500",
                    Manufacturer = "中国中车",
                    SerialNumber = "JC2025002",
                    InstallationDate = System.DateTime.Now.AddYears(-1),
                    LastCalibrationDate = System.DateTime.Now.AddMonths(-2),
                    Certifications = new List<string> { "ISO9001" }
                },
                new Equipment
                {
                    EquipmentId = 3,
                    EquipmentName = "空压机站",
                    EquipmentType = "动力设备",
                    Location = "动力间",
                    Status = "运行中",
                    Model = "KYJ-1000",
                    Manufacturer = "阿特拉斯",
                    SerialNumber = "KYJ2025003",
                    InstallationDate = System.DateTime.Now.AddYears(-3),
                    LastCalibrationDate = System.DateTime.Now.AddMonths(-3),
                    Certifications = new List<string> { "ISO9001", "CE认证" }
                }
            };

            _equipmentStatuses = new List<EquipmentStatus>();
            foreach (var equipment in _equipmentList)
            {
                _equipmentStatuses.Add(new EquipmentStatus
                {
                    StatusId = equipment.EquipmentId,
                    EquipmentId = equipment.EquipmentId,
                    Status = equipment.Status,
                    OperationalHours = 1000 + new System.Random().NextDouble() * 500,
                    CalibrationStatus = 95 + new System.Random().NextDouble() * 5,
                    MaintenanceStatus = "正常",
                    Timestamp = System.DateTime.Now,
                    Issues = new List<string>()
                });
            }
        }

        public List<Equipment> GetAllEquipment()
        {
            return _equipmentList;
        }

        public Equipment GetEquipmentById(int equipmentId)
        {
            return _equipmentList.FirstOrDefault(e => e.EquipmentId == equipmentId);
        }

        public Equipment CreateEquipment(Equipment equipment)
        {
            equipment.EquipmentId = _equipmentList.Max(e => e.EquipmentId) + 1;
            _equipmentList.Add(equipment);
            return equipment;
        }

        public Equipment UpdateEquipment(Equipment equipment)
        {
            var existingEquipment = _equipmentList.FirstOrDefault(e => e.EquipmentId == equipment.EquipmentId);
            if (existingEquipment != null)
            {
                existingEquipment.EquipmentName = equipment.EquipmentName;
                existingEquipment.EquipmentType = equipment.EquipmentType;
                existingEquipment.Location = equipment.Location;
                existingEquipment.Status = equipment.Status;
                existingEquipment.Model = equipment.Model;
                existingEquipment.Manufacturer = equipment.Manufacturer;
                existingEquipment.SerialNumber = equipment.SerialNumber;
                existingEquipment.InstallationDate = equipment.InstallationDate;
                existingEquipment.LastCalibrationDate = equipment.LastCalibrationDate;
                existingEquipment.Certifications = equipment.Certifications;
            }
            return existingEquipment;
        }

        public bool DeleteEquipment(int equipmentId)
        {
            var equipment = _equipmentList.FirstOrDefault(e => e.EquipmentId == equipmentId);
            if (equipment != null)
            {
                _equipmentList.Remove(equipment);
                _equipmentStatuses.RemoveAll(s => s.EquipmentId == equipmentId);
                return true;
            }
            return false;
        }

        public List<EquipmentStatus> GetEquipmentStatus(int equipmentId)
        {
            return _equipmentStatuses.Where(s => s.EquipmentId == equipmentId).ToList();
        }

        public EquipmentStatus UpdateEquipmentStatus(EquipmentStatus status)
        {
            var existingStatus = _equipmentStatuses.FirstOrDefault(s => s.StatusId == status.StatusId);
            if (existingStatus != null)
            {
                existingStatus.Status = status.Status;
                existingStatus.OperationalHours = status.OperationalHours;
                existingStatus.CalibrationStatus = status.CalibrationStatus;
                existingStatus.MaintenanceStatus = status.MaintenanceStatus;
                existingStatus.Timestamp = status.Timestamp;
                existingStatus.Issues = status.Issues;
            }
            else
            {
                status.StatusId = _equipmentStatuses.Max(s => s.StatusId) + 1;
                _equipmentStatuses.Add(status);
            }
            return status;
        }
    }
}