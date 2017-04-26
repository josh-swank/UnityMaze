using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Util.Maze
{
    public class MazeWall
    {
        public MazeWall(bool isHorizontal, int startX, int startY, int size)
        {
            IsHorizontal = isHorizontal;
            StartX = startX;
            StartY = startY;
            Size = size;
        }

        public bool IsHorizontal { get; }
        public int StartX { get; }
        public int StartY { get; }
        public int Size { get; }
    }
}
