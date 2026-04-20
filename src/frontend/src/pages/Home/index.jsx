import { Card, Row, Col, Statistic, Typography, Button } from 'antd';
import { ArrowUpOutlined, ArrowDownOutlined, CheckCircleOutlined, ExclamationCircleOutlined } from '@ant-design/icons';
import { useNavigate } from 'react-router-dom';
import './index.css';

const { Title, Paragraph } = Typography;

function Home() {
  const navigate = useNavigate();

  const statistics = [
    {
      title: '在修车辆',
      value: 12,
      icon: <ExclamationCircleOutlined style={{ color: '#1890ff' }} />,
      color: '#1890ff',
    },
    {
      title: '台位利用率',
      value: '85%',
      icon: <CheckCircleOutlined style={{ color: '#52c41a' }} />,
      color: '#52c41a',
    },
    {
      title: '设备运行率',
      value: '92%',
      icon: <CheckCircleOutlined style={{ color: '#52c41a' }} />,
      color: '#52c41a',
    },
    {
      title: '检修完成率',
      value: '98%',
      icon: <CheckCircleOutlined style={{ color: '#52c41a' }} />,
      color: '#52c41a',
    },
  ];

  return (
    <div className="home-container">
      <div className="hero-section">
        <Title level={2}>动车组检修数字孪生仿真系统</Title>
        <Paragraph>
          基于数字孪生技术的动车组检修仿真平台，实现段厂运营管理、检修作业执行、设备运维保障和人员培训考核的一体化管理
        </Paragraph>
        <Button type="primary" size="large" onClick={() => navigate('/dashboard')}>
          查看运营看板
        </Button>
      </div>
      
      <div className="statistics-section">
        <Title level={4}>实时运营数据</Title>
        <Row gutter={[16, 16]}>
          {statistics.map((stat, index) => (
            <Col key={index} xs={24} sm={12} md={6}>
              <Card>
                <Statistic
                  title={stat.title}
                  value={stat.value}
                  prefix={stat.icon}
                  valueStyle={{ color: stat.color }}
                />
              </Card>
            </Col>
          ))}
        </Row>
      </div>
      
      <div className="features-section">
        <Title level={4}>系统功能</Title>
        <Row gutter={[16, 16]}>
          <Col xs={24} md={8}>
            <Card title="段厂仿真" hoverable>
              <p>段厂3D场景管理、动车组调车仿真、检修台位管理、股道与库线管理</p>
            </Card>
          </Col>
          <Col xs={24} md={8}>
            <Card title="检修作业" hoverable>
              <p>检修工艺流程建模、结构化工艺卡片管理、检修作业过程仿真、工步级作业管控</p>
            </Card>
          </Col>
          <Col xs={24} md={8}>
            <Card title="设备管理" hoverable>
              <p>设备实时状态监控、设备故障预警、设备运行数据可视化、设备维护计划管理</p>
            </Card>
          </Col>
        </Row>
      </div>
    </div>
  );
}

export default Home;