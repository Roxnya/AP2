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

namespace Server.Model
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
            Maze maze = CreateMaze(name, rows, cols);
            gameData.AddSinglePlayerRoom(new SinglePlayerGameRoom(maze));
            return maze;
        }

        public SolutionDetails Solve(string name, Algorithm alg)
        {
            if (gameData.ContainsSingleGame(name))
            {
                Maze m = gameData.GetSinglePlayertRoom(name).Maze;
                SolutionDetails sd = gameData.GetSinglePlayertSolution(m.Name);
                if (sd != null)
                {
                    return sd;
                }

                MazeAdapter ma = new MazeAdapter(m);
                if (alg == Algorithm.BFS)
                {
                    ISearcher<Position> bfs = new BFS<Position>();
                    Solution<Position> sol = bfs.Search(ma);
                    sd = new SolutionDetails(name, sol);
                    gameData.AddSinglePlayerSolution(m.Name, sd);

                }
                if (alg == Algorithm.DFS)
                {
                    ISearcher<Position> dfs = new DFS<Position>();
                    Solution<Position> sol = dfs.Search(ma);
                    sd = new SolutionDetails(name, sol);
                    gameData.AddSinglePlayerSolution(m.Name, sd);

                }
                return sd;

            }
            return new SolutionDetails("", new Solution<Position>(0));

        }

        public bool OpenRoom(string name, int rows, int cols)
        {
            if (gameData.ContainsMultGame(name)) return false;
            Maze m = CreateMaze(name, rows, cols);
            Player host = new Player();
            IMultiPlayerGameRoom room = new GameRoom(m, host);

            controller.SetPlayer(host);
            controller.SetGame(room);
            gameData.AddGame(room);
            room.Notify += controller.Update;
            return true;
        }

        public bool Join(string name)
        {
            if (!gameData.ContainsMultGame(name)) return false;

            IMultiPlayerGameRoom room = gameData.GetMultiPlayerRoom(name);
            Player player2 = new Player();
            controller.SetPlayer(player2);
            controller.SetGame(room);
            room.Notify += controller.Update;
            //test if join successeded
            room.Join(player2);
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

        public void Quit(string name)
        {
            if (this.gameData.ContainsMultGame(name))
            {
                this.gameData.GetMultiPlayerRoom(name).Quit();
                this.gameData.RemoveMultiplayerRoom(name);
            }
        }

        private Maze CreateMaze(string name, int rows, int cols)
        {
            Maze m = new DFSMazeGenerator().Generate(rows, cols);
            m.Name = name;
            return m;
        }
    }

    public enum Algorithm
    {
        BFS = 0,
        DFS = 1
    }
}
