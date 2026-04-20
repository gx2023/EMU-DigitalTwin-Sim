using System;
using System.Collections.Generic;
using System.Linq;

namespace EMU.DT.Algorithm.Scheduling
{
    public class SchedulingOptimizer
    {
        public List<MaintenanceSchedule> OptimizeMaintenanceSchedule(List<MaintenanceTask> tasks, List<MaintenanceBay> bays, List<Worker> workers)
        {
            // 按照任务优先级和截止时间排序
            var sortedTasks = tasks.OrderByDescending(t => t.Priority)
                .ThenBy(t => t.DueDate)
                .ToList();

            var schedules = new List<MaintenanceSchedule>();
            var bayAssignments = new Dictionary<int, DateTime>(); // 台位ID -> 可用时间
            var workerAssignments = new Dictionary<int, DateTime>(); // 工人ID -> 可用时间

            // 初始化台位和工人的可用时间
            foreach (var bay in bays)
            {
                bayAssignments[bay.Id] = DateTime.Now;
            }

            foreach (var worker in workers)
            {
                workerAssignments[worker.Id] = DateTime.Now;
            }

            foreach (var task in sortedTasks)
            {
                // 找到最早可用的台位
                var availableBay = FindEarliestAvailableBay(bayAssignments, task.RequiredBayType);
                if (availableBay == null)
                {
                    // 没有可用台位，跳过此任务
                    continue;
                }

                // 找到最早可用的工人
                var availableWorker = FindEarliestAvailableWorker(workerAssignments, task.RequiredSkills);
                if (availableWorker == null)
                {
                    // 没有可用工人，跳过此任务
                    continue;
                }

                // 计算开始时间（台位和工人都可用的时间）
                var startTime = Math.Max(bayAssignments[availableBay.Id], workerAssignments[availableWorker.Id]);
                var endTime = startTime.AddHours(task.EstimatedDuration);

                // 创建调度计划
                var schedule = new MaintenanceSchedule
                {
                    TaskId = task.Id,
                    BayId = availableBay.Id,
                    WorkerId = availableWorker.Id,
                    StartTime = startTime,
                    EndTime = endTime
                };

                schedules.Add(schedule);

                // 更新台位和工人的可用时间
                bayAssignments[availableBay.Id] = endTime;
                workerAssignments[availableWorker.Id] = endTime;
            }

            return schedules;
        }

        private MaintenanceBay FindEarliestAvailableBay(Dictionary<int, DateTime> bayAssignments, string requiredBayType)
        {
            // 这里简化处理，实际应该根据台位类型进行筛选
            return bayAssignments
                .OrderBy(kv => kv.Value)
                .Select(kv => new MaintenanceBay { Id = kv.Key, Type = requiredBayType })
                .FirstOrDefault();
        }

        private Worker FindEarliestAvailableWorker(Dictionary<int, DateTime> workerAssignments, List<string> requiredSkills)
        {
            // 这里简化处理，实际应该根据工人技能进行筛选
            return workerAssignments
                .OrderBy(kv => kv.Value)
                .Select(kv => new Worker { Id = kv.Key, Skills = requiredSkills })
                .FirstOrDefault();
        }
    }

    public class MaintenanceTask
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public int Priority { get; set; }
        public DateTime DueDate { get; set; }
        public double EstimatedDuration { get; set; }
        public string RequiredBayType { get; set; }
        public List<string> RequiredSkills { get; set; }
    }

    public class MaintenanceBay
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public bool IsAvailable { get; set; }
    }

    public class Worker
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<string> Skills { get; set; }
    }

    public class MaintenanceSchedule
    {
        public int TaskId { get; set; }
        public int BayId { get; set; }
        public int WorkerId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}