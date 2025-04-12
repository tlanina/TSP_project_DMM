using System;
using System.Diagnostics;
using System.Linq;
using System.Collections.Generic;

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
                    double weight = graph.GetWeight(i, j);
                    if (weight == double.PositiveInfinity)
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

    static void PrintAdjacencyList(Graph graph)
    {
        int n = graph.VerticesCount;
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.WriteLine("[Список суміжності]");

        for (int i = 0; i < n; i++)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write($"{i,3}: ");
            Console.ResetColor();

            var neighbors = graph.GetNeighbors(i);
            if (neighbors.Count == 0)
            {
                Console.WriteLine("Немає суміжних вершин.");
            }
            else
            {
                foreach (var (neighbor, weight) in neighbors)
                {
                    Console.Write($"({neighbor}, {weight}) ");
                }
                Console.WriteLine();
            }
        }

        Console.WriteLine();
    }

    static void Main(string[] args)
    {
        int n = 40;
        double density = 1;

        var edgeWeights = GraphGenerator.GenerateEdgeWeights(n, density);
        
        var matrixGraph = new AdjacencyMatrix(n, edgeWeights);
        var listGraph = new AdjacencyList(n, edgeWeights);

        Console.WriteLine($"Кількість вершин: {n}");
        Console.WriteLine($"Кількість ребер: {edgeWeights.Count}");
        
        PrintAdjacencyMatrix(matrixGraph);
        PrintAdjacencyList(listGraph);
        
        Console.WriteLine("\nЖадібний алгоритм (матриця суміжності):");
        var stopwatchMatrix = Stopwatch.StartNew();
        var (costMatrix, pathMatrix) = TSPSolve.Solve(matrixGraph);
        stopwatchMatrix.Stop();
        Console.WriteLine(string.Join(" -> ", pathMatrix));
        Console.WriteLine($"Загальна довжина шляху: {costMatrix}");
        Console.WriteLine($"Час виконання (ms): {stopwatchMatrix.Elapsed.TotalMilliseconds}");
        
        Console.WriteLine("\nЖадібний алгоритм (список суміжності):");
        var stopwatchList = Stopwatch.StartNew();
        var (costList, pathList) = TSPSolve.Solve(listGraph);
        stopwatchList.Stop();
        Console.WriteLine(string.Join(" -> ", pathList));
        Console.WriteLine($"Загальна довжина шляху: {costList}");
        Console.WriteLine($"Час виконання (ms): {stopwatchList.Elapsed.TotalMilliseconds}");
    }
}
