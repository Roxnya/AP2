using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SearchAlgorithmsLib;
using MazeLib;

namespace Server.Model
{
    /// <summary>
    /// Solution details
    /// </summary>
    class SolutionDetails
    {
        /// <summary>
        /// constructor.
        /// </summary>
        /// <param name="name">maze name</param>
        /// <param name="solution">solution of the maze</param>
        public SolutionDetails(string name, Solution<Position> solution)
        {
            this.solution = solution;
            this.NodesEvaluated = solution.NodesEvaluated;
            this.Name = name;
        }

        public Solution<Position> solution;

        /// <summary>
        /// number of nodes evaluated get and set
        /// </summary>
        public int NodesEvaluated { get; private set; }

        /// <summary>
        /// MAze name get and set
        /// </summary>
        public string Name { get; set; }


    }
}
