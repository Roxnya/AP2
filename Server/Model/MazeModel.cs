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
        IGameData gameData;

        public MazeModel(IController controller, IGameData gameData)
        {
            this.controller = controller;
            this.gameData = gameData;
        }

        public Maze GenerateMaze(string name, int rows, int cols)
        {
            if (gameData.ContainsSingleGame(name)) return null;
            Maze maze = new DFSMazeGenerator().Generate(rows, cols);
            maze.Name = name;
            gameData.AddSinglePlayerMaze(maze);
            return maze;
        }

        public SolutionDetails Solve(string name, Algorithm alg)
        {
            if (gameData.ContainsSingleGame(name))
            {
                Maze m = gameData.GetSinglePlayertMaze(name);
                SolutionDetails sd = gameData.GetSinglePlayertSolution(m);
                if (sd != null)
                {
                    return sd;
                }

                MazeAdapter ma = new MazeAdapter(m);
                if (alg == Algorithm.BFS)
                {
                    ISearcher<Position> bfs = new BFS<Position>();
                    Solution<Position> sol = bfs.Search(ma);
                    sd = new SolutionDetails(name, bfs.GetNumberOfNodesEvaluated(), sol);
                    gameData.AddSinglePlayerSolution(m, sd);

                }
                if (alg == Algorithm.DFS)
                {
                    ISearcher<Position> dfs = new DFS<Position>();
                    Solution<Position> sol = dfs.Search(ma);
                    sd = new SolutionDetails(name, dfs.GetNumberOfNodesEvaluated(), sol);
                    gameData.AddSinglePlayerSolution(m, sd);                 

                }
                return sd;

            }
            return new SolutionDetails("", 0, new Solution<Position>());
            
        }


        public bool OpenRoom(string name, int rows, int cols, TcpClient host)
        {
            if (gameData.ContainsMultGame(name)) return false;
            Maze m = new DFSMazeGenerator().Generate(rows, cols);
            IGameRoom room = new GameRoom(m, host);
            gameData.AddGame(room);
            room.Notify += controller.Update;
            return true;
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
