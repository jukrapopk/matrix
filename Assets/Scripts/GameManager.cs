using System;
using Classes;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Matrix Settings")]
    [SerializeField]
    private InitialMatrixVariation initialMatrix;

    [Header("Matrix Settings (For Empty and Custom)")]
    [Range(1, 16)]
    [SerializeField]
    private int x = 5;

    [Range(1, 16)]
    [SerializeField]
    private int y = 8;

    [Header("Matrix Settings (For Custom)")]
    [Range(0, 10)]
    [SerializeField]
    private int minValue = 0;

    [Range(0, 10)]
    [SerializeField]
    private int maxValue = 7;

    [Header("References")]
    [SerializeField]
    private TMP_Text matrixText;

    [SerializeField]
    private TMP_Text rangeText;

    [SerializeField]
    private TMP_Text sizeText;

    [SerializeField]
    private TMP_Text resultText;

    [SerializeField]
    private TMP_Text countText;

    private readonly int[,] _defaultMatrixArray =
    {
        {0, 0, 0, 2, 2},
        {1, 1, 7, 2, 2},
        {2, 2, 7, 2, 1},
        {2, 1, 7, 4, 4},
        {2, 7, 7, 4, 4},
        {4, 6, 6, 0, 4},
        {4, 4, 6, 4, 4},
        {4, 4, 6, 4, 4}
    };

    private Matrix _matrix;

    private void Start()
    {
        switch (initialMatrix)
        {
            case InitialMatrixVariation.Default:
                SetToDefaultMatrix();
                break;
            case InitialMatrixVariation.Empty:
                ClearMatrix();
                break;
            case InitialMatrixVariation.Custom:
                RandomizeMatrix();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void PrintMatrix(Matrix matrix)
    {
        rangeText.text = minValue + " - " + maxValue;
        sizeText.text = x + " x " + y;
        matrixText.text = "";
        resultText.text = "-";
        countText.text = "Count: -";

        for (int j = 0; j < matrix.Y; ++j)
        {
            if (j == 0 || j == matrix.Y - 1)
            {
                matrixText.text += "[";
            }
            else
            {
                matrixText.text += "|";
            }

            for (int i = 0; i < matrix.X; ++i)
            {
                int value = matrix.GetValue(i, j);
                matrixText.text += value;
                if (i < matrix.X - 1)
                {
                    matrixText.text += " ";
                }
            }

            if (j == 0 || j == matrix.Y - 1)
            {
                matrixText.text += "]\n";
            }
            else
            {
                matrixText.text += "|\n";
            }
        }

        matrixText.fontSize = 6 + (8 - Math.Max(matrix.X, matrix.Y)) * 0.5f;
    }

    private void PrintMatrixWithResult(Matrix matrix, bool[,] positions)
    {
        matrixText.text = "";

        for (int j = 0; j < matrix.Y; ++j)
        {
            if (j == 0 || j == matrix.Y - 1)
            {
                matrixText.text += "[";
            }
            else
            {
                matrixText.text += "|";
            }

            for (int i = 0; i < matrix.X; ++i)
            {
                int value = matrix.GetValue(i, j);
                matrixText.text += positions[j, i] ? "<color=#FF0000>" + value + "</color>" : value.ToString();
                if (i < matrix.X - 1)
                {
                    matrixText.text += " ";
                }
            }

            if (j == 0 || j == matrix.Y - 1)
            {
                matrixText.text += "]\n";
            }
            else
            {
                matrixText.text += "|\n";
            }
        }

        matrixText.fontSize = 6 + (8 - Math.Max(matrix.X, matrix.Y)) * 0.5f;
    }

    public void SetToDefaultMatrix()
    {
        x = 5;
        y = 8;
        minValue = 0;
        maxValue = 7;
        _matrix = new Matrix(_defaultMatrixArray);
        PrintMatrix(_matrix);
    }

    public void RandomizeMatrix()
    {
        _matrix = new Matrix(x, y);
        _matrix.Randomize(minValue, maxValue);
        PrintMatrix(_matrix);
    }

    public void ClearMatrix()
    {
        _matrix = new Matrix(x, y);
        PrintMatrix(_matrix);
    }

    public void ChangeMin(int value)
    {
        minValue = Mathf.Clamp(minValue + value, 0, maxValue);
        RandomizeMatrix();
    }

    public void ChangeMax(int value)
    {
        maxValue = Mathf.Clamp(maxValue + value, minValue, 9);
        RandomizeMatrix();
    }

    public void ChangeX(int value)
    {
        x = Mathf.Clamp(x + value, 1, 12);
        RandomizeMatrix();
    }

    public void ChangeY(int value)
    {
        y = Mathf.Clamp(y + value, 1, 12);
        RandomizeMatrix();
    }

    public void FindResult()
    {
        Result result = _matrix.FindCountElementOfBiggestArea();
        resultText.text = result.Number.ToString();
        countText.text = "Count: " + result.Count;
        PrintMatrixWithResult(_matrix, result.Positions);
    }
}

public enum InitialMatrixVariation
{
    Default,
    Empty,
    Custom
}