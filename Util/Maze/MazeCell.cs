using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Util.Maze
{
    public abstract class MazeCell
    {

        private readonly Dir[] _dirs = (Dir[])Enum.GetValues(typeof(Dir));

        protected MazeCell(int x, int y)
        {
            X = x; Y = y;
        }

        public int X { get; }
        public int Y { get; }

        public bool IsIsolated
        {
            get
            {
                foreach (var dir in _dirs)
                    if (!GetWall(dir))
                        return false;
                return true;
            }
        }

        public abstract bool GetWall(Dir dir);
        public abstract void SetWall(Dir dir, bool wall);

        public MazeCell GetCell(Dir dir)
        {
            switch(dir)
            {
                case Dir.Left:
                    return GetCell(X - 1, Y);
                case Dir.Right:
                    return GetCell(X + 1, Y);
                case Dir.Up:
                    return GetCell(X, Y - 1);
                case Dir.Down:
                    return GetCell(X, Y + 1);
                default:
                    throw new ArgumentException();
            }
        }

        protected abstract MazeCell GetCell(int x, int y);

        public enum Dir { Left, Right, Up, Down }
    }
}
