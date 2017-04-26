using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Util.Rand;

namespace Util.Maze.Strategies
{
    public interface IMazeBuildStrategy
    {
        Maze BuildMaze(int width, int height, IRandom random);
    }
}
