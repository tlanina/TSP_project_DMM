namespace TSP_DMM;

public static class GraphGenerator
{
    private static Random random = new Random();

    public static Graph GenerateRandomGraph(int n, double density, bool useMatrix = true)
    {
        int maxEdges = n * (n - 1) / 2;
        int targetEdges = (int)(maxEdges * density);
        Graph graph = useMatrix ? new AdjacencyMatrix(n) : new AdjacencyList(n);

        var edgeSet = new HashSet<(int, int)>();

        while (edgeSet.Count < targetEdges)
        {
            int u = random.Next(n);
            int v = random.Next(n);
            if (u == v || edgeSet.Contains((Math.Min(u, v), Math.Max(u, v))))
                continue;

            int weight = random.Next(1, 101);
            graph.AddEdge(u, v, weight);
            edgeSet.Add((Math.Min(u, v), Math.Max(u, v)));
        }

        return graph;
    }
}