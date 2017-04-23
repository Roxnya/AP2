using MazeLib;
using SearchAlgorithmsLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Model
{
    /// <summary>
    /// SinglePlayerGameRoom class.
    /// </summary>
    class SinglePlayerGameRoom : ISinglePlayerGameRoom
    {
        /// <summary>
        /// single player game room name get and set
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Soluting get and set
        /// </summary>
        public SolutionDetails Solution { get; private set; }

        /// <summary>
        /// Maze get and set
        /// </summary>
        public Maze Maze { get; private set; }

        /// <summary>
        /// Ctor.
        /// </summary>
        public SinglePlayerGameRoom(Maze maze)
        {
            this.Maze = maze;
            this.Name = maze.Name;
        }

        /// <summary>
        /// Adding the solution
        /// </summary>
        /// <param name="solution">solution</param>
        public void AddSolution(SolutionDetails solution)
        {
            this.Solution = solution; 
        }
    }
}
