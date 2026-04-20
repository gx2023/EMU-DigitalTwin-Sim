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
            var openSet = new List<Node> { start };
            var closedSet = new HashSet<Node>();

            // 初始化起点的g和f值
            start.G = 0;
            start.F = CalculateHeuristic(start, goal);

            while (openSet.Count > 0)
            {
                // 找到f值最小的节点
                var current = openSet.OrderBy(n => n.F).First();

                // 如果到达目标节点，回溯路径
                if (current == goal)
                {
                    return ReconstructPath(current);
                }

                openSet.Remove(current);
                closedSet.Add(current);

                // 遍历所有相邻节点
                foreach (var neighbor in GetNeighbors(current, edges))
                {
                    // 如果邻居在闭集中，跳过
                    if (closedSet.Contains(neighbor))
                    {
                        continue;
                    }

                    // 计算从起点到邻居的暂定g值
                    var tentativeG = current.G + CalculateDistance(current, neighbor);

                    // 如果邻居不在开集中，或者暂定g值更小
                    if (!openSet.Contains(neighbor) || tentativeG < neighbor.G)
                    {
                        // 更新邻居的父节点和g、f值
                        neighbor.Parent = current;
                        neighbor.G = tentativeG;
                        neighbor.F = neighbor.G + CalculateHeuristic(neighbor, goal);

                        // 如果邻居不在开集中，添加到开集
                        if (!openSet.Contains(neighbor))
                        {
                            openSet.Add(neighbor);
                        }
                    }
                }
            }

            // 没有找到路径
            return null;
        }

        private List<Node> GetNeighbors(Node node, List<Edge> edges)
        {
            var neighbors = new List<Node>();

            foreach (var edge in edges)
            {
                if (edge.Start == node)
                {
                    neighbors.Add(edge.End);
                }
                else if (edge.End == node)
                {
                    neighbors.Add(edge.Start);
                }
            }

            return neighbors;
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