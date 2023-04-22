using System;
using System.Collections.Generic;

class Minefield
{
    static int[,] field;
    static int[,] distances = new int[5, 5];
    static int[,] parents = new int[5, 5];
    static void GenerateMinefield() {
        Random random = new Random();

        field = new int[5, 5];
        for (int row = 0; row < 5; row++) {
            for (int column = 0; column < 5; column++) {
                field[row, column] = random.Next(2);
            }
        }
    }

    static void PrintMinefield()
    {
        for (int row = 0; row < 5; row++)
        {
            for (int column = 0; column < 5; column++)
            {
                Console.Write(field[row, column] + " ");
            }
            Console.WriteLine();
        }
    }

    static void Main()
    {
        GenerateMinefield();
        PrintMinefield();
        int start = -1;
        for (int i = 0; i < 5; i++)
        {
            if (field[0, i] == 0)
            {
                start = i;
                break;
            }
        }
        if (start == -1)
        {
            Console.WriteLine("There is no safe path in the first row.");
        }
        else
        {
            Console.WriteLine("Safe path:");
            Astar(start);
        }
    }

    static void Astar(int start)
    {
        List<int> openSet = new List<int>();
        List<int> closedSet = new List<int>();

        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                distances[i, j] = int.MaxValue;
            }
        }

        distances[0, start] = 0;
        openSet.Add(0 * 5 + start);

        while (openSet.Count > 0)
        {
            int current = GetLowestDistanceNode(openSet);
            openSet.Remove(current);

            int row = current / 5;
            int column = current % 5;

            if (row == 4)
            {
                PrintPath(start, current);
                return;
            }

            closedSet.Add(current);

            foreach (int neighbor in GetNeighbors(row, column))
            {
                if (closedSet.Contains(neighbor))
                {
                    continue;
                }

                int neighborRow = neighbor / 5;
                int neighborColumn = neighbor % 5;

                int tentativeDistance = distances[row, column] + GetCost(row, column, neighborRow, neighborColumn);

                if (!openSet.Contains(neighbor))
                {
                    openSet.Add(neighbor);
                }
                else if (tentativeDistance >= distances[neighborRow, neighborColumn])
                {
                    continue;
                }

                parents[neighborRow, neighborColumn] = current;
                distances[neighborRow, neighborColumn] = tentativeDistance;
            }
        }

        Console.WriteLine("No safe path found.");
    }

    static int GetLowestDistanceNode(List<int> nodes)
    {
        int lowestIndex = 0;
        for (int i = 1; i < nodes.Count; i++)
        {
            if (distances[nodes[i] / 5, nodes[i] % 5] < distances[nodes[lowestIndex] / 5, nodes[lowestIndex] % 5])
            {
                lowestIndex = i;
            }
        }
        return nodes[lowestIndex];
    }

    static IEnumerable<int> GetNeighbors(int row, int column)
    {
        List<int> neighbors = new List<int>();
        if (row > 0)
        {
            if (column > 0 && field[row - 1, column - 1] == 0)
            {
                neighbors.Add((row - 1) * 5 + column - 1);
            }
            if (field[row - 1, column] == 0)
            {
                neighbors.Add((row - 1) * 5 + column);
            }
            if (column < 4 && field[row - 1, column + 1] == 0)
            {
                neighbors.Add((row - 1) * 5 + column + 1);
            }
        }
        if (column > 0 && field[row, column - 1] == 0)
        {
            neighbors.Add(row * 5 + column - 1);
        }
        if (column < 4 && field[row, column + 1] == 0)
        {
            neighbors.Add(row * 5 + column + 1);
        }
        if (row < 4)
        {
            if (column > 0 && field[row + 1, column - 1] == 0)
            {
                neighbors.Add((row + 1) * 5 + column - 1);
            }
            if (field[row + 1, column] == 0)
            {
                neighbors.Add((row + 1) * 5 + column);
            }
            if (column < 4 && field[row + 1, column + 1] == 0)
            {
                neighbors.Add((row + 1) * 5 + column + 1);
            }
        }
        return neighbors;
    }

    static int GetCost(int fromRow, int fromColumn, int toRow, int toColumn)
    {
        int rowDiff = Math.Abs(fromRow - toRow);
        int columnDiff = Math.Abs(fromColumn - toColumn);
        if (rowDiff == 0 || columnDiff == 0)
        {
            return 1;
        }
        return 2;
    }

    static void PrintPath(int start, int current)
    {
        List<int> path = new List<int>();
        while (current != start)
        {
            path.Add(current);
            current = parents[current / 5, current % 5];
        }
        path.Add(start);
        path.Reverse();
        foreach (int node in path)
        {
            Console.WriteLine($"({node / 5}, {node % 5})");
        }
    }
}

