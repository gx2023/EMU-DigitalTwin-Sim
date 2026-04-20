using EMU.DT.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace EMU.DT.DataService.Data;

public class DataDbContext : DbContext
{
    public DataDbContext(DbContextOptions<DataDbContext> options)
        : base(options)
    {
    }

    public DbSet<EmuInfo> Emus { get; set; }
    public DbSet<DepotInfo> Depots { get; set; }
    public DbSet<TrackInfo> Tracks { get; set; }
    public DbSet<PlatformInfo> Platforms { get; set; }
    public DbSet<DeviceInfo> Devices { get; set; }
    public DbSet<WorkOrder> WorkOrders { get; set; }
    public DbSet<AlertRecord> Alerts { get; set; }
}
