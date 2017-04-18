using MazeLib;
using SearchAlgorithmsLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server.Commands;
using Server.Model;

namespace Server
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
        void AddGame(IGameRoom room);

        /// <summary>
        /// Adds given solution to game data
        /// </summary>
        /// <param name="m">the maze that was solved</param>
        /// <param name="sol">solution to add</param>
        void AddSinglePlayerSolution(Maze m, SolutionDetails sol);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="maze">the maze for which we want the solution</param>
        /// <returns>If given maze has a solution, returns it's solution. Otherwise, returns null.</returns>
        SolutionDetails GetSinglePlayertSolution(Maze maze);

        /// <summary>
        /// Adds given maze to maze list
        /// </summary>
        /// <param name="maze">maze to add</param>
        void AddSinglePlayerMaze(Maze maze);

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
        /// <param name="maze">maze to find</param>
        /// <returns>If given maze name exists, returns a maze with that name. Otherwise, returns null.</returns>
        Maze GetSinglePlayertMaze(string maze);
    }
}