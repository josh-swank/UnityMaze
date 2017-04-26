using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Util.Maze
{
    public class Maze
    {

        private readonly bool[] _hWalls, _vWalls;

        public Maze(int width, int height)
        {
            Width = width;
            Height = height;

            _hWalls = new bool[Width * (Height + 1)];
            _vWalls = new bool[(Width + 1) * Height];
        }

        public int Width { get; private set; }
        public int Height { get; private set; }
        public MazeCell this[int x, int y] { get { return new Cell(this, x, y); } }

        public MazeWall[] GetWalls(bool isHorizontal)
        {
            IList<MazeWall> wallList = new List<MazeWall>();
            GetWalls(isHorizontal, wallList);
            return wallList.ToArray();
        }

        public MazeWall[] GetWalls()
        {
            IList<MazeWall> wallList = new List<MazeWall>();
            GetWalls(true, wallList);
            GetWalls(false, wallList);
            return wallList.ToArray();
        }

        public void SetWalls(bool isHorizontal, bool wall)
        {
            var walls = Walls(isHorizontal);
            for (var i = 0; i < walls.Length; ++i)
                walls[i] = wall;
        }

        public void SetWalls(bool wall)
        {
            SetWalls(true, wall);
            SetWalls(false, wall);
        }

        private bool[] Walls(bool isHorizontal)
        {
            return isHorizontal ? _hWalls : _vWalls;
        }

        private MazeWall CreateWall(bool isHorizontal, int major, int minor, int size)
        {
            if (isHorizontal)
                return new MazeWall(true, minor - size, major, size);
            else
                return new MazeWall(false, major, minor - size, size);
        }

        private void GetWalls(bool isHorizontal, IList<MazeWall> wallList)
        {
            var walls = Walls(isHorizontal);
            var major = isHorizontal ? Height : Width;
            var minor = isHorizontal ? Width : Height;
            for (var i = 0; i < major + 1; ++i)
            {
                var wallSize = 0;
                for (var j = 0; j < minor; ++j)
                {
                    if (walls[i * minor + j])
                        ++wallSize;
                    else if (wallSize > 0)
                    {
                        wallList.Add(CreateWall(isHorizontal, i, j, wallSize));
                        wallSize = 0;
                    }
                }
                wallList.Add(CreateWall(isHorizontal, i, minor, wallSize));
            }
        }

        private class Cell : MazeCell
        {
            private Maze _maze;

            public Cell(Maze maze, int x, int y) : base(x, y)
            {
                _maze = maze;
            }

            public override bool GetWall(Dir dir)
            {
                switch(dir)
                {
                    case Dir.Left:
                        return _maze._vWalls[X * _maze.Height + Y];
                    case Dir.Right:
                        return _maze._vWalls[(X + 1) * _maze.Height + Y];
                    case Dir.Up:
                        return _maze._hWalls[Y * _maze.Width + X];
                    case Dir.Down:
                        return _maze._hWalls[(Y + 1) * _maze.Width + X];
                    default:
                        throw new ArgumentException();
                }
            }

            public override void SetWall(Dir dir, bool wall)
            {
                switch(dir)
                {
                    case Dir.Left:
                        _maze._vWalls[X * _maze.Height + Y] = wall;
                        break;
                    case Dir.Right:
                        _maze._vWalls[(X + 1) * _maze.Height + Y] = wall;
                        break;
                    case Dir.Up:
                        _maze._hWalls[Y * _maze.Width + X] = wall;
                        break;
                    case Dir.Down:
                        _maze._hWalls[(Y + 1) * _maze.Width + X] = wall;
                        break;
                    default:
                        throw new ArgumentException();
                }
            }

            protected override MazeCell GetCell(int x, int y)
            {
                if (x >= 0 && x < _maze.Width && y >= 0 && y < _maze.Height)
                    return new Cell(_maze, x, y);
                else
                    return null;
            }
        }
    }
}
