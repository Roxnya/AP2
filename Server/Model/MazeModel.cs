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
using Server.Model;

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
            if (gameData.mazes.ContainsKey(name))
            {
                return null;
            }
            Maze maze = new DFSMazeGenerator().Generate(rows, cols);
            maze.Name = name;
            gameData.AddMaze(maze);
            return maze;
        }

        public SolutionDetails Solve(string name, Algorithm alg)
        {
            if (gameData.mazes.ContainsKey(name))
            {
                Maze m = gameData.mazes[name];
                if (gameData.solutions.ContainsKey(m))
                {
                    return gameData.solutions[m];
                }
                MazeAdapter ma = new MazeAdapter(m);
                if(alg == Algorithm.BFS)
                {
                    ISearcher<Position> bfs = new BFS<Position>();
                    Solution<Position> sol = bfs.Search(ma);
                    SolutionDetails sd = new SolutionDetails(name, bfs.GetNumberOfNodesEvaluated(), sol);
                    gameData.solutions.Add(m, sd);

                    return sd;

                }
                if (alg == Algorithm.DFS)
                {
                    ISearcher<Position> dfs = new DFS<Position>();
                    Solution<Position> sol = dfs.Search(ma);
                    SolutionDetails sd = new SolutionDetails(name, dfs.GetNumberOfNodesEvaluated(), sol);
                    gameData.solutions.Add(m, sd);

                    return sd;
                 

                }
            }
            //think about that
            return new SolutionDetails("", 0, new Solution<Position>());
            
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
        
        public string GetPathAsString(Solution<Position> sol)
        {
            string result = Model.PathDetails.ConvertSolutionToString(sol);
            return result;
        }
    }

    public enum Algorithm
    {
        BFS = 0,
        DFS = 1
    }
}
