using MazeLib;
using SearchAlgorithmsLib;

namespace Server.Model
{
    /// <summary>
    /// Class for single player rooms. Such rooms can contains a solution and a maze;
    /// </summary>
    internal interface ISinglePlayerGameRoom : IGameRoom
    {
        /// <summary>
        /// Get the solution
        /// </summary>
        SolutionDetails Solution { get; }

        /// <summary>
        /// get the maze
        /// </summary>
        Maze Maze { get; }

        /// <summary>
        /// Adds given solution of the maze to
        /// </summary>
        /// <param name="solution"></param>
        void AddSolution(SolutionDetails solution);
    }
}