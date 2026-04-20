# EMU-DigitalTwin-Sim

## 动车组检修数字孪生仿真系统

基于Unity的动车组一二级修段厂仿真、检修车间检修作业数字孪生仿真、设备运行数字孪生的综合仿真平台。

---

### 📌 项目定位

| 优先级 | 目标 | 说明 |
|--------|------|------|
| **首要** | 设计辅助 | 段厂布局设计验证、检修工艺方案预演、设备配置方案仿真验证 |
| **核心** | 生产调度辅助 | 段厂运用调度优化、检修作业排程优化、资源配置优化 |
| **重要** | 运维辅助 | 设备运行监控、故障预警、预测性维护 |
| **远期** | VR/AR培训 | 检修人员虚拟培训（第三阶段可选功能） |

---

### 🏗️ 技术栈

| 层级 | 技术选型 |
|------|---------|
| 3D引擎 | Unity 2022 LTS / Unity 6, URP渲染管线 |
| 后端框架 | .NET 8 / ASP.NET Core 微服务架构 |
| 数据库 | PostgreSQL, TimescaleDB, Redis |
| 消息队列 | RabbitMQ / Apache Kafka |
| IoT协议 | MQTT (EMQX), OPC UA |
| AI框架 | PyTorch, ONNX Runtime, TensorRT |
| 算法引擎 | OR-Tools, DEAP, Stable-Baselines3 |
| 容器化 | Docker, Kubernetes |
| CI/CD | GitLab CI / GitHub Actions |

---

### 📁 项目结构

```
EMU-DigitalTwin-Sim/
├── docs/                    # 项目文档
│   ├── requirements/        # 需求文档（PRD、岗位开发文档）
│   ├── architecture/        # 架构设计文档
│   ├── design/              # UI/UX设计文档
│   ├── api/                 # API接口文档
│   ├── meeting-notes/       # 会议纪要
│   └── reports/             # 项目报告
├── src/
│   ├── unity/               # Unity前端项目
│   │   └── Assets/
│   │       ├── Scripts/     # C#脚本
│   │       │   ├── Core/        # 核心框架
│   │       │   ├── UI/          # UI界面
│   │       │   ├── Models/      # 数据模型
│   │       │   ├── Simulation/  # 仿真模块
│   │       │   ├── Data/        # 数据管理
│   │       │   └── Utils/       # 工具类
│   │       ├── Scenes/      # 场景文件
│   │       ├── Prefabs/     # 预制体
│   │       ├── Materials/   # 材质
│   │       ├── Textures/    # 贴图
│   │       ├── Models/      # 3D模型
│   │       ├── Animations/  # 动画
│   │       └── AddressableAssets/ # 资源寻址
│   ├── backend/             # .NET后端微服务
│   │   ├── EMU.DT.ApiGateway/       # API网关
│   │   ├── EMU.DT.AuthService/      # 认证授权服务
│   │   ├── EMU.DT.SimulationService/# 仿真服务
│   │   ├── EMU.DT.DataService/      # 数据服务
│   │   ├── EMU.DT.DeviceService/    # 设备服务
│   │   ├── EMU.DT.AlgorithmService/ # 算法服务
│   │   ├── EMU.DT.NotificationService/# 通知服务
│   │   ├── EMU.DT.FileService/      # 文件服务
│   │   ├── EMU.DT.Shared/           # 共享库
│   │   └── tests/                    # 单元测试
│   └── algorithm/           # Python算法模块
│       ├── scheduling/      # 调度优化算法
│       ├── path_planning/   # 路径规划算法
│       ├── phm/             # 故障预测与健康管理
│       ├── vision/          # AI视觉检测
│       ├── simulation/      # 仿真引擎
│       ├── sync/            # 数字孪生同步
│       ├── data_generation/ # 仿真数据生成
│       ├── models/          # 训练模型
│       ├── datasets/        # 数据集
│       ├── notebooks/       # Jupyter笔记本
│       ├── configs/         # 配置文件
│       └── tests/           # 算法测试
├── infra/                   # 基础设施配置
│   ├── docker/              # Docker配置
│   ├── k8s/                 # Kubernetes配置
│   ├── scripts/             # 部署脚本
│   └── nginx/               # Nginx配置
├── tools/                   # 开发工具
│   ├── model-converter/     # 模型转换工具
│   ├── data-generator/      # 数据生成工具
│   └── performance-test/    # 性能测试工具
├── configs/                 # 项目配置文件
├── .github/workflows/       # CI/CD流水线
└── .vscode/                 # VS Code配置
```

---

### 📋 开发阶段

| 阶段 | 周期 | 核心目标 |
|------|------|---------|
| **第一阶段 MVP** | 6个月 | 设计辅助+基础仿真（段厂3D场景、核心部件检修、基础调车、A*路径规划） |
| **第二阶段 增强版** | 6个月 | 生产调度+运维辅助（全部件检修、排程优化、PHM、AI视觉、信号控制、AGV、人员仿真） |
| **第三阶段 完善版** | 6个月 | 高级功能+扩展（VR/AR培训、立体库、多段厂部署、开放API） |

---

### 📄 文档版本

| 文档 | 版本 | 路径 |
|------|------|------|
| 开发PRD文档 | V1.2 | `docs/requirements/PRD_开发需求文档_V1.2.md` |
| 系统架构文档 | V1.2 | `docs/architecture/系统架构设计文档_V1.2.md` |
| Unity前端工程师文档 | V1.2 | `docs/requirements/岗位开发文档-Unity前端工程师_V1.2.md` |
| 后端开发工程师文档 | V1.2 | `docs/requirements/岗位开发文档-后端开发工程师_V1.2.md` |
| 算法工程师文档 | V1.2 | `docs/requirements/岗位开发文档-算法工程师_V1.2.md` |
| UI/UX设计师文档 | V1.2 | `docs/design/岗位开发文档-UI-UX设计师_V1.2.md` |
| 市场分析报告 | V1.1 | `docs/reports/市场分析报告_V1.1.pptx` |
| DB Metaverse分析 | V1.0 | `docs/reports/DB_Metaverse综合分析报告_V1.0.pptx` |
