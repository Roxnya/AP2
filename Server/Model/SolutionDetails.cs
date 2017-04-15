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
        public SolutionDetails(string name, int nodesEvaluated, Solution<Position> solution)
        {
            this.solution = solution;
            this.name = name;
            this.nodesEvaluated = nodesEvaluated;
        }
        public Solution<Position> solution;
        public int nodesEvaluated { get; set; }
        public string name { get; set; }

        
    }
}
