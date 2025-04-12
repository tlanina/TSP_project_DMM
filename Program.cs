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
        int n = 20;
        double density = 1.0;
        int experimentsCount = 20;

        double totalTimeMatrix = 0;
        double totalTimeList = 0;
        double totalCostMatrix = 0;
        double totalCostList = 0;

        Console.WriteLine($"[Експерименти для графа з {n} вершинами та щільністю {density}]");
        Console.WriteLine($"Виконується {experimentsCount} запусків для кожної структури...\n");

        Console.WriteLine($"Кількість вершин: {n}");
        
        for (int i = 0; i < experimentsCount; i++)
        {

            var edgeWeights = GraphGenerator.GenerateEdgeWeights(n, density);
            var matrixGraph = new AdjacencyMatrix(n, edgeWeights);
            var listGraph = new AdjacencyList(n, edgeWeights);
            PrintAdjacencyMatrix(matrixGraph);
            PrintAdjacencyList(listGraph);
            Console.WriteLine($"Кількість ребер: {edgeWeights.Count}");

            var stopwatchMatrix = Stopwatch.StartNew();
            var (costMatrix, _) = TSPSolve.Solve(matrixGraph);
            stopwatchMatrix.Stop();

            totalTimeMatrix += stopwatchMatrix.Elapsed.TotalMilliseconds;
            totalCostMatrix += costMatrix;
            
            Console.WriteLine($"Час виконання для матриці суміжності (експеримент {i + 1}): {stopwatchMatrix.Elapsed.TotalMilliseconds:F4} ms");

            var stopwatchList = Stopwatch.StartNew();
            var (costList, _) = TSPSolve.Solve(listGraph);
            stopwatchList.Stop();

            totalTimeList += stopwatchList.Elapsed.TotalMilliseconds;
            totalCostList += costList;
            Console.WriteLine($"Час виконання для списку суміжності (експеримент {i + 1}): {stopwatchList.Elapsed.TotalMilliseconds:F4} ms");
        }

        Console.WriteLine($"[Результати після {experimentsCount} запусків]");
        Console.WriteLine($"\nМатриця суміжності:");
        Console.WriteLine($"  Середній час виконання: {totalTimeMatrix / experimentsCount:F4} ms");

        Console.WriteLine($"\nСписок суміжності:");
        Console.WriteLine($"  Середній час виконання: {totalTimeList / experimentsCount:F4} ms");

    }
}
