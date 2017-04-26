using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Util.Rand;

namespace Util.Maze.Strategies
{
    public class RecursiveBacktrackingBuildStrategy : IMazeBuildStrategy
    {

        public Maze BuildMaze(int width, int height, IRandom random)
        {
            var maze = new Maze(width, height);
            maze.SetWalls(true);

            var cellStack = new Stack<MazeCell>();
            var cell = maze[random.NextInt32(width), random.NextInt32(height)];
            cellStack.Push(cell);

            // TODO

            return maze;
        }
    }
}
