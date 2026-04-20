using System.Collections.Generic;

namespace EMU.DT.SimulationService
{
    public interface ISimulationService
    {
        Simulation CreateSimulation(Simulation simulation);
        Simulation GetSimulationById(int simulationId);
        List<Simulation> GetAllSimulations();
        Simulation UpdateSimulation(Simulation simulation);
        bool DeleteSimulation(int simulationId);
        SimulationStartResponse StartSimulation(int simulationId);
        SimulationStopResponse StopSimulation(int simulationId);
        SimulationPauseResponse PauseSimulation(int simulationId);
        SimulationResumeResponse ResumeSimulation(int simulationId);
        SimulationState GetSimulationState(int simulationId);
        List<SimulationEvent> GetSimulationEvents(int simulationId);
        SimulationStats GetSimulationStats(int simulationId);
    }

    public class Simulation
    {
        public int SimulationId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public System.DateTime? StartedDate { get; set; }
        public System.DateTime? EndedDate { get; set; }
        public int DurationSeconds { get; set; }
        public string CreatedBy { get; set; }
        public List<SimulationParameter> Parameters { get; set; }
        public List<int> VehicleIds { get; set; }
        public List<int> BayIds { get; set; }
    }

    public class SimulationParameter
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public string Description { get; set; }
    }

    public class SimulationStartResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public int SimulationId { get; set; }
        public System.DateTime StartTime { get; set; }
    }

    public class SimulationStopResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public int SimulationId { get; set; }
        public System.DateTime StopTime { get; set; }
        public int DurationSeconds { get; set; }
    }

    public class SimulationPauseResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public int SimulationId { get; set; }
        public System.DateTime PauseTime { get; set; }
    }

    public class SimulationResumeResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public int SimulationId { get; set; }
        public System.DateTime ResumeTime { get; set; }
    }

    public class SimulationState
    {
        public int SimulationId { get; set; }
        public string Status { get; set; }
        public System.DateTime? StartTime { get; set; }
        public System.DateTime? LastUpdateTime { get; set; }
        public int ElapsedSeconds { get; set; }
        public int TotalSeconds { get; set; }
        public double ProgressPercentage { get; set; }
        public List<VehicleState> VehicleStates { get; set; }
    }

    public class VehicleState
    {
        public int VehicleId { get; set; }
        public string VehicleNumber { get; set; }
        public string Status { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
        public double Speed { get; set; }
        public string BayId { get; set; }
        public string CurrentTask { get; set; }
    }

    public class SimulationEvent
    {
        public int EventId { get; set; }
        public int SimulationId { get; set; }
        public string EventType { get; set; }
        public string EventMessage { get; set; }
        public System.DateTime Timestamp { get; set; }
        public string Source { get; set; }
        public string Details { get; set; }
    }

    public class SimulationStats
    {
        public int SimulationId { get; set; }
        public int TotalVehicles { get; set; }
        public int ActiveVehicles { get; set; }
        public int CompletedTasks { get; set; }
        public int TotalTasks { get; set; }
        public double AverageTaskDuration { get; set; }
        public double BayUtilization { get; set; }
        public double EnergyConsumption { get; set; }
        public List<VehicleStats> VehicleStats { get; set; }
    }

    public class VehicleStats
    {
        public int VehicleId { get; set; }
        public string VehicleNumber { get; set; }
        public int TasksCompleted { get; set; }
        public double TotalTaskTime { get; set; }
        public double AverageSpeed { get; set; }
        public string Status { get; set; }
    }
}