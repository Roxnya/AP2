using MazeLib;
using SearchAlgorithmsLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server.Model;
using System.Net.Sockets;

namespace Server.Model
{
    /// <summary>
    /// Container Class for relevant game data such as rooms, maze list, solutions, etc.
    /// </summary>
    class GameData : IGameData
    {
        private Dictionary<string, ISinglePlayerGameRoom> singlePlayerRoomsList;
        private Dictionary<string, IMultiPlayerGameRoom> rooms;

        /// <summary>
        /// Ctor. Inits containers.
        /// </summary>
        public GameData()
        {
            singlePlayerRoomsList = new Dictionary<string, ISinglePlayerGameRoom>();
            rooms = new Dictionary<string, IMultiPlayerGameRoom>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>returns list of room names that are waiting for another player</returns>
        public List<string> GetJoinableRooms()
        {
            lock (rooms)
            {
                return rooms.Values.Where(r => r.Mode == Mode.WaitingForPlayer)
                                                    .Select(r => r.Name).ToList();
            }
        }

        /// <summary>
        /// Adds given room to game's room list
        /// </summary>
        /// <param name="room">room to add</param>
        /// <returns>true if game was added, false otherwise</returns>
        public void AddGame(IMultiPlayerGameRoom room)
        {
            lock (rooms)
            {
                rooms.Add(room.Name, room);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name">game name to find</param>
        /// <returns>true if exists, false otherwise</returns>
        public bool ContainsMultGame(string name)
        {
            lock (rooms)
            {
                return rooms.ContainsKey(name);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name">game name to find</param>
        /// <returns>true if exists, false otherwise</returns>
        public bool ContainsSingleGame(string name)
        {
            lock (singlePlayerRoomsList)
            {
                return singlePlayerRoomsList.ContainsKey(name);
            }
        }

        /// <summary>
        /// Adds given solution to game data
        /// </summary>
        /// <param name="name">name of the maze that was solved</param>
        /// <param name="sol">solution to add</param>
        public void AddSinglePlayerSolution(string name, SolutionDetails sol)
        {
            lock (singlePlayerRoomsList)
            {
                singlePlayerRoomsList[name].AddSolution(sol);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="maze">the maze for which we want the solution</param>
        /// <returns>If given maze has a solution, returns it's solution. Otherwise, returns null.</returns>
        public SolutionDetails GetSinglePlayertSolution(string name)
        {
            lock (singlePlayerRoomsList)
            {
                return singlePlayerRoomsList.ContainsKey(name) ? singlePlayerRoomsList[name].Solution : null;
            }
        }

        /// <summary>
        /// Adds given room to single player rooms list
        /// </summary>
        /// <param name="room">room to add</param>
        public void AddSinglePlayerRoom(ISinglePlayerGameRoom room)
        {
            lock (singlePlayerRoomsList)
            {
                singlePlayerRoomsList.Add(room.Name, room);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="maze">maze to find</param>
        /// <returns>If given maze name exists, returns a maze with that name. Otherwise, returns null.</returns>
        public ISinglePlayerGameRoom GetSinglePlayertRoom(string maze)
        {
            lock (singlePlayerRoomsList)
            {
                return singlePlayerRoomsList.ContainsKey(maze) ? singlePlayerRoomsList[maze] : null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name">name of the room to return</param>
        /// <returns>returns a room with given name if it exists. null otherwise</returns>
        public IMultiPlayerGameRoom GetMultiPlayerRoom(string name)
        {
            lock (rooms)
            {
                return rooms.ContainsKey(name) ? rooms[name] : null;
            }
        }

        /// <summary>
        /// Removes given game's name from list
        /// </summary>
        /// <param name="name">name of the room to remove</param>
        public void RemoveMultiplayerRoom(string name)
        {
            if (ContainsMultGame(name))
            {
                lock(rooms)
                {
                    rooms.Remove(name);
                }
            }
        }
    }
}