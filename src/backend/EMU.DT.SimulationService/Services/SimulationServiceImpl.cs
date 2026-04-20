using System.Collections.Generic;
using System.Linq;

namespace EMU.DT.SimulationService
{
    public class SimulationServiceImpl : ISimulationService
    {
        private List<Simulation> _simulations;
        private List<SimulationEvent> _simulationEvents;

        public SimulationServiceImpl()
        {
            _simulations = new List<Simulation>
            {
                new Simulation
                {
                    SimulationId = 1,
                    Name = "段厂日常运营仿真",
                    Description = "模拟段厂日常运营场景，包括动车组入库、检修、出库等流程",
                    Type = "段厂运营",
                    Status = "已创建",
                    CreatedDate = System.DateTime.Now.AddDays(-1),
                    CreatedBy = "系统管理员",
                    Parameters = new List<SimulationParameter>
                    {
                        new SimulationParameter { Name = "simulationSpeed", Value = "1", Description = "仿真速度倍率" },
                        new SimulationParameter { Name = "vehicleCount", Value = "5", Description = "车辆数量" },
                        new SimulationParameter { Name = "bayCount", Value = "8", Description = "台位数量" }
                    },
                    VehicleIds = new List<int> { 1, 2, 3, 4, 5 },
                    BayIds = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8 }
                },
                new Simulation
                {
                    SimulationId = 2,
                    Name = "检修流程优化仿真",
                    Description = "模拟检修流程优化场景，测试不同工艺流程的效率",
                    Type = "流程优化",
                    Status = "运行中",
                    CreatedDate = System.DateTime.Now.AddHours(-2),
                    StartedDate = System.DateTime.Now.AddHours(-1),
                    CreatedBy = "系统管理员",
                    Parameters = new List<SimulationParameter>
                    {
                        new SimulationParameter { Name = "simulationSpeed", Value = "2", Description = "仿真速度倍率" },
                        new SimulationParameter { Name = "vehicleCount", Value = "3", Description = "车辆数量" },
                        new SimulationParameter { Name = "bayCount", Value = "6", Description = "台位数量" }
                    },
                    VehicleIds = new List<int> { 6, 7, 8 },
                    BayIds = new List<int> { 1, 2, 3, 4, 5, 6 }
                }
            };

            _simulationEvents = new List<SimulationEvent>
            {
                new SimulationEvent
                {
                    EventId = 1,
                    SimulationId = 2,
                    EventType = "系统",
                    EventMessage = "仿真开始",
                    Timestamp = System.DateTime.Now.AddHours(-1),
                    Source = "系统",
                    Details = "检修流程优化仿真开始运行"
                },
                new SimulationEvent
                {
                    EventId = 2,
                    SimulationId = 2,
                    EventType = "车辆",
                    EventMessage = "CR400AF-2048 入库",
                    Timestamp = System.DateTime.Now.AddHours(-1).AddMinutes(5),
                    Source = "CR400AF-2048",
                    Details = "车辆进入检修库"
                },
                new SimulationEvent
                {
                    EventId = 3,
                    SimulationId = 2,
                    EventType = "台位",
                    EventMessage = "台位1 占用",
                    Timestamp = System.DateTime.Now.AddHours(-1).AddMinutes(6),
                    Source = "台位1",
                    Details = "CR400AF-2048 占用台位1"
                }
            };
        }

        public Simulation CreateSimulation(Simulation simulation)
        {
            simulation.SimulationId = _simulations.Max(s => s.SimulationId) + 1;
            simulation.CreatedDate = System.DateTime.Now;
            simulation.Status = "已创建";
            _simulations.Add(simulation);
            return simulation;
        }

        public Simulation GetSimulationById(int simulationId)
        {
            return _simulations.FirstOrDefault(s => s.SimulationId == simulationId);
        }

        public List<Simulation> GetAllSimulations()
        {
            return _simulations;
        }

        public Simulation UpdateSimulation(Simulation simulation)
        {
            var existingSimulation = _simulations.FirstOrDefault(s => s.SimulationId == simulation.SimulationId);
            if (existingSimulation != null)
            {
                existingSimulation.Name = simulation.Name;
                existingSimulation.Description = simulation.Description;
                existingSimulation.Type = simulation.Type;
                existingSimulation.Status = simulation.Status;
                existingSimulation.Parameters = simulation.Parameters;
                existingSimulation.VehicleIds = simulation.VehicleIds;
                existingSimulation.BayIds = simulation.BayIds;
            }
            return existingSimulation;
        }

        public bool DeleteSimulation(int simulationId)
        {
            var simulation = _simulations.FirstOrDefault(s => s.SimulationId == simulationId);
            if (simulation != null)
            {
                _simulations.Remove(simulation);
                _simulationEvents.RemoveAll(e => e.SimulationId == simulationId);
                return true;
            }
            return false;
        }

        public SimulationStartResponse StartSimulation(int simulationId)
        {
            var simulation = _simulations.FirstOrDefault(s => s.SimulationId == simulationId);
            if (simulation != null)
            {
                simulation.Status = "运行中";
                simulation.StartedDate = System.DateTime.Now;
                
                _simulationEvents.Add(new SimulationEvent
                {
                    EventId = _simulationEvents.Max(e => e.EventId) + 1,
                    SimulationId = simulationId,
                    EventType = "系统",
                    EventMessage = "仿真开始",
                    Timestamp = System.DateTime.Now,
                    Source = "系统",
                    Details = $"{simulation.Name} 开始运行"
                });

                return new SimulationStartResponse
                {
                    Success = true,
                    Message = "仿真已开始",
                    SimulationId = simulationId,
                    StartTime = System.DateTime.Now
                };
            }
            return new SimulationStartResponse
            {
                Success = false,
                Message = "仿真不存在",
                SimulationId = simulationId,
                StartTime = System.DateTime.Now
            };
        }

        public SimulationStopResponse StopSimulation(int simulationId)
        {
            var simulation = _simulations.FirstOrDefault(s => s.SimulationId == simulationId);
            if (simulation != null && simulation.StartedDate.HasValue)
            {
                simulation.Status = "已结束";
                simulation.EndedDate = System.DateTime.Now;
                simulation.DurationSeconds = (int)(simulation.EndedDate.Value - simulation.StartedDate.Value).TotalSeconds;

                _simulationEvents.Add(new SimulationEvent
                {
                    EventId = _simulationEvents.Max(e => e.EventId) + 1,
                    SimulationId = simulationId,
                    EventType = "系统",
                    EventMessage = "仿真结束",
                    Timestamp = System.DateTime.Now,
                    Source = "系统",
                    Details = $"{simulation.Name} 运行结束"
                });

                return new SimulationStopResponse
                {
                    Success = true,
                    Message = "仿真已结束",
                    SimulationId = simulationId,
                    StopTime = System.DateTime.Now,
                    DurationSeconds = simulation.DurationSeconds
                };
            }
            return new SimulationStopResponse
            {
                Success = false,
                Message = "仿真不存在或未开始",
                SimulationId = simulationId,
                StopTime = System.DateTime.Now,
                DurationSeconds = 0
            };
        }

        public SimulationPauseResponse PauseSimulation(int simulationId)
        {
            var simulation = _simulations.FirstOrDefault(s => s.SimulationId == simulationId);
            if (simulation != null && simulation.Status == "运行中")
            {
                simulation.Status = "已暂停";

                _simulationEvents.Add(new SimulationEvent
                {
                    EventId = _simulationEvents.Max(e => e.EventId) + 1,
                    SimulationId = simulationId,
                    EventType = "系统",
                    EventMessage = "仿真暂停",
                    Timestamp = System.DateTime.Now,
                    Source = "系统",
                    Details = $"{simulation.Name} 已暂停"
                });

                return new SimulationPauseResponse
                {
                    Success = true,
                    Message = "仿真已暂停",
                    SimulationId = simulationId,
                    PauseTime = System.DateTime.Now
                };
            }
            return new SimulationPauseResponse
            {
                Success = false,
                Message = "仿真不存在或未运行",
                SimulationId = simulationId,
                PauseTime = System.DateTime.Now
            };
        }

        public SimulationResumeResponse ResumeSimulation(int simulationId)
        {
            var simulation = _simulations.FirstOrDefault(s => s.SimulationId == simulationId);
            if (simulation != null && simulation.Status == "已暂停")
            {
                simulation.Status = "运行中";

                _simulationEvents.Add(new SimulationEvent
                {
                    EventId = _simulationEvents.Max(e => e.EventId) + 1,
                    SimulationId = simulationId,
                    EventType = "系统",
                    EventMessage = "仿真恢复",
                    Timestamp = System.DateTime.Now,
                    Source = "系统",
                    Details = $"{simulation.Name} 已恢复运行"
                });

                return new SimulationResumeResponse
                {
                    Success = true,
                    Message = "仿真已恢复",
                    SimulationId = simulationId,
                    ResumeTime = System.DateTime.Now
                };
            }
            return new SimulationResumeResponse
            {
                Success = false,
                Message = "仿真不存在或未暂停",
                SimulationId = simulationId,
                ResumeTime = System.DateTime.Now
            };
        }

        public SimulationState GetSimulationState(int simulationId)
        {
            var simulation = _simulations.FirstOrDefault(s => s.SimulationId == simulationId);
            if (simulation != null)
            {
                int elapsedSeconds = 0;
                int totalSeconds = simulation.DurationSeconds > 0 ? simulation.DurationSeconds : 3600; // 默认1小时
                
                if (simulation.StartedDate.HasValue)
                {
                    var endTime = simulation.EndedDate ?? System.DateTime.Now;
                    elapsedSeconds = (int)(endTime - simulation.StartedDate.Value).TotalSeconds;
                }

                double progressPercentage = (double)elapsedSeconds / totalSeconds * 100;
                if (progressPercentage > 100) progressPercentage = 100;

                return new SimulationState
                {
                    SimulationId = simulationId,
                    Status = simulation.Status,
                    StartTime = simulation.StartedDate,
                    LastUpdateTime = System.DateTime.Now,
                    ElapsedSeconds = elapsedSeconds,
                    TotalSeconds = totalSeconds,
                    ProgressPercentage = progressPercentage,
                    VehicleStates = GetVehicleStates(simulationId)
                };
            }
            return null;
        }

        public List<SimulationEvent> GetSimulationEvents(int simulationId)
        {
            return _simulationEvents.Where(e => e.SimulationId == simulationId).OrderBy(e => e.Timestamp).ToList();
        }

        public SimulationStats GetSimulationStats(int simulationId)
        {
            var simulation = _simulations.FirstOrDefault(s => s.SimulationId == simulationId);
            if (simulation != null)
            {
                return new SimulationStats
                {
                    SimulationId = simulationId,
                    TotalVehicles = simulation.VehicleIds.Count,
                    ActiveVehicles = simulation.VehicleIds.Count, // 简化处理
                    CompletedTasks = 10, // 模拟数据
                    TotalTasks = 20, // 模拟数据
                    AverageTaskDuration = 15.5, // 模拟数据
                    BayUtilization = 75.5, // 模拟数据
                    EnergyConsumption = 120.5, // 模拟数据
                    VehicleStats = GetVehicleStats(simulationId)
                };
            }
            return null;
        }

        private List<VehicleState> GetVehicleStates(int simulationId)
        {
            return new List<VehicleState>
            {
                new VehicleState
                {
                    VehicleId = 1,
                    VehicleNumber = "CR400AF-2048",
                    Status = "检修中",
                    X = 100.0,
                    Y = 50.0,
                    Z = 0.0,
                    Speed = 0.0,
                    BayId = "1",
                    CurrentTask = "一级修"
                },
                new VehicleState
                {
                    VehicleId = 2,
                    VehicleNumber = "CRH380B-1024",
                    Status = "入库中",
                    X = 80.0,
                    Y = 30.0,
                    Z = 0.0,
                    Speed = 5.0,
                    BayId = "",
                    CurrentTask = "入库"
                },
                new VehicleState
                {
                    VehicleId = 3,
                    VehicleNumber = "CR400BF-3072",
                    Status = "出库中",
                    X = 120.0,
                    Y = 70.0,
                    Z = 0.0,
                    Speed = 10.0,
                    BayId = "",
                    CurrentTask = "出库"
                }
            };
        }

        private List<VehicleStats> GetVehicleStats(int simulationId)
        {
            return new List<VehicleStats>
            {
                new VehicleStats
                {
                    VehicleId = 1,
                    VehicleNumber = "CR400AF-2048",
                    TasksCompleted = 3,
                    TotalTaskTime = 45.5,
                    AverageSpeed = 8.5,
                    Status = "检修中"
                },
                new VehicleStats
                {
                    VehicleId = 2,
                    VehicleNumber = "CRH380B-1024",
                    TasksCompleted = 2,
                    TotalTaskTime = 30.0,
                    AverageSpeed = 6.2,
                    Status = "入库中"
                },
                new VehicleStats
                {
                    VehicleId = 3,
                    VehicleNumber = "CR400BF-3072",
                    TasksCompleted = 4,
                    TotalTaskTime = 60.0,
                    AverageSpeed = 9.8,
                    Status = "出库中"
                }
            };
        }
    }
}