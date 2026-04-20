using System.Collections.Generic;
using System.Linq;

namespace EMU.DT.DeviceService
{
    public class MaintenanceServiceImpl : IMaintenanceService
    {
        private List<MaintenanceTask> _maintenanceTasks;
        private List<MaintenanceRecord> _maintenanceRecords;
        private List<MaintenancePlan> _maintenancePlans;

        public MaintenanceServiceImpl()
        {
            _maintenanceTasks = new List<MaintenanceTask>
            {
                new MaintenanceTask
                {
                    TaskId = 1,
                    TaskName = "不落轮镟床月度维护",
                    DeviceId = "1",
                    EquipmentId = "",
                    TaskType = "预防性维护",
                    Priority = "中",
                    Status = "进行中",
                    ScheduledDate = System.DateTime.Now.AddDays(-1),
                    DueDate = System.DateTime.Now.AddDays(2),
                    AssignedTo = "张三",
                    Description = "进行月度维护检查，包括润滑、校准和功能测试",
                    PartsRequired = new List<string> { "润滑油", "过滤器" }
                },
                new MaintenanceTask
                {
                    TaskId = 2,
                    TaskName = "地沟设备振动异常处理",
                    DeviceId = "3",
                    EquipmentId = "",
                    TaskType = "故障维修",
                    Priority = "高",
                    Status = "待处理",
                    ScheduledDate = System.DateTime.Now,
                    DueDate = System.DateTime.Now.AddDays(1),
                    AssignedTo = "李四",
                    Description = "处理地沟设备振动值偏高的问题",
                    PartsRequired = new List<string> { "轴承", "密封件" }
                },
                new MaintenanceTask
                {
                    TaskId = 3,
                    TaskName = "洗车机季度保养",
                    DeviceId = "2",
                    EquipmentId = "",
                    TaskType = "预防性维护",
                    Priority = "低",
                    Status = "计划中",
                    ScheduledDate = System.DateTime.Now.AddDays(7),
                    DueDate = System.DateTime.Now.AddDays(10),
                    AssignedTo = "王五",
                    Description = "进行季度保养，包括清洗、润滑和检查",
                    PartsRequired = new List<string> { "清洁剂", "润滑油" }
                }
            };

            _maintenanceRecords = new List<MaintenanceRecord>
            {
                new MaintenanceRecord
                {
                    RecordId = 1,
                    DeviceId = 1,
                    EquipmentId = 0,
                    MaintenanceType = "月度维护",
                    MaintenanceDate = System.DateTime.Now.AddMonths(-1),
                    PerformedBy = "张三",
                    Description = "进行了月度维护检查",
                    PartsReplaced = new List<string> { "过滤器" },
                    Status = "完成",
                    DurationHours = 2.5,
                    Notes = "设备运行正常"
                },
                new MaintenanceRecord
                {
                    RecordId = 2,
                    DeviceId = 2,
                    EquipmentId = 0,
                    MaintenanceType = "季度保养",
                    MaintenanceDate = System.DateTime.Now.AddMonths(-3),
                    PerformedBy = "王五",
                    Description = "进行了季度保养",
                    PartsReplaced = new List<string> { "清洁剂", "润滑油" },
                    Status = "完成",
                    DurationHours = 4.0,
                    Notes = "设备运行正常"
                }
            };

            _maintenancePlans = new List<MaintenancePlan>
            {
                new MaintenancePlan
                {
                    PlanId = 1,
                    PlanName = "不落轮镟床维护计划",
                    DeviceType = "机加工设备",
                    EquipmentType = "",
                    MaintenanceType = "预防性维护",
                    IntervalDays = 30,
                    Tasks = new List<string> { "润滑检查", "校准", "功能测试", "清洁" },
                    RequiredSkills = new List<string> { "机修工", "电工" },
                    Instructions = "按照维护手册进行操作",
                    CreatedDate = System.DateTime.Now.AddMonths(-6),
                    LastUpdatedDate = System.DateTime.Now.AddMonths(-1)
                },
                new MaintenancePlan
                {
                    PlanId = 2,
                    PlanName = "洗车机维护计划",
                    DeviceType = "清洗设备",
                    EquipmentType = "",
                    MaintenanceType = "预防性维护",
                    IntervalDays = 90,
                    Tasks = new List<string> { "清洗", "润滑", "检查", "校准" },
                    RequiredSkills = new List<string> { "机修工" },
                    Instructions = "按照维护手册进行操作",
                    CreatedDate = System.DateTime.Now.AddMonths(-6),
                    LastUpdatedDate = System.DateTime.Now.AddMonths(-1)
                }
            };
        }

        public List<MaintenanceTask> GetAllMaintenanceTasks()
        {
            return _maintenanceTasks;
        }

        public MaintenanceTask GetMaintenanceTaskById(int taskId)
        {
            return _maintenanceTasks.FirstOrDefault(t => t.TaskId == taskId);
        }

        public MaintenanceTask CreateMaintenanceTask(MaintenanceTask task)
        {
            task.TaskId = _maintenanceTasks.Max(t => t.TaskId) + 1;
            _maintenanceTasks.Add(task);
            return task;
        }

        public MaintenanceTask UpdateMaintenanceTask(MaintenanceTask task)
        {
            var existingTask = _maintenanceTasks.FirstOrDefault(t => t.TaskId == task.TaskId);
            if (existingTask != null)
            {
                existingTask.TaskName = task.TaskName;
                existingTask.DeviceId = task.DeviceId;
                existingTask.EquipmentId = task.EquipmentId;
                existingTask.TaskType = task.TaskType;
                existingTask.Priority = task.Priority;
                existingTask.Status = task.Status;
                existingTask.ScheduledDate = task.ScheduledDate;
                existingTask.DueDate = task.DueDate;
                existingTask.CompletedDate = task.CompletedDate;
                existingTask.AssignedTo = task.AssignedTo;
                existingTask.Description = task.Description;
                existingTask.PartsRequired = task.PartsRequired;
            }
            return existingTask;
        }

        public bool DeleteMaintenanceTask(int taskId)
        {
            var task = _maintenanceTasks.FirstOrDefault(t => t.TaskId == taskId);
            if (task != null)
            {
                _maintenanceTasks.Remove(task);
                return true;
            }
            return false;
        }

        public List<MaintenanceRecord> GetMaintenanceRecords(int deviceId)
        {
            return _maintenanceRecords.Where(r => r.DeviceId == deviceId).ToList();
        }

        public MaintenanceRecord CreateMaintenanceRecord(MaintenanceRecord record)
        {
            record.RecordId = _maintenanceRecords.Max(r => r.RecordId) + 1;
            _maintenanceRecords.Add(record);
            return record;
        }

        public List<MaintenancePlan> GetMaintenancePlans()
        {
            return _maintenancePlans;
        }

        public MaintenancePlan CreateMaintenancePlan(MaintenancePlan plan)
        {
            plan.PlanId = _maintenancePlans.Max(p => p.PlanId) + 1;
            plan.CreatedDate = System.DateTime.Now;
            plan.LastUpdatedDate = System.DateTime.Now;
            _maintenancePlans.Add(plan);
            return plan;
        }
    }
}