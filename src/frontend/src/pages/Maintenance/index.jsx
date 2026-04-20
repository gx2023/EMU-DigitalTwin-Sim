import { Card, Row, Col, Typography, Table, Button, Space, Tag, Modal, Form, Input, Select, DatePicker, Radio } from 'antd';
import { PlusOutlined, EditOutlined, DeleteOutlined, EyeOutlined } from '@ant-design/icons';
import { useState } from 'react';
import './index.css';

const { Title, Text } = Typography;
const { Option } = Select;
const { RangePicker } = DatePicker;

function Maintenance() {
  const [isModalVisible, setIsModalVisible] = useState(false);
  const [selectedRecord, setSelectedRecord] = useState(null);
  const [form] = Form.useForm();

  // 模拟检修作业数据
  const maintenanceData = [
    {
      key: '1',
      equipment: 'CRH380A-001',
      type: '一级修',
      status: '进行中',
      startTime: '2024-01-15 08:00',
      estimatedEndTime: '2024-01-15 12:00',
      operator: '张师傅',
      progress: 65,
    },
    {
      key: '2',
      equipment: 'CR400AF-002',
      type: '二级修',
      status: '待开始',
      startTime: '2024-01-15 13:00',
      estimatedEndTime: '2024-01-16 13:00',
      operator: '李师傅',
      progress: 0,
    },
    {
      key: '3',
      equipment: 'CRH380B-003',
      type: '一级修',
      status: '已完成',
      startTime: '2024-01-14 08:00',
      estimatedEndTime: '2024-01-14 12:00',
      operator: '王师傅',
      progress: 100,
    },
    {
      key: '4',
      equipment: 'CR400BF-004',
      type: '二级修',
      status: '进行中',
      startTime: '2024-01-14 13:00',
      estimatedEndTime: '2024-01-15 13:00',
      operator: '赵师傅',
      progress: 45,
    },
  ];

  // 模拟工艺卡片数据
  const processCards = [
    {
      key: '1',
      name: '转向架一级修检查',
      type: '一级修',
      model: 'CRH380A',
      version: 'V1.0',
      status: '已发布',
    },
    {
      key: '2',
      name: '制动系统检查',
      type: '一级修',
      model: 'CRH380A',
      version: 'V1.0',
      status: '已发布',
    },
    {
      key: '3',
      name: '牵引系统检查',
      type: '二级修',
      model: 'CR400AF',
      version: 'V1.0',
      status: '草稿',
    },
    {
      key: '4',
      name: '车顶设备检查',
      type: '二级修',
      model: 'CR400AF',
      version: 'V1.0',
      status: '已发布',
    },
  ];

  const handleAdd = () => {
    setSelectedRecord(null);
    form.resetFields();
    setIsModalVisible(true);
  };

  const handleEdit = (record) => {
    setSelectedRecord(record);
    form.setFieldsValue(record);
    setIsModalVisible(true);
  };

  const handleDelete = (key) => {
    // 模拟删除操作
    console.log('Delete:', key);
  };

  const handleOk = () => {
    form.validateFields().then((values) => {
      console.log('Form values:', values);
      setIsModalVisible(false);
    });
  };

  const handleCancel = () => {
    setIsModalVisible(false);
  };

  const maintenanceColumns = [
    {
      title: '设备编号',
      dataIndex: 'equipment',
      key: 'equipment',
    },
    {
      title: '检修类型',
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
          case '待开始':
            color = 'blue';
            break;
          case '进行中':
            color = 'green';
            break;
          case '已完成':
            color = 'gray';
            break;
          default:
            color = 'blue';
        }
        return <Tag color={color}>{status}</Tag>;
      },
    },
    {
      title: '开始时间',
      dataIndex: 'startTime',
      key: 'startTime',
    },
    {
      title: '预计结束时间',
      dataIndex: 'estimatedEndTime',
      key: 'estimatedEndTime',
    },
    {
      title: '操作人员',
      dataIndex: 'operator',
      key: 'operator',
    },
    {
      title: '进度',
      dataIndex: 'progress',
      key: 'progress',
      render: (progress) => (
        <div className="progress-container">
          <div className="progress-bar">
            <div className="progress-fill" style={{ width: `${progress}%` }} />
          </div>
          <Text>{progress}%</Text>
        </div>
      ),
    },
    {
      title: '操作',
      key: 'action',
      render: (_, record) => (
        <Space size="middle">
          <Button size="small" icon={<EyeOutlined />}>查看</Button>
          <Button size="small" icon={<EditOutlined />} onClick={() => handleEdit(record)}>编辑</Button>
          <Button size="small" danger icon={<DeleteOutlined />} onClick={() => handleDelete(record.key)}>删除</Button>
        </Space>
      ),
    },
  ];

  const processColumns = [
    {
      title: '工艺卡片名称',
      dataIndex: 'name',
      key: 'name',
    },
    {
      title: '检修类型',
      dataIndex: 'type',
      key: 'type',
    },
    {
      title: '适用车型',
      dataIndex: 'model',
      key: 'model',
    },
    {
      title: '版本',
      dataIndex: 'version',
      key: 'version',
    },
    {
      title: '状态',
      dataIndex: 'status',
      key: 'status',
      render: (status) => {
        let color = '';
        switch (status) {
          case '草稿':
            color = 'blue';
            break;
          case '已发布':
            color = 'green';
            break;
          default:
            color = 'blue';
        }
        return <Tag color={color}>{status}</Tag>;
      },
    },
    {
      title: '操作',
      key: 'action',
      render: (_, record) => (
        <Space size="middle">
          <Button size="small" icon={<EyeOutlined />}>查看</Button>
          <Button size="small" icon={<EditOutlined />}>编辑</Button>
        </Space>
      ),
    },
  ];

  return (
    <div className="maintenance-container">
      <Title level={3}>检修作业管理</Title>
      
      <Row gutter={[16, 16]}>
        <Col xs={24}>
          <Card title="检修作业列表" extra={<Button type="primary" icon={<PlusOutlined />} onClick={handleAdd}>添加作业</Button>}>
            <Table columns={maintenanceColumns} dataSource={maintenanceData} />
          </Card>
        </Col>
        <Col xs={24}>
          <Card title="工艺卡片管理">
            <Table columns={processColumns} dataSource={processCards} />
          </Card>
        </Col>
      </Row>

      <Modal
        title={selectedRecord ? '编辑检修作业' : '添加检修作业'}
        open={isModalVisible}
        onOk={handleOk}
        onCancel={handleCancel}
      >
        <Form form={form} layout="vertical">
          <Form.Item name="equipment" label="设备编号" rules={[{ required: true, message: '请输入设备编号' }]}>
            <Input placeholder="请输入设备编号" />
          </Form.Item>
          <Form.Item name="type" label="检修类型" rules={[{ required: true, message: '请选择检修类型' }]}>
            <Select placeholder="请选择检修类型">
              <Option value="一级修">一级修</Option>
              <Option value="二级修">二级修</Option>
            </Select>
          </Form.Item>
          <Form.Item name="status" label="状态" rules={[{ required: true, message: '请选择状态' }]}>
            <Radio.Group>
              <Radio value="待开始">待开始</Radio>
              <Radio value="进行中">进行中</Radio>
              <Radio value="已完成">已完成</Radio>
            </Radio.Group>
          </Form.Item>
          <Form.Item name="startTime" label="开始时间" rules={[{ required: true, message: '请选择开始时间' }]}>
            <DatePicker showTime placeholder="请选择开始时间" style={{ width: '100%' }} />
          </Form.Item>
          <Form.Item name="estimatedEndTime" label="预计结束时间" rules={[{ required: true, message: '请选择预计结束时间' }]}>
            <DatePicker showTime placeholder="请选择预计结束时间" style={{ width: '100%' }} />
          </Form.Item>
          <Form.Item name="operator" label="操作人员" rules={[{ required: true, message: '请输入操作人员' }]}>
            <Input placeholder="请输入操作人员" />
          </Form.Item>
          <Form.Item name="progress" label="进度" rules={[{ required: true, message: '请输入进度' }]}>
            <Input type="number" placeholder="请输入进度" />
          </Form.Item>
        </Form>
      </Modal>
    </div>
  );
}

export default Maintenance;