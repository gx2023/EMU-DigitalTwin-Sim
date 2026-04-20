import { Card, Form, Input, Button, Typography, Alert } from 'antd';
import { LockOutlined, UserOutlined } from '@ant-design/icons';
import { useNavigate } from 'react-router-dom';
import { useState } from 'react';
import './index.css';

const { Title, Text } = Typography;

function Login() {
  const [form] = Form.useForm();
  const [error, setError] = useState('');
  const navigate = useNavigate();

  const onFinish = (values) => {
    console.log('Login form values:', values);
    // 模拟登录验证
    if (values.username === 'admin' && values.password === 'admin123') {
      // 登录成功，跳转到首页
      navigate('/');
    } else {
      setError('用户名或密码错误');
    }
  };

  return (
    <div className="login-container">
      <Card className="login-card">
        <Title level={3} className="login-title">动车组检修数字孪生仿真系统</Title>
        <Text className="login-subtitle">请登录系统</Text>
        
        {error && (
          <Alert
            message={error}
            type="error"
            style={{ marginBottom: 20 }}
            onClose={() => setError('')}
          />
        )}
        
        <Form
          form={form}
          name="login"
          onFinish={onFinish}
          initialValues={{ remember: true }}
        >
          <Form.Item
            name="username"
            rules={[{ required: true, message: '请输入用户名' }]}
          >
            <Input
              prefix={<UserOutlined className="site-form-item-icon" />}
              placeholder="用户名"
              size="large"
            />
          </Form.Item>
          <Form.Item
            name="password"
            rules={[{ required: true, message: '请输入密码' }]}
          >
            <Input
              prefix={<LockOutlined className="site-form-item-icon" />}
              type="password"
              placeholder="密码"
              size="large"
            />
          </Form.Item>
          <Form.Item>
            <Button type="primary" htmlType="submit" className="login-button" size="large">
              登录
            </Button>
          </Form.Item>
        </Form>
        
        <div className="login-tip">
          <Text type="secondary">默认用户名: admin</Text>
          <Text type="secondary">默认密码: admin123</Text>
        </div>
      </Card>
    </div>
  );
}

export default Login;