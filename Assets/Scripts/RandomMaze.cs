using UnityEngine;
using Util.Maze;
using Util.Maze.Strategies;
using Util.Rand;

public class RandomMaze : MonoBehaviour
{

    private static readonly IMazeBuildStrategy _mazeBuildStrategy
        = new RecursiveBacktrackingBuildStrategy();

    [SerializeField]
    private int _width = 3;
    [SerializeField]
    private int _length = 3;
    [SerializeField]
    private GameObject _floorPrefab;
    [SerializeField]
    private GameObject _wallPrefab;
    [SerializeField]
    private bool _useRandomSeed = true;
    [SerializeField]
    private ulong _seed;

    private IRandom _random;
    private Maze _maze;

    public int Width { get { return _width; } }
    public int Length { get { return _length; } }
    public ulong Seed { get { return _random.Seed.Value; } }

    private void OnValidate()
    {
        _width = Mathf.Max(_width, 0);
        _length = Mathf.Max(_length, 0);
    }

    private void Start()
    {
        if (_useRandomSeed)
            _seed = new FclRandom().NextUInt64();
        _random = new FclRandom(_seed);

        _maze = _mazeBuildStrategy.BuildMaze(_width, _length, _random);

        AddFloor();

        var walls = _maze.GetWalls();
        foreach (var wall in walls)
            AddWall(wall);
    }

    private void AddFloor()
    {
        var floorObj = Instantiate(_floorPrefab);
        floorObj.name = "Floor";
        floorObj.transform.SetParent(transform, true);

        var floorScale = floorObj.transform.localScale;
        floorScale.x *= Width;
        floorScale.z *= Length;
        floorObj.transform.localScale = floorScale;
    }

    private void AddWall(MazeWall wall)
    {
        var wallObj = Instantiate(_wallPrefab);
        wallObj.name = "Wall";
        wallObj.transform.SetParent(transform, true);

        var wallPos = wallObj.transform.position;
        if (wall.IsHorizontal)
        {
            wallPos.x = wall.StartX - (Width - wall.Size) / 2.0f;
            wallPos.z = wall.StartY - Length / 2.0f;
        }
        else
        {
            wallPos.x = wall.StartX - Width / 2.0f;
            wallPos.z = wall.StartY - (Length - wall.Size) / 2.0f;

            var wallRot = wallObj.transform.eulerAngles;
            wallRot.y = 90.0f;
            wallObj.transform.eulerAngles = wallRot;
        }
        wallObj.transform.position = wallPos;

        var wallScale = wallObj.transform.localScale;
        wallScale.x *= wall.Size;
        wallObj.transform.localScale = wallScale;
    }
}
