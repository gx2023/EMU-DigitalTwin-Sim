
using EMU.DT.Shared.DTOs;
using EMU.DT.Shared.Models;

namespace EMU.DT.Shared.Extensions;

public static class MappingExtensions
{
    public static UserDto ToDto(this UserInfo user)
    {
        return new UserDto
        {
            Id = user.Id,
            Username = user.Username,
            Email = user.Email,
            RealName = user.RealName,
            PhoneNumber = user.PhoneNumber,
            Role = user.Role,
            IsActive = user.IsActive,
            CreatedAt = user.CreatedAt,
            UpdatedAt = user.UpdatedAt
        };
    }

    public static EmuDto ToDto(this EmuInfo emu)
    {
        return new EmuDto
        {
            Id = emu.Id,
            EmuType = emu.EmuType,
            EmuSeries = emu.EmuSeries,
            TrainSetNo = emu.TrainSetNo,
            ManufacturingDate = emu.ManufacturingDate,
            DepotId = emu.DepotId,
            Status = emu.Status,
            Mileage = emu.Mileage,
            Remark = emu.Remark,
            CreatedAt = emu.CreatedAt
        };
    }

    public static WorkOrderDto ToDto(this WorkOrder order)
    {
        return new WorkOrderDto
        {
            Id = order.Id,
            OrderNo = order.OrderNo,
            EmuId = order.EmuId,
            MaintenanceLevel = order.MaintenanceLevel,
            DepotId = order.DepotId,
            PlatformId = order.PlatformId,
            Status = order.Status,
            PlannedStartTime = order.PlannedStartTime,
            PlannedEndTime = order.PlannedEndTime,
            ActualStartTime = order.ActualStartTime,
            ActualEndTime = order.ActualEndTime,
            ManagerId = order.ManagerId,
            Description = order.Description,
            Remark = order.Remark,
            CreatedAt = order.CreatedAt
        };
    }
}
