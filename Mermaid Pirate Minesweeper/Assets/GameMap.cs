using UnityEngine;

public class GameMap
{
    private readonly CustomGrid<MapGridObject> _grid;

    public GameMap()
    {
        _grid = new CustomGrid<MapGridObject>(
            10,
            10,
            10.0f,
            new Vector3(-5, -5),
            (CustomGrid<MapGridObject> g, int x, int y) => new MapGridObject(g, x, y));
    }
}
