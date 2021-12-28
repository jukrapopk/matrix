using System;
using Random = UnityEngine.Random;

namespace Classes
{
    internal class Matrix
    {
        public int X { get; private set; }
        public int Y { get; private set; }

        private int[,] Tab;

        public Matrix(int x, int y)
        {
            if (x <= 0 || y <= 0)
                throw new System.ArgumentException("Invalid arguments");

            X = x;
            Y = y;

            Tab = new int[Y, X];
            for (int i = 0; i < X; ++i)
            {
                for (int j = 0; j < Y; ++j)
                {
                    Tab[j, i] = 0;
                }
            }
        }

        public Matrix(int[,] values)
        {
            if (values == null)
                throw new System.ArgumentException("Invalid arguments");

            X = values.GetLength(1);
            Y = values.GetLength(0);

            Tab = new int[Y, X];
            for (int i = 0; i < X; ++i)
            {
                for (int j = 0; j < Y; ++j)
                {
                    Tab[j, i] = values[j, i];
                }
            }
        }

        public int GetValue(int x, int y)
        {
            if (x < 0 || x >= X || y < 0 || y >= Y)
                throw new System.ArgumentException("Invalid arguments");

            return Tab[y, x];
        }

        public void Print()
        {
            for (int y = 0; y < Y; ++y)
            {
                if (y == 0 || y == Y - 1)
                {
                    Console.Write("[");
                }
                else
                {
                    Console.Write("|");
                }

                for (int x = 0; x < X; ++x)
                {
                    Console.Write("" + GetValue(x, y));
                    if (x < X - 1)
                    {
                        Console.Write(" ");
                    }
                }

                if (y == 0 || y == Y - 1)
                {
                    Console.WriteLine("]");
                }
                else
                {
                    Console.WriteLine("|");
                }
            }
        }

        //find the biggest area (the area composed with the biggest number of elements)
        //return the number of elements from this area.
        public Result FindCountElementOfBiggestArea()
        {
            int maxNumber = GetValue(0, 0);
            bool[,] maxPositions = new bool[Y, X];
            int maxCount = 0;

            int[,] tempTab = (int[,]) Tab.Clone();
            for (int j = 0; j < Y; ++j)
            {
                for (int i = 0; i < X; ++i)
                {
                    if (GetValue(i, j) != -1)
                    {
                        int number = GetValue(i, j);
                        bool[,] positions = new bool[Y, X];
                        int count = GetRegionSize(i, j, number, tempTab, positions);
                        if (count >= maxCount)
                        {
                            maxNumber = number;
                            maxPositions = positions;
                            maxCount = count;
                        }
                    }
                }
            }

            return new Result(maxNumber, maxCount, maxPositions);
        }

        private int GetRegionSize(int x, int y, int number, int[,] matrix, bool[,] positions)
        {
            int count = 1;
            if (x < 0 || y < 0 || x >= X || y > Y || number == -1)
            {
                return 0;
            }

            matrix[y, x] = -1;
            positions[y, x] = true;
            for (int c = x - 1; c <= x + 1; c++)
            {
                if (c < 0 || c >= X || c == x || matrix[y, c] != number) continue;
                count += GetRegionSize(c, y, number, matrix, positions);
                positions[y, c] = true;
            }

            for (int r = y - 1; r <= y + 1; r++)
            {
                if (r < 0 || r >= Y || r == y || matrix[r, x] != number) continue;
                count += GetRegionSize(x, r, number, matrix, positions);
                positions[r, x] = true;
            }

            return count;
        }

        public void Randomize(int minValue, int maxValue)
        {
            for (int i = 0; i < X; i++)
            {
                for (int j = 0; j < Y; j++)
                {
                    Tab[j, i] = Random.Range(minValue, maxValue + 1);
                }
            }
        }
    }

    internal class Result
    {
        public int Number { get; private set; }
        public int Count { get; private set; }

        public bool[,] Positions { get; }

        public Result(int number, int count, bool[,] positions)
        {
            this.Number = number;
            this.Count = count;
            this.Positions = positions;
        }
    }
}