using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomGrid<T>
{
    public event EventHandler<OnGridValueChangedEventArgs> OnGridValueChanged;
    public class OnGridValueChangedEventArgs : EventArgs
    {
        public int x;
        public int y;
    }

    public int Width { get; private set; }
    public int Height { get; private set; }
    public float CellSize { get; private set; }

    private Vector3 OriginPosition;
    private T[,] GridArray;

    public CustomGrid(
        int width,
        int height,
        float cellSize,
        Vector3 originPosition,
        Func<CustomGrid<T>, int, int, T> cellInitializer)
    {
        Width = width;
        Height = height;
        CellSize = cellSize;
        OriginPosition = originPosition;

        GridArray = new T[Width, Height];

        for (var x = 0; x < Width; x++)
        {
            for (var y = 0; y < Height; y++)
            {
                GridArray[x, y] = cellInitializer(this, x, y);
            }
        }

        var debug = true;

        if (debug)
        {
            for (var x = 0; x < Width; x++)
            {
                for (var y = 0; y < Height; y++)
                {
                    Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1), Color.white, 100f);
                    Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), Color.white, 100f);
                }
            }
            Debug.DrawLine(GetWorldPosition(0, Height), GetWorldPosition(Width, Height), Color.white, 100f);
            Debug.DrawLine(GetWorldPosition(Width, 0), GetWorldPosition(Width, Height), Color.white, 100f);
        }
    }

    public bool SetValue(int x, int y, T value)
    {
        if (!IsInGrid(x, y))
        {
            return false;
        }

        GridArray[x, y] = value;
        if (!(OnGridValueChanged is null))
        {
            OnGridValueChanged(this, new OnGridValueChangedEventArgs { x = x, y = y });
        }

        return true;
    }

    public string GetLocationString(Vector3 worldPosition)
    {
        var (x, y) = GetXY(worldPosition);

        if (!IsInGrid(x, y))
        {
            return "Not in grid!";
        }

        return $"[{x}, {y}]";
    }

    private (int, int) GetXY(Vector3 worldPosition)
    {
        var x = Mathf.FloorToInt((worldPosition - OriginPosition).x / CellSize);
        var y = Mathf.FloorToInt((worldPosition - OriginPosition).y / CellSize);
        return (x, y);
    }

    private Vector3 GetWorldPosition(int x, int y)
        => (new Vector3(x, y) * CellSize) + OriginPosition;

    private bool IsInGrid(int x, int y)
        => (x >= 0 && y >= 0 && x < Width && y < Height);
}
