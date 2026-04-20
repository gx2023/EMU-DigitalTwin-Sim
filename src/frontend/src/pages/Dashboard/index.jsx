import { Card, Row, Col, Typography, Spin } from 'antd';
import { useEffect, useRef, useState } from 'react';
import * as echarts from 'echarts';
import './index.css';

const { Title, Text } = Typography;

function Dashboard() {
  const [loading, setLoading] = useState(true);
  const maintenanceChartRef = useRef(null);
  const equipmentChartRef = useRef(null);
  const qualityChartRef = useRef(null);

  useEffect(() => {
    // 模拟数据加载
    setTimeout(() => {
      setLoading(false);
      initCharts();
    }, 1000);

    return () => {
      // 清理图表实例
      if (maintenanceChartRef.current) {
        echarts.dispose(maintenanceChartRef.current);
      }
      if (equipmentChartRef.current) {
        echarts.dispose(equipmentChartRef.current);
      }
      if (qualityChartRef.current) {
        echarts.dispose(qualityChartRef.current);
      }
    };
  }, []);

  const initCharts = () => {
    // 检修完成量趋势图
    const maintenanceChart = echarts.init(maintenanceChartRef.current);
    maintenanceChart.setOption({
      title: {
        text: '检修完成量趋势',
        left: 'center',
      },
      tooltip: {
        trigger: 'axis',
      },
      xAxis: {
        type: 'category',
        data: ['1月', '2月', '3月', '4月', '5月', '6月'],
      },
      yAxis: {
        type: 'value',
        name: '完成组数',
      },
      series: [
        {
          name: '一级修',
          type: 'line',
          data: [120, 132, 101, 134, 90, 230],
          smooth: true,
        },
        {
          name: '二级修',
          type: 'line',
          data: [60, 72, 51, 74, 40, 130],
          smooth: true,
        },
      ],
    });

    // 设备运行状态图
    const equipmentChart = echarts.init(equipmentChartRef.current);
    equipmentChart.setOption({
      title: {
        text: '设备运行状态',
        left: 'center',
      },
      tooltip: {
        trigger: 'item',
      },
      legend: {
        top: 'bottom',
      },
      series: [
        {
          name: '设备状态',
          type: 'pie',
          radius: ['40%', '70%'],
          avoidLabelOverlap: false,
          itemStyle: {
            borderRadius: 10,
            borderColor: '#fff',
            borderWidth: 2,
          },
          label: {
            show: false,
            position: 'center',
          },
          emphasis: {
            label: {
              show: true,
              fontSize: '18',
              fontWeight: 'bold',
            },
          },
          labelLine: {
            show: false,
          },
          data: [
            { value: 85, name: '正常运行', itemStyle: { color: '#52c41a' } },
            { value: 10, name: '维护中', itemStyle: { color: '#faad14' } },
            { value: 5, name: '故障', itemStyle: { color: '#f5222d' } },
          ],
        },
      ],
    });

    // 检修质量统计图
    const qualityChart = echarts.init(qualityChartRef.current);
    qualityChart.setOption({
      title: {
        text: '检修质量统计',
        left: 'center',
      },
      tooltip: {
        trigger: 'axis',
        axisPointer: {
          type: 'shadow',
        },
      },
      xAxis: {
        type: 'category',
        data: ['转向架', '制动系统', '牵引系统', '车门系统', '空调系统'],
      },
      yAxis: {
        type: 'value',
        name: '合格率(%)',
        max: 100,
      },
      series: [
        {
          name: '一次交验合格率',
          type: 'bar',
          data: [98, 95, 97, 96, 94],
          itemStyle: {
            color: '#1890ff',
          },
        },
      ],
    });

    // 响应式调整
    window.addEventListener('resize', () => {
      maintenanceChart.resize();
      equipmentChart.resize();
      qualityChart.resize();
    });
  };

  return (
    <div className="dashboard-container">
      <Title level={3}>段厂运营看板</Title>
      
      <Row gutter={[16, 16]} className="metrics-row">
        <Col xs={24} sm={12} md={6}>
          <Card className="metric-card">
            <div className="metric-content">
              <Text strong className="metric-title">日检修完成</Text>
              <div className="metric-value">24</div>
              <Text className="metric-unit">组</Text>
            </div>
          </Card>
        </Col>
        <Col xs={24} sm={12} md={6}>
          <Card className="metric-card">
            <div className="metric-content">
              <Text strong className="metric-title">台位利用率</Text>
              <div className="metric-value">85</div>
              <Text className="metric-unit">%</Text>
            </div>
          </Card>
        </Col>
        <Col xs={24} sm={12} md={6}>
          <Card className="metric-card">
            <div className="metric-content">
              <Text strong className="metric-title">设备可用率</Text>
              <div className="metric-value">92</div>
              <Text className="metric-unit">%</Text>
            </div>
          </Card>
        </Col>
        <Col xs={24} sm={12} md={6}>
          <Card className="metric-card">
            <div className="metric-content">
              <Text strong className="metric-title">一次交验合格率</Text>
              <div className="metric-value">98</div>
              <Text className="metric-unit">%</Text>
            </div>
          </Card>
        </Col>
      </Row>

      <Row gutter={[16, 16]} className="charts-row">
        <Col xs={24} md={12}>
          <Card className="chart-card">
            {loading ? (
              <div className="loading-container">
                <Spin size="large" />
              </div>
            ) : (
              <div ref={maintenanceChartRef} className="chart-container" />
            )}
          </Card>
        </Col>
        <Col xs={24} md={12}>
          <Card className="chart-card">
            {loading ? (
              <div className="loading-container">
                <Spin size="large" />
              </div>
            ) : (
              <div ref={equipmentChartRef} className="chart-container" />
            )}
          </Card>
        </Col>
        <Col xs={24}>
          <Card className="chart-card">
            {loading ? (
              <div className="loading-container">
                <Spin size="large" />
              </div>
            ) : (
              <div ref={qualityChartRef} className="chart-container" />
            )}
          </Card>
        </Col>
      </Row>
    </div>
  );
}

export default Dashboard;