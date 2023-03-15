public class MapGridObject
{
    public enum GridTileType
    {
        Empty,
        Mine,
        Indicator_1,
        Indicator_2,
        Indicator_3,
        Indicator_4,
        Indicator_5,
        Indicator_6,
        Indicator_7,
        Indicator_8,
    }

    private readonly CustomGrid<MapGridObject> _grid;
    private readonly int _x;
    private readonly int _y;
    private GridTileType _tileType;
    
    public MapGridObject(CustomGrid<MapGridObject> grid, int x, int y)
    {
        _grid = grid;
        _x = x;
        _y = y;

        _tileType = GridTileType.Empty;
    }
}
