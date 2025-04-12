using TSP_DMM;

class Program
{
    static void PrintAdjacencyMatrix(Graph graph, int chunkSize = 20)
    {
        int n = graph.VerticesCount;
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.WriteLine("[Матриця суміжності]");

        for (int startCol = 0; startCol < n; startCol += chunkSize)
        {
            int endCol = Math.Min(startCol + chunkSize, n);

            Console.Write("     ");
            for (int j = startCol; j < endCol; j++)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write($"{j,4} ");
            }
            Console.ResetColor();
            Console.WriteLine();

            Console.WriteLine("     " + new string('-', 5 * (endCol - startCol)));

            for (int i = 0; i < n; i++)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write($"{i,3} | ");
                Console.ResetColor();

                for (int j = startCol; j < endCol; j++)
                {
                    int weight = graph.GetWeight(i, j);
                    if (weight == int.MaxValue || double.IsInfinity(weight))
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.Write($"{"∞",4} ");
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write($"{weight,4} ");
                    }
                    Console.ResetColor();
                }
                Console.WriteLine();
            }

            Console.WriteLine(); 
        }
    }
    static void Main(string[] args)
    {
        int n = 40;
        double fully =1;

        var edgeWeights = GraphGenerator.GenerateEdgeWeights(n, fully);

        var matrixGraph = new AdjacencyMatrix(n, edgeWeights);
        var listGraph = new AdjacencyList(n, edgeWeights);

        Console.WriteLine($"Кількість вершин: {n}");
        Console.WriteLine($"Кількість ребер: {edgeWeights.Count}");

       
        PrintAdjacencyMatrix(matrixGraph);
        
        listGraph.PrintAdjacencyList();

        var (cost, path) = TSPSolve.Solve(matrixGraph);
        Console.WriteLine("\nЖадібний алгоритм (найближчий сусід):");
        Console.WriteLine(string.Join(" -> ", path));
        Console.WriteLine($"Загальна довжина шляху: {cost:F2}");
    }


}