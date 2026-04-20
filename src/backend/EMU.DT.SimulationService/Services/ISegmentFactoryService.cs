using System.Collections.Generic;

namespace EMU.DT.SimulationService
{
    public interface ISegmentFactoryService
    {
        List<SegmentFactory> GetAllSegmentFactories();
        SegmentFactory GetSegmentFactoryById(int factoryId);
        SegmentFactory CreateSegmentFactory(SegmentFactory factory);
        SegmentFactory UpdateSegmentFactory(SegmentFactory factory);
        bool DeleteSegmentFactory(int factoryId);
        List<Bay> GetBaysByFactoryId(int factoryId);
        Bay GetBayById(int bayId);
        Bay CreateBay(Bay bay);
        Bay UpdateBay(Bay bay);
        bool DeleteBay(int bayId);
        List<Track> GetTracksByFactoryId(int factoryId);
        Track GetTrackById(int trackId);
        Track CreateTrack(Track track);
        Track UpdateTrack(Track track);
        bool DeleteTrack(int trackId);
    }

    public class SegmentFactory
    {
        public int FactoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public int TotalBays { get; set; }
        public int TotalTracks { get; set; }
        public string Status { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public System.DateTime LastUpdatedDate { get; set; }
        public List<Bay> Bays { get; set; }
        public List<Track> Tracks { get; set; }
    }

    public class Bay
    {
        public int BayId { get; set; }
        public int FactoryId { get; set; }
        public string BayNumber { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
        public string VehicleId { get; set; }
        public double Length { get; set; }
        public double Width { get; set; }
        public List<string> Equipment { get; set; }
        public System.DateTime LastMaintenanceDate { get; set; }
    }

    public class Track
    {
        public int TrackId { get; set; }
        public int FactoryId { get; set; }
        public string TrackNumber { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
        public double Length { get; set; }
        public int MaxSpeed { get; set; }
        public List<int> ConnectedBays { get; set; }
        public List<Signal> Signals { get; set; }
    }

    public class Signal
    {
        public int SignalId { get; set; }
        public int TrackId { get; set; }
        public string SignalNumber { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
        public double Position { get; set; }
    }
}