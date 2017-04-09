using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeLib;
using MazeGeneratorLib;
using SearchAlgorithmsLib;

namespace Server
{
    class MazeModel : IModel
    {
        private Dictionary<string, Maze> mazes;
        private Dictionary<Solution<string>, Maze> solutions;
        private List<GameRoom> rooms;

        public Maze GenerateMaze(string name, int rows, int cols)
        {
            Maze maze = new DFSMazeGenerator().Generate(rows, cols);
            maze.Name = name;
            mazes.Add(maze.Name, maze);
            return maze;
        }

        //public Solution<string> SolveMaze(string name, Algorithm alg)
        //{

        //}

        public Maze OpenRoom(string name, int rows, int cols)
        {
            Maze m = GenerateMaze(name, rows, cols);
            rooms.Add(new GameRoom(m));
            //
            //ToDo:wait for another player
            //
            return m;
        }

        public List<string> GetJoinableGamesList()
        {
            return rooms.Where(r => r.mode == Mode.WaitingForPlayer)
                                                .Select(r => r.maze.Name).ToList();
        }




    }

    public enum Algorithm
    {
        BFS = 0,
        DFS = 1
    }
}
