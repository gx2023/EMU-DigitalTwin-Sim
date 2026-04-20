using System.Collections.Generic;
using System.Linq;

namespace EMU.DT.SimulationService
{
    public class SegmentFactoryServiceImpl : ISegmentFactoryService
    {
        private List<SegmentFactory> _segmentFactories;
        private List<Bay> _bays;
        private List<Track> _tracks;

        public SegmentFactoryServiceImpl()
        {
            _segmentFactories = new List<SegmentFactory>
            {
                new SegmentFactory
                {
                    FactoryId = 1,
                    Name = "北京动车段",
                    Description = "主要承担CR400AF和CRH380B型动车组的一二级修",
                    Location = "北京市大兴区",
                    TotalBays = 8,
                    TotalTracks = 12,
                    Status = "运行中",
                    CreatedDate = System.DateTime.Now.AddYears(-5),
                    LastUpdatedDate = System.DateTime.Now.AddMonths(-1),
                    Bays = new List<Bay>(),
                    Tracks = new List<Track>()
                },
                new SegmentFactory
                {
                    FactoryId = 2,
                    Name = "上海动车段",
                    Description = "主要承担CR400BF和CRH380A型动车组的一二级修",
                    Location = "上海市嘉定区",
                    TotalBays = 10,
                    TotalTracks = 15,
                    Status = "运行中",
                    CreatedDate = System.DateTime.Now.AddYears(-4),
                    LastUpdatedDate = System.DateTime.Now.AddMonths(-2),
                    Bays = new List<Bay>(),
                    Tracks = new List<Track>()
                }
            };

            _bays = new List<Bay>
            {
                new Bay
                {
                    BayId = 1,
                    FactoryId = 1,
                    BayNumber = "1",
                    Type = "一级修台位",
                    Status = "占用",
                    VehicleId = "1",
                    Length = 200.0,
                    Width = 10.0,
                    Equipment = new List<string> { "地沟设备", "不落轮镟床" },
                    LastMaintenanceDate = System.DateTime.Now.AddMonths(-1)
                },
                new Bay
                {
                    BayId = 2,
                    FactoryId = 1,
                    BayNumber = "2",
                    Type = "一级修台位",
                    Status = "空闲",
                    VehicleId = "",
                    Length = 200.0,
                    Width = 10.0,
                    Equipment = new List<string> { "地沟设备" },
                    LastMaintenanceDate = System.DateTime.Now.AddMonths(-1)
                },
                new Bay
                {
                    BayId = 3,
                    FactoryId = 1,
                    BayNumber = "3",
                    Type = "二级修台位",
                    Status = "占用",
                    VehicleId = "2",
                    Length = 200.0,
                    Width = 12.0,
                    Equipment = new List<string> { "地沟设备", "架车机" },
                    LastMaintenanceDate = System.DateTime.Now.AddMonths(-2)
                },
                new Bay
                {
                    BayId = 4,
                    FactoryId = 2,
                    BayNumber = "1",
                    Type = "一级修台位",
                    Status = "空闲",
                    VehicleId = "",
                    Length = 200.0,
                    Width = 10.0,
                    Equipment = new List<string> { "地沟设备" },
                    LastMaintenanceDate = System.DateTime.Now.AddMonths(-1)
                }
            };

            _tracks = new List<Track>
            {
                new Track
                {
                    TrackId = 1,
                    FactoryId = 1,
                    TrackNumber = "1",
                    Type = "入库线",
                    Status = "正常",
                    Length = 500.0,
                    MaxSpeed = 25,
                    ConnectedBays = new List<int> { 1, 2 },
                    Signals = new List<Signal>
                    {
                        new Signal
                        {
                            SignalId = 1,
                            TrackId = 1,
                            SignalNumber = "S1",
                            Type = "进站信号机",
                            Status = "绿色",
                            Position = 100.0
                        }
                    }
                },
                new Track
                {
                    TrackId = 2,
                    FactoryId = 1,
                    TrackNumber = "2",
                    Type = "出库线",
                    Status = "正常",
                    Length = 500.0,
                    MaxSpeed = 25,
                    ConnectedBays = new List<int> { 1, 2, 3 },
                    Signals = new List<Signal>
                    {
                        new Signal
                        {
                            SignalId = 2,
                            TrackId = 2,
                            SignalNumber = "S2",
                            Type = "出站信号机",
                            Status = "红色",
                            Position = 150.0
                        }
                    }
                },
                new Track
                {
                    TrackId = 3,
                    FactoryId = 2,
                    TrackNumber = "1",
                    Type = "入库线",
                    Status = "正常",
                    Length = 600.0,
                    MaxSpeed = 25,
                    ConnectedBays = new List<int> { 4 },
                    Signals = new List<Signal>
                    {
                        new Signal
                        {
                            SignalId = 3,
                            TrackId = 3,
                            SignalNumber = "S3",
                            Type = "进站信号机",
                            Status = "绿色",
                            Position = 120.0
                        }
                    }
                }
            };

            // 关联关系
            foreach (var factory in _segmentFactories)
            {
                factory.Bays = _bays.Where(b => b.FactoryId == factory.FactoryId).ToList();
                factory.Tracks = _tracks.Where(t => t.FactoryId == factory.FactoryId).ToList();
            }
        }

        public List<SegmentFactory> GetAllSegmentFactories()
        {
            return _segmentFactories;
        }

        public SegmentFactory GetSegmentFactoryById(int factoryId)
        {
            return _segmentFactories.FirstOrDefault(f => f.FactoryId == factoryId);
        }

        public SegmentFactory CreateSegmentFactory(SegmentFactory factory)
        {
            factory.FactoryId = _segmentFactories.Max(f => f.FactoryId) + 1;
            factory.CreatedDate = System.DateTime.Now;
            factory.LastUpdatedDate = System.DateTime.Now;
            factory.Status = "运行中";
            factory.Bays = new List<Bay>();
            factory.Tracks = new List<Track>();
            _segmentFactories.Add(factory);
            return factory;
        }

        public SegmentFactory UpdateSegmentFactory(SegmentFactory factory)
        {
            var existingFactory = _segmentFactories.FirstOrDefault(f => f.FactoryId == factory.FactoryId);
            if (existingFactory != null)
            {
                existingFactory.Name = factory.Name;
                existingFactory.Description = factory.Description;
                existingFactory.Location = factory.Location;
                existingFactory.TotalBays = factory.TotalBays;
                existingFactory.TotalTracks = factory.TotalTracks;
                existingFactory.Status = factory.Status;
                existingFactory.LastUpdatedDate = System.DateTime.Now;
            }
            return existingFactory;
        }

        public bool DeleteSegmentFactory(int factoryId)
        {
            var factory = _segmentFactories.FirstOrDefault(f => f.FactoryId == factoryId);
            if (factory != null)
            {
                _segmentFactories.Remove(factory);
                _bays.RemoveAll(b => b.FactoryId == factoryId);
                _tracks.RemoveAll(t => t.FactoryId == factoryId);
                return true;
            }
            return false;
        }

        public List<Bay> GetBaysByFactoryId(int factoryId)
        {
            return _bays.Where(b => b.FactoryId == factoryId).ToList();
        }

        public Bay GetBayById(int bayId)
        {
            return _bays.FirstOrDefault(b => b.BayId == bayId);
        }

        public Bay CreateBay(Bay bay)
        {
            bay.BayId = _bays.Max(b => b.BayId) + 1;
            bay.LastMaintenanceDate = System.DateTime.Now;
            _bays.Add(bay);
            
            // 更新段厂的台位数量
            var factory = _segmentFactories.FirstOrDefault(f => f.FactoryId == bay.FactoryId);
            if (factory != null)
            {
                factory.TotalBays = _bays.Count(b => b.FactoryId == bay.FactoryId);
                factory.LastUpdatedDate = System.DateTime.Now;
            }
            
            return bay;
        }

        public Bay UpdateBay(Bay bay)
        {
            var existingBay = _bays.FirstOrDefault(b => b.BayId == bay.BayId);
            if (existingBay != null)
            {
                existingBay.BayNumber = bay.BayNumber;
                existingBay.Type = bay.Type;
                existingBay.Status = bay.Status;
                existingBay.VehicleId = bay.VehicleId;
                existingBay.Length = bay.Length;
                existingBay.Width = bay.Width;
                existingBay.Equipment = bay.Equipment;
                existingBay.LastMaintenanceDate = bay.LastMaintenanceDate;
            }
            return existingBay;
        }

        public bool DeleteBay(int bayId)
        {
            var bay = _bays.FirstOrDefault(b => b.BayId == bayId);
            if (bay != null)
            {
                _bays.Remove(bay);
                
                // 更新段厂的台位数量
                var factory = _segmentFactories.FirstOrDefault(f => f.FactoryId == bay.FactoryId);
                if (factory != null)
                {
                    factory.TotalBays = _bays.Count(b => b.FactoryId == bay.FactoryId);
                    factory.LastUpdatedDate = System.DateTime.Now;
                }
                
                return true;
            }
            return false;
        }

        public List<Track> GetTracksByFactoryId(int factoryId)
        {
            return _tracks.Where(t => t.FactoryId == factoryId).ToList();
        }

        public Track GetTrackById(int trackId)
        {
            return _tracks.FirstOrDefault(t => t.TrackId == trackId);
        }

        public Track CreateTrack(Track track)
        {
            track.TrackId = _tracks.Max(t => t.TrackId) + 1;
            _tracks.Add(track);
            
            // 更新段厂的轨道数量
            var factory = _segmentFactories.FirstOrDefault(f => f.FactoryId == track.FactoryId);
            if (factory != null)
            {
                factory.TotalTracks = _tracks.Count(t => t.FactoryId == track.FactoryId);
                factory.LastUpdatedDate = System.DateTime.Now;
            }
            
            return track;
        }

        public Track UpdateTrack(Track track)
        {
            var existingTrack = _tracks.FirstOrDefault(t => t.TrackId == track.TrackId);
            if (existingTrack != null)
            {
                existingTrack.TrackNumber = track.TrackNumber;
                existingTrack.Type = track.Type;
                existingTrack.Status = track.Status;
                existingTrack.Length = track.Length;
                existingTrack.MaxSpeed = track.MaxSpeed;
                existingTrack.ConnectedBays = track.ConnectedBays;
                existingTrack.Signals = track.Signals;
            }
            return existingTrack;
        }

        public bool DeleteTrack(int trackId)
        {
            var track = _tracks.FirstOrDefault(t => t.TrackId == trackId);
            if (track != null)
            {
                _tracks.Remove(track);
                
                // 更新段厂的轨道数量
                var factory = _segmentFactories.FirstOrDefault(f => f.FactoryId == track.FactoryId);
                if (factory != null)
                {
                    factory.TotalTracks = _tracks.Count(t => t.FactoryId == track.FactoryId);
                    factory.LastUpdatedDate = System.DateTime.Now;
                }
                
                return true;
            }
            return false;
        }
    }
}