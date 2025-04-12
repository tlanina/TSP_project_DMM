public class AdjacencyMatrix : Graph
{
    private double?[,] matrix;

    public AdjacencyMatrix(int n)
    {
        VerticesCount = n;
        matrix = new double?[n, n];
    }

    public AdjacencyMatrix(int n, Dictionary<(int, int), double> edgeWeights) : this(n)
    {
        foreach (var ((u, v), weight) in edgeWeights)
        {
            AddEdge(u, v, weight);
        }
    }

    public override void AddEdge(int u, int v, double weight)
    {
        matrix[u, v] = weight;
        matrix[v, u] = weight;
    }

    public override bool HasEdge(int u, int v)
    {
        return matrix[u, v].HasValue;
    }

    public override double GetWeight(int u, int v)
    {
        return matrix[u, v] ?? double.PositiveInfinity;
    }

    public override List<(int, double)> GetNeighbors(int u)
    {
        var neighbors = new List<(int, double)>();
        for (int v = 0; v < VerticesCount; v++)
        {
            if (matrix[u, v].HasValue)
                neighbors.Add((v, matrix[u, v].Value));
        }
        return neighbors;
    }
}