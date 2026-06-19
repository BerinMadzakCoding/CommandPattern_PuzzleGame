using UnityEngine;

public class GridManager : MonoBehaviour
{
    [Header("Level Layout")]
    [SerializeField] private MapLayout[] layouts;

    [Header("Prefabs")]
    [SerializeField] private GameObject floorPrefab;
    [SerializeField] private GameObject obstaclePrefab;
    [SerializeField] private GameObject startPrefab;
    [SerializeField] private GameObject endPrefab;

    [Header("Settings")]
    [SerializeField] private float cellSize = 1f;
    [SerializeField] private float surfaceHeight = 3f;

    public int Width { get; private set; }
    public int Height { get; private set; }

    private CellType[,] grid;
    private Vector2Int startCoord;
    private Vector2Int endCoord;

    public Vector2Int StartCoord => startCoord;
    public Vector2Int EndCoord => endCoord;

    private void Awake()
    {
        GenerateMap();
    }

    public void GenerateMap()
    {
        ParseLayout();
        BuildVisuals();
    }

    private void ParseLayout()
    {
        string[] layout = layouts[Random.Range(0, layouts.Length)].layout;
        Height = layout.Length;
        Width = layout[0].Length;
        grid = new CellType[Width, Height];

        for (int row = 0; row < Height; row++)
        {
            string line = layout[row];
            int y = Height - 1 - row;

            for (int x = 0; x < Width; x++)
            {
                CellType type = CharToCellType(line[x]);
                grid[x, y] = type;

                if (type == CellType.Start) startCoord = new Vector2Int(x, y);
                if (type == CellType.End) endCoord = new Vector2Int(x, y);
            }
        }
    }

    private CellType CharToCellType(char c)
    {
        switch (c)
        {
            case '#': return CellType.Obstacle;
            case 'S': return CellType.Start;
            case 'E': return CellType.End;
            default: return CellType.Empty;
        }
    }

    private void BuildVisuals()
    {
        for (int y = 0; y < Height; y++)
        {
            for (int x = 0; x < Width; x++)
            {
                Vector2Int coord = new Vector2Int(x, y);
                CellType type = grid[x, y];

                switch (type)
                {
                    case CellType.Obstacle:
                        Instantiate(floorPrefab, GridToWorld(coord), Quaternion.identity, transform);
                        Instantiate(obstaclePrefab, GetSurfaceWorld(coord), Quaternion.identity, transform);
                        break;

                    case CellType.Start:
                        Instantiate(startPrefab, GridToWorld(coord), Quaternion.identity, transform);
                        break;

                    case CellType.End:
                        Instantiate(endPrefab, GridToWorld(coord), Quaternion.identity, transform);
                        break;

                    default:
                        Instantiate(floorPrefab, GridToWorld(coord), Quaternion.identity, transform);
                        break;
                }
            }
        }
    }

    public Vector3 GridToWorld(Vector2Int coord)
    {
        return new Vector3(coord.x * cellSize, 0f, coord.y * cellSize);
    }

    public Vector3 GetSurfaceWorld(Vector2Int coord)
    {
        return new Vector3(coord.x * cellSize, surfaceHeight, coord.y * cellSize);
    }

    public bool IsInBounds(Vector2Int coord)
    {
        return coord.x >= 0 && coord.x < Width && coord.y >= 0 && coord.y < Height;
    }

    public bool IsObstacle(Vector2Int coord)
    {
        return grid[coord.x, coord.y] == CellType.Obstacle;
    }

    public bool IsBlocked(Vector2Int coord)
    {
        return !IsInBounds(coord) || IsObstacle(coord);
    }
}