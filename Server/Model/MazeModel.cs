using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeLib;
using MazeGeneratorLib;
using SearchAlgorithmsLib;
using System.Net.Sockets;

namespace Server
{
    class MazeModel : IModel
    {
        IController controller;
        GameData gameData;

        public MazeModel(IController controller, GameData gameData)
        {
            this.controller = controller;
            this.gameData = gameData;
        }

        public Maze GenerateMaze(string name, int rows, int cols)
        {
            Maze maze = new DFSMazeGenerator().Generate(rows, cols);
            maze.Name = name;
            gameData.AddMaze(maze);
            return maze;
        }

        //public Solution<string> SolveMaze(string name, Algorithm alg)
        //{

        //}

        public void OpenRoom(string name, int rows, int cols)
        {
            //check that maze name is unique...
            Maze m = GenerateMaze(name, rows, cols);
            IGameRoom room = new GameRoom(m);
            gameData.AddGame(room);
            room.Notify += controller.Update;
        }

        public List<string> GetJoinableGamesList()
        {
            return gameData.GetJoinableRooms();
        }
    }

    public enum Algorithm
    {
        BFS = 0,
        DFS = 1
    }
}
