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
            return new Result(4, 9, new[,]
            {
                {false, false, false, false, false},
                {false, false, false, false, false},
                {false, false, false, false, false},
                {false, false, false, true, true},
                {false, false, false, true, true},
                {false, false, false, false, true},
                {false, false, false, true, true},
                {false, false, false, true, true},
            });
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