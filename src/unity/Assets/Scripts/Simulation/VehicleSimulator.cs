using UnityEngine;
using System.Collections.Generic;
using System;

namespace EMU.DT.Unity.Simulation
{
    public class VehicleSimulator : MonoBehaviour, ISimulator
    {
        public List<Vehicle> vehicles;
        private Dictionary<int, Vehicle> vehicleMap;

        private void Awake()
        {
            vehicleMap = new Dictionary<int, Vehicle>();
            foreach (var vehicle in vehicles)
            {
                vehicleMap.Add(vehicle.id, vehicle);
            }
        }

        public void StartSimulation()
        {
            foreach (var vehicle in vehicles)
            {
                vehicle.StartSimulation();
            }
        }

        public void PauseSimulation()
        {
            foreach (var vehicle in vehicles)
            {
                vehicle.PauseSimulation();
            }
        }

        public void ResumeSimulation()
        {
            foreach (var vehicle in vehicles)
            {
                vehicle.ResumeSimulation();
            }
        }

        public void StopSimulation()
        {
            foreach (var vehicle in vehicles)
            {
                vehicle.StopSimulation();
            }
        }

        public void UpdateSimulation(float deltaTime)
        {
            foreach (var vehicle in vehicles)
            {
                vehicle.UpdateSimulation(deltaTime);
            }
        }

        public Vehicle GetVehicle(int id)
        {
            if (vehicleMap.ContainsKey(id))
            {
                return vehicleMap[id];
            }
            return null;
        }

        public void AddVehicle(Vehicle vehicle)
        {
            if (!vehicleMap.ContainsKey(vehicle.id))
            {
                vehicles.Add(vehicle);
                vehicleMap.Add(vehicle.id, vehicle);
            }
        }

        public void RemoveVehicle(int id)
        {
            if (vehicleMap.ContainsKey(id))
            {
                var vehicle = vehicleMap[id];
                vehicles.Remove(vehicle);
                vehicleMap.Remove(id);
            }
        }
    }

    [System.Serializable]
    public class Vehicle
    {
        public int id;
        public string name;
        public string model;
        public string number;
        public VehicleState state;
        public Vector3 position;
        public Quaternion rotation;
        public float speed;
        public float maxSpeed = 15f; // 段厂内最大速度

        private Vector3 targetPosition;
        private bool isMoving;

        public void StartSimulation()
        {
            state = VehicleState.Idle;
            speed = 0;
            isMoving = false;
        }

        public void PauseSimulation()
        {
            // 暂停逻辑
        }

        public void ResumeSimulation()
        {
            // 恢复逻辑
        }

        public void StopSimulation()
        {
            state = VehicleState.Idle;
            speed = 0;
            isMoving = false;
        }

        public void UpdateSimulation(float deltaTime)
        {
            if (isMoving)
            {
                float distance = Vector3.Distance(position, targetPosition);
                if (distance > 0.1f)
                {
                    speed = Mathf.Min(speed + 0.5f * deltaTime, maxSpeed);
                    Vector3 direction = (targetPosition - position).normalized;
                    position += direction * speed * deltaTime;
                    rotation = Quaternion.LookRotation(direction);
                }
                else
                {
                    speed = 0;
                    isMoving = false;
                    state = VehicleState.Idle;
                }
            }
        }

        public void MoveTo(Vector3 target)
        {
            targetPosition = target;
            isMoving = true;
            state = VehicleState.Moving;
        }

        public void SetState(VehicleState newState)
        {
            state = newState;
        }
    }

    public enum VehicleState
    {
        Idle,
        Moving,
        Maintenance,
        Waiting,
        Fault
    }
}