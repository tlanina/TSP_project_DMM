namespace TSP_DMM;

public class AdjacencyMatrix : Graph
{
    private double?[,] matrix;

    public AdjacencyMatrix(int n)
    {
        VerticesCount = n;
        matrix = new double?[n, n];
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

    public override int GetWeight(int u, int v)
    {
        return (int)(matrix[u, v] ?? double.PositiveInfinity);
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

    public int EdgesCount()
    {
        int count = 0;
        for (int i = 0; i < VerticesCount; i++)
        {
            for (int j = i + 1; j < VerticesCount; j++)
            {
                if (matrix[i, j].HasValue) 
                {
                    count++;
                }
            }
        }
        return count;
    }
}
