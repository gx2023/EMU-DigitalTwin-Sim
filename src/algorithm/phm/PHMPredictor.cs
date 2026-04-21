using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMU.DT.Algorithm.PHM
{
    public class PHMPredictor
    {
        private Dictionary<int, DeviceHealthModel> _deviceModels;
        private readonly object _lockObject = new object();

        public PHMPredictor()
        {
            _deviceModels = new Dictionary<int, DeviceHealthModel>();
        }

        public HealthPrediction PredictHealth(int deviceId, List<SensorData> sensorData)
        {
            DeviceHealthModel model;
            
            lock (_lockObject)
            {
                if (!_deviceModels.TryGetValue(deviceId, out model))
                {
                    model = new DeviceHealthModel(deviceId);
                    _deviceModels[deviceId] = model;
                }
            }

            model.UpdateModel(sensorData);

            var healthScore = CalculateHealthScore(sensorData);
            var prediction = new HealthPrediction
            {
                DeviceId = deviceId,
                CurrentHealthScore = healthScore,
                RemainingUsefulLife = EstimateRUL(model, healthScore),
                HealthTrend = DetermineTrend(model.History),
                PotentialFaults = IdentifyPotentialFaults(sensorData),
                PredictionTimestamp = DateTime.Now
            };

            return prediction;
        }

        public List<Anomaly> DetectAnomalies(List<SensorData> sensorData)
        {
            var anomalies = new List<Anomaly>();

            foreach (var data in sensorData)
            {
                if (IsAnomaly(data))
                {
                    anomalies.Add(new Anomaly
                    {
                        SensorId = data.SensorId,
                        Timestamp = data.Timestamp,
                        Value = data.Value,
                        AnomalyType = ClassifyAnomaly(data),
                        Severity = CalculateSeverity(data),
                        Message = GenerateAnomalyMessage(data)
                    });
                }
            }

            return anomalies;
        }

        public FaultDiagnosis DiagnoseFault(int deviceId, List<SensorData> sensorData)
        {
            var diagnosis = new FaultDiagnosis
            {
                DeviceId = deviceId,
                DiagnosisTimestamp = DateTime.Now,
                ProbableFaults = new List<ProbableFault>()
            };

            // 基于传感器数据分析故障类型
            if (CheckVibrationFault(sensorData))
            {
                diagnosis.ProbableFaults.Add(new ProbableFault
                {
                    FaultType = "振动异常",
                    Probability = CalculateVibrationFaultProbability(sensorData),
                    Severity = "高",
                    RecommendedAction = "检查轴承和齿轮"
                });
            }

            if (CheckTemperatureFault(sensorData))
            {
                diagnosis.ProbableFaults.Add(new ProbableFault
                {
                    FaultType = "温度过高",
                    Probability = CalculateTemperatureFaultProbability(sensorData),
                    Severity = "中",
                    RecommendedAction = "检查冷却系统"
                });
            }

            if (CheckElectricalFault(sensorData))
            {
                diagnosis.ProbableFaults.Add(new ProbableFault
                {
                    FaultType = "电气异常",
                    Probability = CalculateElectricalFaultProbability(sensorData),
                    Severity = "中",
                    RecommendedAction = "检查电路和电机"
                });
            }

            return diagnosis;
        }

        public async Task<HealthPrediction> PredictHealthAsync(int deviceId, List<SensorData> sensorData)
        {
            return await Task.Run(() => PredictHealth(deviceId, sensorData));
        }

        public async Task<List<Anomaly>> DetectAnomaliesAsync(List<SensorData> sensorData)
        {
            return await Task.Run(() => DetectAnomalies(sensorData));
        }

        public async Task<FaultDiagnosis> DiagnoseFaultAsync(int deviceId, List<SensorData> sensorData)
        {
            return await Task.Run(() => DiagnoseFault(deviceId, sensorData));
        }

        private double CalculateHealthScore(List<SensorData> sensorData)
        {
            // 基于多传感器数据计算健康评分（0-100）
            double score = 100;

            foreach (var data in sensorData)
            {
                double deviation = CalculateDeviationFromNormal(data);
                score -= deviation * 5; // 每个偏差扣相应分数
            }

            return Math.Max(0, Math.Min(100, score));
        }

        private int EstimateRUL(DeviceHealthModel model, double healthScore)
        {
            // 估算剩余使用寿命（小时）
            double degradationRate = model.CalculateDegradationRate();

            if (degradationRate <= 0) return 9999; // 健康状态良好

            int remainingHours = (int)((healthScore - 30) / degradationRate);
            return Math.Max(0, remainingHours);
        }

        private string DetermineTrend(List<HealthRecord> history)
        {
            if (history.Count < 5) return "稳定";

            var recentScores = history.TakeLast(5).Select(h => h.HealthScore).ToList();
            double trend = (recentScores.Last() - recentScores.First()) / recentScores.Count;

            if (trend > 1) return "改善";
            if (trend < -1) return "恶化";
            return "稳定";
        }

        private List<string> IdentifyPotentialFaults(List<SensorData> sensorData)
        {
            var faults = new List<string>();

            if (CheckVibrationFault(sensorData)) faults.Add("轴承磨损");
            if (CheckTemperatureFault(sensorData)) faults.Add("过热风险");
            if (CheckElectricalFault(sensorData)) faults.Add("电气异常");

            return faults;
        }

        private bool IsAnomaly(SensorData data)
        {
            // 简单的异常检测逻辑
            return data.Value > data.UpperThreshold || data.Value < data.LowerThreshold;
        }

        private string ClassifyAnomaly(SensorData data)
        {
            if (data.Value > data.UpperThreshold) return "超出上限";
            if (data.Value < data.LowerThreshold) return "低于下限";
            return "未知异常";
        }

        private string CalculateSeverity(SensorData data)
        {
            double deviation = Math.Abs(data.Value - (data.UpperThreshold + data.LowerThreshold) / 2);
            double range = data.UpperThreshold - data.LowerThreshold;

            if (deviation > range * 0.5) return "严重";
            if (deviation > range * 0.25) return "警告";
            return "提示";
        }

        private string GenerateAnomalyMessage(SensorData data)
        {
            return $"传感器 {data.SensorId} 数值异常: {data.Value:F2} (范围: {data.LowerThreshold:F2}-{data.UpperThreshold:F2})";
        }

        private double CalculateDeviationFromNormal(SensorData data)
        {
            double normalRange = data.UpperThreshold - data.LowerThreshold;
            double midPoint = (data.UpperThreshold + data.LowerThreshold) / 2;
            double deviation = Math.Abs(data.Value - midPoint);

            return Math.Min(10, deviation / normalRange * 10);
        }

        private bool CheckVibrationFault(List<SensorData> sensorData)
        {
            var vibrationData = sensorData.Where(d => d.SensorType == "振动").ToList();
            return vibrationData.Any(d => d.Value > 10);
        }

        private bool CheckTemperatureFault(List<SensorData> sensorData)
        {
            var tempData = sensorData.Where(d => d.SensorType == "温度").ToList();
            return tempData.Any(d => d.Value > 80);
        }

        private bool CheckElectricalFault(List<SensorData> sensorData)
        {
            var electricalData = sensorData.Where(d => d.SensorType == "电流" || d.SensorType == "电压").ToList();
            return electricalData.Any(d => d.Value > d.UpperThreshold * 1.2);
        }

        private double CalculateVibrationFaultProbability(List<SensorData> sensorData)
        {
            var vibrationData = sensorData.Where(d => d.SensorType == "振动").FirstOrDefault();
            if (vibrationData == null) return 0;

            return Math.Min(1, vibrationData.Value / 20);
        }

        private double CalculateTemperatureFaultProbability(List<SensorData> sensorData)
        {
            var tempData = sensorData.Where(d => d.SensorType == "温度").FirstOrDefault();
            if (tempData == null) return 0;

            return Math.Min(1, (tempData.Value - 60) / 40);
        }

        private double CalculateElectricalFaultProbability(List<SensorData> sensorData)
        {
            var electricalData = sensorData.Where(d => d.SensorType == "电流" || d.SensorType == "电压").FirstOrDefault();
            if (electricalData == null) return 0;

            double deviation = Math.Abs(electricalData.Value - electricalData.UpperThreshold);
            return Math.Min(1, deviation / 10);
        }
    }

    public class DeviceHealthModel
    {
        public int DeviceId { get; set; }
        public List<HealthRecord> History { get; set; }
        public DateTime LastUpdate { get; set; }

        public DeviceHealthModel(int deviceId)
        {
            DeviceId = deviceId;
            History = new List<HealthRecord>();
            LastUpdate = DateTime.Now;
        }

        public void UpdateModel(List<SensorData> sensorData)
        {
            // 更新模型（简化处理）
            LastUpdate = DateTime.Now;
            History.Add(new HealthRecord
            {
                Timestamp = DateTime.Now,
                HealthScore = CalculateCurrentHealthScore(sensorData)
            });

            // 只保留最近100条记录
            if (History.Count > 100)
            {
                History = History.TakeLast(100).ToList();
            }
        }

        public double CalculateDegradationRate()
        {
            if (History.Count < 10) return 0;

            var recent = History.TakeLast(10).ToList();
            double firstScore = recent.First().HealthScore;
            double lastScore = recent.Last().HealthScore;
            double hours = (recent.Last().Timestamp - recent.First().Timestamp).TotalHours;

            return hours > 0 ? (firstScore - lastScore) / hours : 0;
        }

        private double CalculateCurrentHealthScore(List<SensorData> sensorData)
        {
            double score = 100;
            foreach (var data in sensorData)
            {
                double normalRange = data.UpperThreshold - data.LowerThreshold;
                double midPoint = (data.UpperThreshold + data.LowerThreshold) / 2;
                double deviation = Math.Abs(data.Value - midPoint);
                score -= Math.Min(20, deviation / normalRange * 20);
            }
            return Math.Max(0, score);
        }
    }

    public class HealthRecord
    {
        public DateTime Timestamp { get; set; }
        public double HealthScore { get; set; }
    }

    public class HealthPrediction
    {
        public int DeviceId { get; set; }
        public double CurrentHealthScore { get; set; }
        public int RemainingUsefulLife { get; set; }
        public string HealthTrend { get; set; }
        public List<string> PotentialFaults { get; set; }
        public DateTime PredictionTimestamp { get; set; }
    }

    public class SensorData
    {
        public int SensorId { get; set; }
        public string SensorType { get; set; }
        public double Value { get; set; }
        public double UpperThreshold { get; set; }
        public double LowerThreshold { get; set; }
        public DateTime Timestamp { get; set; }
    }

    public class Anomaly
    {
        public int SensorId { get; set; }
        public DateTime Timestamp { get; set; }
        public double Value { get; set; }
        public string AnomalyType { get; set; }
        public string Severity { get; set; }
        public string Message { get; set; }
    }

    public class FaultDiagnosis
    {
        public int DeviceId { get; set; }
        public DateTime DiagnosisTimestamp { get; set; }
        public List<ProbableFault> ProbableFaults { get; set; }
    }

    public class ProbableFault
    {
        public string FaultType { get; set; }
        public double Probability { get; set; }
        public string Severity { get; set; }
        public string RecommendedAction { get; set; }
    }
}
