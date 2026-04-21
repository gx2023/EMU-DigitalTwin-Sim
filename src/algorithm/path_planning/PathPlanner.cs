using System;
using System.Collections.Generic;
using System.Linq;

namespace EMU.DT.Algorithm.PathPlanning
{
    public class PathPlanner
    {
        public List<Node> PlanPath(Node start, Node goal, List<Edge> edges, List<Obstacle> obstacles)
        {
            // 使用A*算法进行路径规划
            var openSet = new PriorityQueue<Node>((a, b) => a.F.CompareTo(b.F));
            var closedSet = new HashSet<Node>();
            var nodeMap = new Dictionary<Node, Node>(); // 用于快速查找节点

            // 初始化起点的g和f值
            start.G = 0;
            start.F = CalculateHeuristic(start, goal);
            openSet.Enqueue(start);
            nodeMap[start] = start;

            while (openSet.Count > 0)
            {
                // 找到f值最小的节点
                var current = openSet.Dequeue();

                // 如果到达目标节点，回溯路径
                if (current == goal)
                {
                    return ReconstructPath(current);
                }

                closedSet.Add(current);

                // 遍历所有相邻节点
                foreach (var neighbor in GetNeighbors(current, edges, obstacles))
                {
                    // 如果邻居在闭集中，跳过
                    if (closedSet.Contains(neighbor))
                    {
                        continue;
                    }

                    // 计算从起点到邻居的暂定g值
                    var tentativeG = current.G + CalculateDistance(current, neighbor);

                    // 如果邻居不在开集中，或者暂定g值更小
                    if (!nodeMap.ContainsKey(neighbor) || tentativeG < neighbor.G)
                    {
                        // 更新邻居的父节点和g、f值
                        neighbor.Parent = current;
                        neighbor.G = tentativeG;
                        neighbor.F = neighbor.G + CalculateHeuristic(neighbor, goal);

                        // 如果邻居不在开集中，添加到开集
                        if (!nodeMap.ContainsKey(neighbor))
                        {
                            openSet.Enqueue(neighbor);
                            nodeMap[neighbor] = neighbor;
                        }
                    }
                }
            }

            // 没有找到路径
            return null;
        }

        private List<Node> GetNeighbors(Node node, List<Edge> edges, List<Obstacle> obstacles)
        {
            var neighbors = new List<Node>();

            foreach (var edge in edges)
            {
                Node neighbor = null;
                if (edge.Start == node)
                {
                    neighbor = edge.End;
                }
                else if (edge.End == node)
                {
                    neighbor = edge.Start;
                }

                if (neighbor != null && !IsObstacle(neighbor, obstacles))
                {
                    neighbors.Add(neighbor);
                }
            }

            return neighbors;
        }

        private bool IsObstacle(Node node, List<Obstacle> obstacles)
        {
            // 简单的障碍物检测
            foreach (var obstacle in obstacles)
            {
                // 这里可以实现更复杂的碰撞检测算法
                foreach (var point in obstacle.Points)
                {
                    if (Math.Abs(node.X - point.X) < 0.5 && Math.Abs(node.Y - point.Y) < 0.5)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private double CalculateDistance(Node node1, Node node2)
        {
            // 计算欧几里得距离
            return Math.Sqrt(Math.Pow(node1.X - node2.X, 2) + Math.Pow(node1.Y - node2.Y, 2));
        }

        private double CalculateHeuristic(Node node, Node goal)
        {
            // 使用欧几里得距离作为启发函数
            return CalculateDistance(node, goal);
        }

        private List<Node> ReconstructPath(Node current)
        {
            var path = new List<Node>();

            while (current != null)
            {
                path.Add(current);
                current = current.Parent;
            }

            path.Reverse();
            return path;
        }
    }

    // 简单的优先队列实现
    public class PriorityQueue<T>
    {
        private List<T> _items;
        private Comparison<T> _comparison;

        public int Count => _items.Count;

        public PriorityQueue(Comparison<T> comparison)
        {
            _items = new List<T>();
            _comparison = comparison;
        }

        public void Enqueue(T item)
        {
            _items.Add(item);
            int i = _items.Count - 1;
            while (i > 0)
            {
                int parent = (i - 1) / 2;
                if (_comparison(_items[i], _items[parent]) >= 0)
                    break;
                Swap(i, parent);
                i = parent;
            }
        }

        public T Dequeue()
        {
            T result = _items[0];
            _items[0] = _items[_items.Count - 1];
            _items.RemoveAt(_items.Count - 1);
            int i = 0;
            while (true)
            {
                int left = 2 * i + 1;
                int right = 2 * i + 2;
                int smallest = i;
                if (left < _items.Count && _comparison(_items[left], _items[smallest]) < 0)
                    smallest = left;
                if (right < _items.Count && _comparison(_items[right], _items[smallest]) < 0)
                    smallest = right;
                if (smallest == i)
                    break;
                Swap(i, smallest);
                i = smallest;
            }
            return result;
        }

        private void Swap(int i, int j)
        {
            T temp = _items[i];
            _items[i] = _items[j];
            _items[j] = temp;
        }
    }

    public class Node
    {
        public int Id { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public double G { get; set; } // 从起点到当前节点的实际成本
        public double F { get; set; } // 估计总成本 (G + H)
        public Node Parent { get; set; }
    }

    public class Edge
    {
        public Node Start { get; set; }
        public Node End { get; set; }
        public double Weight { get; set; }
    }

    public class Obstacle
    {
        public int Id { get; set; }
        public List<Point> Points { get; set; }
    }

    public class Point
    {
        public double X { get; set; }
        public double Y { get; set; }
    }
}