namespace TSP_DMM;

public abstract class Graph
{
    public int VerticesCount { get; protected set; }
    public abstract void AddEdge(int u, int v, double weight);
    public abstract bool HasEdge(int u, int v);
    public abstract int GetWeight(int u, int v);
    public abstract List<(int, double)> GetNeighbors(int u);
}