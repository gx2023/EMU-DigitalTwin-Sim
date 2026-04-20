using UnityEngine;
using System.Collections.Generic;
using System;

namespace EMU.DT.Unity.Simulation
{
    public class SimulationManager : MonoBehaviour
    {
        public static SimulationManager Instance { get; private set; }

        public enum SimulationState
        {
            Idle,
            Running,
            Paused,
            Stopped
        }

        public SimulationState CurrentState { get; private set; }
        public float SimulationSpeed { get; set; } = 1.0f;

        private List<ISimulator> simulators;
        private float simulationTime;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
                InitializeSimulators();
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void InitializeSimulators()
        {
            simulators = new List<ISimulator>();
            // 这里可以添加各种仿真器，如车辆仿真器、设备仿真器、人员仿真器等
            // 示例：simulators.Add(new VehicleSimulator());
        }

        public void StartSimulation()
        {
            if (CurrentState == SimulationState.Idle || CurrentState == SimulationState.Stopped)
            {
                CurrentState = SimulationState.Running;
                simulationTime = 0;
                foreach (var simulator in simulators)
                {
                    simulator.StartSimulation();
                }
                Debug.Log("Simulation started");
            }
        }

        public void PauseSimulation()
        {
            if (CurrentState == SimulationState.Running)
            {
                CurrentState = SimulationState.Paused;
                foreach (var simulator in simulators)
                {
                    simulator.PauseSimulation();
                }
                Debug.Log("Simulation paused");
            }
        }

        public void ResumeSimulation()
        {
            if (CurrentState == SimulationState.Paused)
            {
                CurrentState = SimulationState.Running;
                foreach (var simulator in simulators)
                {
                    simulator.ResumeSimulation();
                }
                Debug.Log("Simulation resumed");
            }
        }

        public void StopSimulation()
        {
            if (CurrentState != SimulationState.Idle && CurrentState != SimulationState.Stopped)
            {
                CurrentState = SimulationState.Stopped;
                foreach (var simulator in simulators)
                {
                    simulator.StopSimulation();
                }
                Debug.Log("Simulation stopped");
            }
        }

        public void SetSimulationSpeed(float speed)
        {
            if (speed > 0)
            {
                SimulationSpeed = speed;
                Time.timeScale = speed;
                Debug.Log($"Simulation speed set to {speed}x");
            }
        }

        private void Update()
        {
            if (CurrentState == SimulationState.Running)
            {
                simulationTime += Time.deltaTime * SimulationSpeed;
                foreach (var simulator in simulators)
                {
                    simulator.UpdateSimulation(Time.deltaTime * SimulationSpeed);
                }
            }
        }

        public float GetSimulationTime()
        {
            return simulationTime;
        }

        public void AddSimulator(ISimulator simulator)
        {
            if (!simulators.Contains(simulator))
            {
                simulators.Add(simulator);
                Debug.Log($"Simulator added: {simulator.GetType().Name}");
            }
        }

        public void RemoveSimulator(ISimulator simulator)
        {
            if (simulators.Contains(simulator))
            {
                simulators.Remove(simulator);
                Debug.Log($"Simulator removed: {simulator.GetType().Name}");
            }
        }
    }

    public interface ISimulator
    {
        void StartSimulation();
        void PauseSimulation();
        void ResumeSimulation();
        void StopSimulation();
        void UpdateSimulation(float deltaTime);
    }
}