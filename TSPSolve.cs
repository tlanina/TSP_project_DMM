public static class TSPSolve
{
    public static (double cost, List<int> path) Solve(Graph graph)
    {
        int n = graph.VerticesCount;
        var visited = new bool[n];
        var path = new List<int>();
        double totalCost = 0;

        int current = 0;
        path.Add(current);
        visited[current] = true;

        for (int step = 1; step < n; step++)
        {
            var neighbors = graph.GetNeighbors(current).Where(neigh => !visited[neigh.Item1]).OrderBy(neigh => neigh.Item2).ThenBy(neigh => neigh.Item1).ToList();

            if (neighbors.Count == 0)
                break;

            var (next, weight) = neighbors.First();
            path.Add(next);
            visited[next] = true;
            totalCost += weight;
            current = next;
        }
        
        double returnWeight = graph.GetWeight(current, path[0]);
        if (returnWeight != double.PositiveInfinity && returnWeight != int.MaxValue)
        {
            path.Add(path[0]);
            totalCost += returnWeight;
        }

        return (totalCost, path);
    }
}