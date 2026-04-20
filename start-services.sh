#!/bin/bash

# 启动服务脚本
echo "=== 动车组检修数字孪生仿真系统启动脚本 ==="
echo ""

# 检查是否安装了dotnet
if ! command -v dotnet &> /dev/null; then
    echo "错误: 未安装dotnet SDK，请先安装dotnet SDK 6.0或更高版本"
    exit 1
fi

echo "1. 启动 AlgorithmService"
cd src/backend/EMU.DT.AlgorithmService
dotnet run --urls "http://localhost:5001" &
algorithm_pid=$!
sleep 2

echo "2. 启动 DeviceService"
cd ../EMU.DT.DeviceService
dotnet run --urls "http://localhost:5002" &
device_pid=$!
sleep 2

echo "3. 启动 SimulationService"
cd ../EMU.DT.SimulationService
dotnet run --urls "http://localhost:5003" &
simulation_pid=$!
sleep 2

echo "4. 启动 Web 前端服务器"
cd ../../web
python3 -m http.server 8000 &
web_pid=$!
sleep 2

echo ""
echo "=== 服务启动完成 ==="
echo "算法服务: http://localhost:5001"
echo "设备服务: http://localhost:5002"
echo "仿真服务: http://localhost:5003"
echo "Web前端: http://localhost:8000"
echo ""
echo "按 Ctrl+C 停止所有服务"

# 等待信号
trap "kill $algorithm_pid $device_pid $simulation_pid $web_pid" SIGINT
wait