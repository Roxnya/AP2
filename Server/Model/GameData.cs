using MazeLib;
using SearchAlgorithmsLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server.Model;
using System.Net.Sockets;

namespace Server
{
    /// <summary>
    /// Container Class for relevant game data such as rooms, maze list, solutions, etc.
    /// </summary>
    class GameData : IGameData
    {
        private Dictionary<string, Maze> singlePlayerMazeList;
        private Dictionary<Maze, SolutionDetails> singlePlayerSolutions;
        private Dictionary<string, IGameRoom> rooms;
        
        /// <summary>
        /// Ctor. Inits containers.
        /// </summary>
        public GameData()
        {
            singlePlayerMazeList = new Dictionary<string, Maze>();
            singlePlayerSolutions = new Dictionary<Maze, SolutionDetails>();
            rooms = new Dictionary<string, IGameRoom>();
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
        /// <returns>true if game was added, false otherwise</returns>
        public void AddGame(IGameRoom room)
        {
            rooms.Add(room.Name, room);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name">game name to find</param>
        /// <returns>true if exists, false otherwise</returns>
        public bool ContainsMultGame(string name)
        {
            return rooms.ContainsKey(name);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name">game name to find</param>
        /// <returns>true if exists, false otherwise</returns>
        public bool ContainsSingleGame(string name)
        {
            return singlePlayerMazeList.ContainsKey(name);
        }

        /// <summary>
        /// Adds given solution to game data
        /// </summary>
        /// <param name="m">the maze that was solved</param>
        /// <param name="sol">solution to add</param>
        public void AddSinglePlayerSolution(Maze m, SolutionDetails sol)
        {
            singlePlayerSolutions.Add(m, sol);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="maze">the maze for which we want the solution</param>
        /// <returns>If given maze has a solution, returns it's solution. Otherwise, returns null.</returns>
        public SolutionDetails GetSinglePlayertSolution(Maze maze)
        {
            return singlePlayerSolutions.ContainsKey(maze) ? singlePlayerSolutions[maze] : null;
        }

        /// <summary>
        /// Adds given maze to maze list
        /// </summary>
        /// <param name="maze">maze to add</param>
        /// <returns>true if maze was added, false otherwise</returns>
        public void AddSinglePlayerMaze(Maze maze)
        {
            singlePlayerMazeList.Add(maze.Name, maze);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="maze">maze to find</param>
        /// <returns>If given maze name exists, returns a maze with that name. Otherwise, returns null.</returns>
        public Maze GetSinglePlayertMaze(string maze)
        {
            return singlePlayerMazeList.ContainsKey(maze) ? singlePlayerMazeList[maze] : null;
        }

        public IGameRoom GetPlayerGame(TcpClient client)
        {
            foreach(KeyValuePair<string, IGameRoom> gm in rooms)
            {
                if(gm.Value.GetFirstPlayer() == client||gm.Value.GetSecondPlayer() == client)
                {
                    return gm.Value;
                  
                }
            }
            return null;
        }
        
            
    
    }
}