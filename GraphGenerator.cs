public static class GraphGenerator
{
    private static Random random = new Random();

    public static Dictionary<(int, int), int> GenerateEdgeWeights(int n, double density)
    {
        int maxEdges = n * (n - 1) / 2;
        int targetEdges = (int)(maxEdges * density);
        var edgeWeights = new Dictionary<(int, int), int>();
        
        while (edgeWeights.Count < targetEdges)
        {
            int u = random.Next(n);
            int v = random.Next(n);
            if (u == v) continue;

            var key = (Math.Min(u, v), Math.Max(u, v));
            if (edgeWeights.ContainsKey(key)) continue;

            edgeWeights[key] = random.Next(1, 101);
        }

        return edgeWeights;
    }
}