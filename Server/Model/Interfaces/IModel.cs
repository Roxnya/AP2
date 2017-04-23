using MazeLib;
using System;
using System.Collections.Generic;
using SearchAlgorithmsLib;
using System.Net.Sockets;
using Server.Model;

namespace Server.Model
{
    /// <summary>
    /// IModel interface.
    /// </summary>
    interface IModel
    {
        /// <summary>
        /// Generating the Maze
        /// </summary>
        /// <param name="name">maze name</param>
        /// <param name="rows">maze rows</param>
        /// <param name="cols">maze cols</param>
        /// <returns>maze thet was just generated</returns>
        Maze GenerateMaze(string name, int rows, int cols);

        /// <summary>
        /// Solving the maze
        /// </summary>
        /// <param name="name">maze's anme</param>
        /// <param name="alg">algotithm to solve the maze</param>
        /// <returns>Solution of the maze with details</returns>
        SolutionDetails Solve(string name, Algorithm alg);

        /// <summary>
        /// Get the list of the joinable games
        /// </summary>
        /// <returns>list</returns>
        List<string> GetJoinableGamesList();

        /// <summary>
        /// Get the path of the maze solution as a string
        /// </summary>
        /// <param name="sol"></param>
        /// <returns>path as string</returns>
        string GetPathAsString(Solution<Position> sol);

        /// <summary>
        /// Open the new game room(starting a new multiplayer game)
        /// </summary>
        /// <param name="name">maze's game</param>
        /// <param name="rows">rows</param>
        /// <param name="cols">cols</param>
        /// <returns>true if the opening succeed , false otherwise</returns>
        bool OpenRoom(string name, int rows, int cols);

        /// <summary>
        /// Joining the game room
        /// </summary>
        /// <param name="name">game name to join</param>
        /// <returns>true if succeed , false otherwise</returns>
        bool Join(string name);

        /// <summary>
        /// Quiting the game
        /// </summary>
        /// <param name="name">gmae name to quit</param>
        void Quit(string name);
    }
}
