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
    /// <summary>
    /// Maze model , implementing the IModel interface
    /// </summary>
    class MazeModel : IModel
    {
        IController controller;
        IGameData gameData;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="controller">Controller</param>
        /// <param name="gameData">gamedata</param>
        public MazeModel(IController controller, IGameData gameData)
        {
            this.controller = controller;
            this.gameData = gameData;
        }

        /// <summary>
        /// Generate the maze.
        /// </summary>
        /// <param name="name">maze name</param>
        /// <param name="rows">maze rows</param>
        /// <param name="cols">maze cols</param>
        /// <returns>generated maze</returns>
        public Maze GenerateMaze(string name, int rows, int cols)
        {
            //if theres already maze with this name , return null
            if (gameData.ContainsSingleGame(name)) return null;
            //create the maze
            Maze maze = CreateMaze(name, rows, cols);
            gameData.AddSinglePlayerRoom(new SinglePlayerGameRoom(maze));
            return maze;
        }

        /// <summary>
        /// Solve the maze
        /// </summary>
        /// <param name="name">maze name to solve</param>
        /// <param name="alg">algorithm to solve the maze with</param>
        /// <returns>detailed solution</returns>
        public SolutionDetails Solve(string name, Algorithm alg)
        {
            if (gameData.ContainsSingleGame(name))
            {
                //get the maze with this name and solve it
                Maze m = gameData.GetSinglePlayertRoom(name).Maze;
                SolutionDetails sd = gameData.GetSinglePlayertSolution(m.Name);
                if (sd != null)
                {
                    return sd;
                }

                MazeAdapter ma = new MazeAdapter(m);
                ISearcher<Position> searcher = SearcherFactory.GetSearcher(alg);
                sd = new SolutionDetails(name, searcher.Search(ma));
                gameData.AddSinglePlayerSolution(m.Name, sd);

                return sd;

            }
            throw new InvalidOperationException("game name doesn't exist");

        }

        /// <summary>
        /// Open the game room for the new game
        /// </summary>
        /// <param name="name">the name of the game</param>
        /// <param name="rows">maze rows</param>
        /// <param name="cols">maze cols</param>
        /// <returns></returns>
        public bool OpenRoom(string name, int rows, int cols)
        {
            if (gameData.ContainsMultGame(name)) return false;
            //create the maze
            Maze m = CreateMaze(name, rows, cols);
            //create the player
            Player host = new Player();
            //create the gam room
            IMultiPlayerGameRoom room = new GameRoom(m, host);

            //provide the controller with the needed info
            controller.SetPlayer(host);
            controller.SetGame(room);
            gameData.AddGame(room);
            //set the notify function
            room.Notify += controller.Update;
            return true;
        }

        /// <summary>
        /// join the gameroom
        /// </summary>
        /// <param name="name">name of the game to join</param>
        /// <returns>true if succeed, false otherwise</returns>
        public bool Join(string name)
        {
            //if there's no sucjh game return false
            if (!gameData.ContainsMultGame(name)) return false;

            //find the room
            IMultiPlayerGameRoom room = gameData.GetMultiPlayerRoom(name);
            //create new player
            Player player2 = new Player();
            //provide the controller wihth the neede info
            controller.SetPlayer(player2);
            controller.SetGame(room);
            //set the notify function
            room.Notify += controller.Update;
            //test if join successeded
            if (!room.Join(player2))
            {
                room.Notify -= controller.Update;
                return false;
            }
            return true;
        }

        /// <summary>
        /// GetJoinableGamesList
        /// </summary>
        /// <returns>get the list of the games to join</returns>
        public List<string> GetJoinableGamesList()
        {
            return gameData.GetJoinableRooms();
        }

        /// <summary>
        /// convert solution to the path string
        /// </summary>
        /// <param name="sol"></param>
        /// <returns></returns>
        public string GetPathAsString(Solution<Position> sol)
        {
            string result = Model.PathDetails.ConvertSolutionToString(sol);
            return result;
        }

        /// <summary>
        /// quit the game 
        /// </summary>
        /// <param name="name">game name to quit</param>
        public void Quit(string name)
        {
            if (this.gameData.ContainsMultGame(name))
            {
                this.gameData.GetMultiPlayerRoom(name).Quit();
                this.gameData.RemoveMultiplayerRoom(name);
            }
        }

        /// <summary>
        /// create a maze
        /// </summary>
        /// <param name="name">maze name</param>
        /// <param name="rows">maze rows</param>
        /// <param name="cols">maze cols</param>
        /// <returns></returns>
        private Maze CreateMaze(string name, int rows, int cols)
        {
            //create the maze with DFSMazeGenerator
            Maze m = new DFSMazeGenerator().Generate(rows, cols);
            m.Name = name;
            return m;
        }
    }

    /// <summary>
    /// Algorithm enum, BFS and DFS
    /// </summary>
    public enum Algorithm
    {
        BFS = 0,
        DFS = 1
    }
}
