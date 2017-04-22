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
    /// 
    /// </summary>
    class SinglePlayerGameRoom : ISinglePlayerGameRoom
    {
        public string Name { get; private set; }
        public SolutionDetails Solution { get; private set; }
        public Maze Maze { get; private set; }

        /// <summary>
        /// Ctor.
        /// </summary>
        public SinglePlayerGameRoom(Maze maze)
        {
            this.Maze = maze;
            this.Name = maze.Name;
        }

        public void AddSolution(SolutionDetails solution)
        {
            this.Solution = solution; 
        }
    }
}
