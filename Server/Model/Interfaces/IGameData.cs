using MazeLib;
using SearchAlgorithmsLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server.Commands;
using Server.Model;
using System.Net.Sockets;

namespace Server.Model
{
    /// <summary>
    /// Interface for Game Data. GameData is a container for game's containers.
    /// </summary>
    interface IGameData
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns>returns list of room names that are waiting for another player</returns>
        List<string> GetJoinableRooms();

        /// <summary>
        /// Adds given room to game's room list
        /// </summary>
        /// <param name="room">room to add</param>
        void AddGame(IMultiPlayerGameRoom room);

        /// <summary>
        /// Adds given solution to game data
        /// </summary>
        /// <param name="name">the name of maze that was solved</param>
        /// <param name="sol">solution to add</param>
        void AddSinglePlayerSolution(string name, SolutionDetails sol);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name">the maze for which we want the solution</param>
        /// <returns>If given maze has a solution, returns it's solution. Otherwise, returns null.</returns>
        SolutionDetails GetSinglePlayertSolution(string name);

        /// <summary>
        /// Adds given maze to maze list
        /// </summary>
        /// <param name="room">room to add</param>
        void AddSinglePlayerRoom(ISinglePlayerGameRoom room);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name">game name to find</param>
        /// <returns>true if exists, false otherwise</returns>
        bool ContainsMultGame(string name);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name">game name to find</param>
        /// <returns>true if exists, false otherwise</returns>
        bool ContainsSingleGame(string name);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name">room to find</param>
        /// <returns>If given room name exists, returns a room with that name. Otherwise, returns null.</returns>
        ISinglePlayerGameRoom GetSinglePlayertRoom(string name);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="name">name of the room to return</param>
        /// <returns>returns a room with given name if it exists. null otherwise</returns>
        IMultiPlayerGameRoom GetMultiPlayerRoom(string name);

        /// <summary>
        /// Removes given game's name from list
        /// </summary>
        /// <param name="name">name of the room to remove</param>
        void RemoveMultiplayerRoom(string name);

    }
}