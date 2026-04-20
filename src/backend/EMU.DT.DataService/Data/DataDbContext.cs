using Microsoft.EntityFrameworkCore;
using EMU.DT.DataService.Models;

namespace EMU.DT.DataService.Data
{
    public class DataDbContext : DbContext
    {
        public DataDbContext(DbContextOptions<DataDbContext> options) : base(options)
        {
        }

        public DbSet<Device> Devices { get; set; }
        public DbSet<DeviceStatus> DeviceStatuses { get; set; }
        public DbSet<DeviceMaintenance> DeviceMaintenances { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<VehicleStatus> VehicleStatuses { get; set; }
        public DbSet<MaintenanceTask> MaintenanceTasks { get; set; }
        public DbSet<MaintenanceStep> MaintenanceSteps { get; set; }
        public DbSet<ProcessCard> ProcessCards { get; set; }
        public DbSet<ProcessStep> ProcessSteps { get; set; }
        public DbSet<QualityCheck> QualityChecks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // 配置关系
            modelBuilder.Entity<DeviceStatus>()
                .HasOne(ds => ds.Device)
                .WithMany(d => d.Statuses)
                .HasForeignKey(ds => ds.DeviceId);

            modelBuilder.Entity<DeviceMaintenance>()
                .HasOne(dm => dm.Device)
                .WithMany(d => d.Maintenances)
                .HasForeignKey(dm => dm.DeviceId);

            modelBuilder.Entity<VehicleStatus>()
                .HasOne(vs => vs.Vehicle)
                .WithMany(v => v.Statuses)
                .HasForeignKey(vs => vs.VehicleId);

            modelBuilder.Entity<MaintenanceTask>()
                .HasOne(mt => mt.Vehicle)
                .WithMany(v => v.MaintenanceTasks)
                .HasForeignKey(mt => mt.VehicleId);

            modelBuilder.Entity<MaintenanceTask>()
                .HasOne(mt => mt.ProcessCard)
                .WithMany(pc => pc.MaintenanceTasks)
                .HasForeignKey(mt => mt.ProcessCardId);

            modelBuilder.Entity<MaintenanceStep>()
                .HasOne(ms => ms.MaintenanceTask)
                .WithMany(mt => mt.Steps)
                .HasForeignKey(ms => ms.MaintenanceTaskId);

            modelBuilder.Entity<ProcessStep>()
                .HasOne(ps => ps.ProcessCard)
                .WithMany(pc => pc.Steps)
                .HasForeignKey(ps => ps.ProcessCardId);

            modelBuilder.Entity<QualityCheck>()
                .HasOne(qc => qc.MaintenanceStep)
                .WithMany(ms => ms.QualityChecks)
                .HasForeignKey(qc => qc.MaintenanceStepId);

            // 初始化示例数据
            // 设备数据
            modelBuilder.Entity<Device>().HasData(
                new Device { Id = 1, Name = "不落轮镟床", Type = "机加工设备", Location = "A区", Model = "XCMG-123", SerialNumber = "SN001", InstallDate = DateTime.Now.AddYears(-2) },
                new Device { Id = 2, Name = "洗车机", Type = "清洗设备", Location = "B区", Model = "KARCHER-456", SerialNumber = "SN002", InstallDate = DateTime.Now.AddYears(-1) },
                new Device { Id = 3, Name = "地沟设备", Type = "检查设备", Location = "C区", Model = "SANY-789", SerialNumber = "SN003", InstallDate = DateTime.Now.AddYears(-3) },
                new Device { Id = 4, Name = "立体库", Type = "存储设备", Location = "D区", Model = "EFORK-321", SerialNumber = "SN004", InstallDate = DateTime.Now.AddYears(-1) },
                new Device { Id = 5, Name = "转向架试验台", Type = "试验设备", Location = "E区", Model = "HAITAI-654", SerialNumber = "SN005", InstallDate = DateTime.Now.AddYears(-2) }
            );

            // 车辆数据
            modelBuilder.Entity<Vehicle>().HasData(
                new Vehicle { Id = 1, Model = "CR400AF", Number = "2048", Type = "复兴号", TotalMileage = 1200000, LastMaintenanceDate = DateTime.Now.AddMonths(-1), Status = "在修" },
                new Vehicle { Id = 2, Model = "CRH380B", Number = "1024", Type = "和谐号", TotalMileage = 2500000, LastMaintenanceDate = DateTime.Now.AddMonths(-2), Status = "等待中" },
                new Vehicle { Id = 3, Model = "CR400BF", Number = "3072", Type = "复兴号", TotalMileage = 800000, LastMaintenanceDate = DateTime.Now.AddMonths(-1), Status = "异常处理" }
            );

            // 工艺卡片数据
            modelBuilder.Entity<ProcessCard>().HasData(
                new ProcessCard { Id = 1, Name = "CR400AF一级检修", Type = "一级修", VehicleModel = "CR400AF", Version = "V1.0", Description = "CR400AF车型的一级检修工艺" },
                new ProcessCard { Id = 2, Name = "CRH380B二级检修", Type = "二级修", VehicleModel = "CRH380B", Version = "V1.0", Description = "CRH380B车型的二级检修工艺" },
                new ProcessCard { Id = 3, Name = "CR400BF转向架检修", Type = "专项修", VehicleModel = "CR400BF", Version = "V1.0", Description = "CR400BF车型的转向架专项检修工艺" }
            );

            // 工艺步骤数据
            modelBuilder.Entity<ProcessStep>().HasData(
                new ProcessStep { Id = 1, ProcessCardId = 1, StepNumber = 1, Name = "接车入库", Description = "引导动车组进入检修库", Duration = 30 },
                new ProcessStep { Id = 2, ProcessCardId = 1, StepNumber = 2, Name = "断电降弓", Description = "切断动车组电源，降下受电弓", Duration = 15 },
                new ProcessStep { Id = 3, ProcessCardId = 1, StepNumber = 3, Name = "预检", Description = "对动车组进行初步检查", Duration = 20 },
                new ProcessStep { Id = 4, ProcessCardId = 1, StepNumber = 4, Name = "车顶设备检查", Description = "检查车顶设备状态", Duration = 45 },
                new ProcessStep { Id = 5, ProcessCardId = 1, StepNumber = 5, Name = "车下设备检查", Description = "检查车下设备状态", Duration = 60 },
                new ProcessStep { Id = 6, ProcessCardId = 1, StepNumber = 6, Name = "车内设施检查", Description = "检查车内设施状态", Duration = 30 },
                new ProcessStep { Id = 7, ProcessCardId = 1, StepNumber = 7, Name = "制动试验", Description = "进行制动系统试验", Duration = 25 },
                new ProcessStep { Id = 8, ProcessCardId = 1, StepNumber = 8, Name = "车轮踏面诊断", Description = "诊断车轮踏面状态", Duration = 20 },
                new ProcessStep { Id = 9, ProcessCardId = 1, StepNumber = 9, Name = "动车试验", Description = "进行动车试验", Duration = 30 },
                new ProcessStep { Id = 10, ProcessCardId = 1, StepNumber = 10, Name = "恢复送电", Description = "恢复动车组电源", Duration = 15 },
                new ProcessStep { Id = 11, ProcessCardId = 1, StepNumber = 11, Name = "出库", Description = "引导动车组出库", Duration = 20 }
            );
        }
    }
}