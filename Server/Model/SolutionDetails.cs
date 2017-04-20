using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SearchAlgorithmsLib;
using MazeLib;

namespace Server.Model
{
    class SolutionDetails
    {
        public SolutionDetails(string name, Solution<Position> solution)
        {
            this.solution = solution;
            this.NodesEvaluated = solution.NodesEvaluated;
            this.Name = name;
        }
        public Solution<Position> solution;
        public int NodesEvaluated { get; private set; }
        public string Name { get; set; }


    }
}
