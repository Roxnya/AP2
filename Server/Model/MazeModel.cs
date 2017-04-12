using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeLib;
using MazeGeneratorLib;
using SearchAlgorithmsLib;
using System.Net.Sockets;
using CompareSolvers;

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

        public Solution<Position> Solve(string name, Algorithm alg)
        {
            if (mazes.ContainsKey(name))
            {
                Maze m = mazes[name];
                if (solutions.ContainsKey(m))
                {
                    return solutions[m];
                }
                MazeAdapter ma = new MazeAdapter(m);
                if(alg == Algorithm.BFS)
                {
                    ISearcher<Position> bfs = new BFS<Position>();
                    Solution<Position> sol = bfs.Search(ma);
                    solutions.Add(m, sol);
                    return sol;

                }
                if (alg == Algorithm.DFS)
                {
                    ISearcher<Position> dfs = new DFS<Position>();
                    Solution<Position> sol = dfs.Search(ma);
                    solutions.Add(m, sol);
                    return sol;
                 

                }
            }
            
            return new Solution<Position>();
            
        }

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
