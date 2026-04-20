import { Card, Row, Col, Typography, Table, Space, Tag, Button, Alert, Badge } from 'antd';
import { SettingOutlined, AlertOutlined, CheckCircleOutlined, ClockCircleOutlined } from '@ant-design/icons';
import { useState } from 'react';
import './index.css';

const { Title, Text } = Typography;

function Equipment() {
  // 模拟设备数据
  const equipmentData = [
    {
      key: '1',
      name: '不落轮镟床',
      type: '机加工设备',
      status: '运行中',
      location: '检修车间A区',
      lastMaintenance: '2024-01-10',
      nextMaintenance: '2024-02-10',
      health: 95,
    },
    {
      key: '2',
      name: '车轮踏面诊断装置',
      type: '检测设备',
      status: '运行中',
      location: '检修车间B区',
      lastMaintenance: '2024-01-05',
      nextMaintenance: '2024-02-05',
      health: 92,
    },
    {
      key: '3',
      name: '地沟式检查设备',
      type: '检查设备',
      status: '维护中',
      location: '检修车间C区',
      lastMaintenance: '2024-01-12',
      nextMaintenance: '2024-02-12',
      health: 85,
    },
    {
      key: '4',
      name: '架车机',
      type: '起重设备',
      status: '故障',
      location: '检修车间A区',
      lastMaintenance: '2024-01-01',
      nextMaintenance: '2024-02-01',
      health: 60,
    },
    {
      key: '5',
      name: '洗车机',
      type: '清洗设备',
      status: '运行中',
      location: '洗车区',
      lastMaintenance: '2024-01-08',
      nextMaintenance: '2024-02-08',
      health: 90,
    },
  ];

  // 模拟预警信息
  const alerts = [
    {
      key: '1',
      device: '架车机',
      type: '故障',
      message: '液压系统压力不足',
      time: '2024-01-15 10:30',
      status: '未处理',
    },
    {
      key: '2',
      device: '地沟式检查设备',
      type: '预警',
      message: '传感器校准过期',
      time: '2024-01-15 09:15',
      status: '处理中',
    },
    {
      key: '3',
      device: '不落轮镟床',
      type: '预警',
      message: '润滑油液位低',
      time: '2024-01-14 16:45',
      status: '已处理',
    },
  ];

  const equipmentColumns = [
    {
      title: '设备名称',
      dataIndex: 'name',
      key: 'name',
    },
    {
      title: '设备类型',
      dataIndex: 'type',
      key: 'type',
    },
    {
      title: '状态',
      dataIndex: 'status',
      key: 'status',
      render: (status) => {
        let color = '';
        switch (status) {
          case '运行中':
            color = 'green';
            break;
          case '维护中':
            color = 'orange';
            break;
          case '故障':
            color = 'red';
            break;
          default:
            color = 'blue';
        }
        return <Tag color={color}>{status}</Tag>;
      },
    },
    {
      title: '位置',
      dataIndex: 'location',
      key: 'location',
    },
    {
      title: '上次维护',
      dataIndex: 'lastMaintenance',
      key: 'lastMaintenance',
    },
    {
      title: '下次维护',
      dataIndex: 'nextMaintenance',
      key: 'nextMaintenance',
    },
    {
      title: '健康度',
      dataIndex: 'health',
      key: 'health',
      render: (health) => (
        <div className="health-container">
          <div className="health-bar">
            <div 
              className="health-fill" 
              style={{ 
                width: `${health}%`,
                backgroundColor: health > 80 ? '#52c41a' : health > 60 ? '#faad14' : '#f5222d'
              }} 
            />
          </div>
          <Text>{health}%</Text>
        </div>
      ),
    },
    {
      title: '操作',
      key: 'action',
      render: () => (
        <Space size="middle">
          <Button size="small" icon={<SettingOutlined />}>维护</Button>
          <Button size="small" icon={<AlertOutlined />}>预警</Button>
        </Space>
      ),
    },
  ];

  const alertColumns = [
    {
      title: '设备',
      dataIndex: 'device',
      key: 'device',
    },
    {
      title: '类型',
      dataIndex: 'type',
      key: 'type',
      render: (type) => (
        <Tag color={type === '故障' ? 'red' : 'orange'}>{type}</Tag>
      ),
    },
    {
      title: '消息',
      dataIndex: 'message',
      key: 'message',
    },
    {
      title: '时间',
      dataIndex: 'time',
      key: 'time',
    },
    {
      title: '状态',
      dataIndex: 'status',
      key: 'status',
      render: (status) => {
        let color = '';
        switch (status) {
          case '未处理':
            color = 'red';
            break;
          case '处理中':
            color = 'orange';
            break;
          case '已处理':
            color = 'green';
            break;
          default:
            color = 'blue';
        }
        return <Tag color={color}>{status}</Tag>;
      },
    },
  ];

  return (
    <div className="equipment-container">
      <Title level={3}>设备管理</Title>
      
      <Row gutter={[16, 16]}>
        <Col xs={24} md={16}>
          <Card title="设备状态监控">
            <Table columns={equipmentColumns} dataSource={equipmentData} />
          </Card>
        </Col>
        <Col xs={24} md={8}>
          <Card title="设备预警信息">
            <div className="alerts-container">
              {alerts.map((alert) => (
                <Alert
                  key={alert.key}
                  message={
                    <div className="alert-message">
                      <div className="alert-header">
                        <Text strong>{alert.device}</Text>
                        <Tag color={alert.type === '故障' ? 'red' : 'orange'}>{alert.type}</Tag>
                      </div>
                      <div className="alert-content">{alert.message}</div>
                      <div className="alert-footer">
                        <Text type="secondary">{alert.time}</Text>
                        <Tag color={alert.status === '未处理' ? 'red' : alert.status === '处理中' ? 'orange' : 'green'}>{alert.status}</Tag>
                      </div>
                    </div>
                  }
                  type={alert.type === '故障' ? 'error' : 'warning'}
                  showIcon
                  style={{ marginBottom: 12 }}
                />
              ))}
            </div>
          </Card>
          
          <Card title="设备维护计划" style={{ marginTop: 16 }}>
            <div className="maintenance-plan">
              <div className="plan-item">
                <ClockCircleOutlined className="plan-icon" />
                <div className="plan-content">
                  <Text strong>不落轮镟床</Text>
                  <Text type="secondary">2024-02-10</Text>
                </div>
              </div>
              <div className="plan-item">
                <ClockCircleOutlined className="plan-icon" />
                <div className="plan-content">
                  <Text strong>车轮踏面诊断装置</Text>
                  <Text type="secondary">2024-02-05</Text>
                </div>
              </div>
              <div className="plan-item">
                <ClockCircleOutlined className="plan-icon" />
                <div className="plan-content">
                  <Text strong>地沟式检查设备</Text>
                  <Text type="secondary">2024-02-12</Text>
                </div>
              </div>
            </div>
          </Card>
        </Col>
      </Row>
    </div>
  );
}

export default Equipment;