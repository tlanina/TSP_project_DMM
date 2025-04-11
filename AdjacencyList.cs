namespace TSP_DMM;

public class AdjacencyList : Graph
{
    private List<(int, double)>[] adjList;

    public AdjacencyList(int n)
    {
        VerticesCount = n;
        adjList = new List<(int, double)>[n];
        for (int i = 0; i < n; i++)
            adjList[i] = new List<(int, double)>();
    }

    public override void AddEdge(int u, int v, double weight)
    {
        adjList[u].Add((v, weight));
        adjList[v].Add((u, weight));  
    }

    public override bool HasEdge(int u, int v)
    {
        return adjList[u].Any(e => e.Item1 == v);
    }

    public override int GetWeight(int u, int v)
    {
        var edge = adjList[u].FirstOrDefault(e => e.Item1 == v);
        return (int)(edge == default ? double.PositiveInfinity : edge.Item2);
    }

    public override List<(int, double)> GetNeighbors(int u)
    {
        return adjList[u];
    }

    public int EdgesCount()
    {
        int count = 0;
        for (int i = 0; i < VerticesCount; i++)
        {
            foreach (var neighbor in adjList[i])
            {
                if (i < neighbor.Item1)  
                    count++;
            }
        }
        return count;
    }
}
