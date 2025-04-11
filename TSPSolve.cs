namespace TSP_DMM;

using System;
using System.Collections.Generic;

public class TSPSolve
{
    public static (double cost, List<int> path) Solve(Graph graph, int start = 0)
    {
        int n = graph.VerticesCount;
        bool[] visited = new bool[n];
        List<int> path = new List<int>();
        double totalCost = 0;

        int current = start;
        visited[current] = true;
        path.Add(current);

        for (int step = 1; step < n; step++)
        {
            double minDistance = double.PositiveInfinity;
            int next = -1;

            foreach (var (neighbor, weight) in graph.GetNeighbors(current))
            {
                if (!visited[neighbor] && weight < minDistance)
                {
                    minDistance = weight;
                    next = neighbor;
                }
            }

            if (next == -1)
                break;

            visited[next] = true;
            path.Add(next);
            totalCost += minDistance;
            current = next;
        }
        totalCost += graph.GetWeight(current, start);
        path.Add(start);

        return (totalCost, path);
    }
}
