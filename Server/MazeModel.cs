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
        private Dictionary<Maze, Solution<string>> solutions;
        private Dictionary<string,GameRoom> rooms;

        public MazeModel()
        {
            mazes = new Dictionary<string, Maze>();
            solutions = new Dictionary<Maze, Solution<string>>();
            rooms = new Dictionary<string, GameRoom>();
        }

        public Maze GenerateMaze(string name, int rows, int cols)
        {
            Maze maze = new DFSMazeGenerator().Generate(rows, cols);
            maze.Name = name;
            mazes.Add(name, maze);
            return maze;
        }

        //public Solution<string> SolveMaze(string name, Algorithm alg)
        //{

        //}

        public Maze OpenRoom(string name, int rows, int cols)
        {
            //check that maze name is unique...
            Maze m = GenerateMaze(name, rows, cols);
            rooms.Add(m.Name, new GameRoom(m));
            //
            //ToDo:wait for another player
            //
            return m;
        }

        public List<string> GetJoinableGamesList()
        {
            return rooms.Values.Where(r => r.mode == Mode.WaitingForPlayer)
                                                .Select(r => r.maze.Name).ToList();
        }




    }

    public enum Algorithm
    {
        BFS = 0,
        DFS = 1
    }
}
