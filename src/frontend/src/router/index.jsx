import { createBrowserRouter } from 'react-router-dom';
import Layout from '../components/Layout';
import Home from '../pages/Home';
import Dashboard from '../pages/Dashboard';
import Maintenance from '../pages/Maintenance';
import Equipment from '../pages/Equipment';
import Login from '../pages/Login';

const router = createBrowserRouter([
  {
    path: '/',
    element: <Layout />,
    children: [
      {
        path: '',
        element: <Home />,
      },
      {
        path: 'dashboard',
        element: <Dashboard />,
      },
      {
        path: 'maintenance',
        element: <Maintenance />,
      },
      {
        path: 'equipment',
        element: <Equipment />,
      },
    ],
  },
  {
    path: '/login',
    element: <Login />,
  },
]);

export default router;