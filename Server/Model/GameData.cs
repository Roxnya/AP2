using MazeLib;
using SearchAlgorithmsLib;
using Server.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    /// <summary>
    /// Container Class for relevant game data such as rooms, maze list, solutions, etc.
    /// </summary>
    class GameData
    {
        //private Dictionary<string, Maze> mazes;
       // private Dictionary<Maze, Solution<Position>> solutions;
        private Dictionary<string, IGameRoom> rooms;

        /// <summary>
        /// Ctor. Inits containers.
        /// </summary>
        public GameData()
        {
            mazes = new Dictionary<string, Maze>();
            solutions = new Dictionary<Maze, SolutionDetails>();
            rooms = new Dictionary<string, IGameRoom>();
        }

        public Dictionary<string, Maze> mazes { get; }

        public Dictionary<Maze, SolutionDetails> solutions
        {
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns>returns list of room names that are waiting for another player</returns>
        public List<string> GetJoinableRooms()
        {
            return rooms.Values.Where(r => r.Mode == Mode.WaitingForPlayer)
                                                .Select(r => r.Name).ToList();
        }

        /// <summary>
        /// Adds given room to game's room list
        /// </summary>
        /// <param name="room">room to add</param>
        public void AddGame(IGameRoom room)
        {
            rooms.Add(room.Name, room);
        }

        /// <summary>
        /// Adds given maze to maze list
        /// </summary>
        /// <param name="maze">maze to add</param>
        public void AddMaze(Maze maze)
        {
            mazes.Add(maze.Name, maze);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="maze">maze to find</param>
        /// <returns>If given maze name exists, returns a maze with that name. Otherwise, returns null.</returns>
        public Maze GetMaze(string maze)
        {
            return mazes.ContainsKey(maze) ? mazes[maze] : null;
        }
    }
}
