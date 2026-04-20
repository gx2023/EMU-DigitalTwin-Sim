import { Layout as AntLayout, Menu, Button } from 'antd';
import { Outlet, useNavigate } from 'react-router-dom';
import { DashboardOutlined, HomeOutlined, ToolOutlined, SettingOutlined, LogoutOutlined } from '@ant-design/icons';
import './index.css';

const { Header, Sider, Content } = AntLayout;

function Layout() {
  const navigate = useNavigate();

  const menuItems = [
    {
      key: 'home',
      icon: <HomeOutlined />,
      label: '首页',
      onClick: () => navigate('/'),
    },
    {
      key: 'dashboard',
      icon: <DashboardOutlined />,
      label: '运营看板',
      onClick: () => navigate('/dashboard'),
    },
    {
      key: 'maintenance',
      icon: <ToolOutlined />,
      label: '检修作业',
      onClick: () => navigate('/maintenance'),
    },
    {
      key: 'equipment',
      icon: <SettingOutlined />,
      label: '设备管理',
      onClick: () => navigate('/equipment'),
    },
  ];

  return (
    <AntLayout style={{ minHeight: '100vh' }}>
      <Sider theme="dark" width={256}>
        <div className="logo" />
        <Menu
          mode="inline"
          theme="dark"
          items={menuItems}
          style={{ height: '100%', borderRight: 0 }}
        />
      </Sider>
      <AntLayout>
        <Header style={{ display: 'flex', alignItems: 'center', justifyContent: 'flex-end', padding: 0, paddingRight: 24 }}>
          <Button 
            type="text" 
            icon={<LogoutOutlined />} 
            onClick={() => navigate('/login')}
            style={{ color: '#fff' }}
          >
            退出登录
          </Button>
        </Header>
        <Content style={{ margin: '24px 16px', padding: 24, background: '#fff', minHeight: 280 }}>
          <Outlet />
        </Content>
      </AntLayout>
    </AntLayout>
  );
}

export default Layout;