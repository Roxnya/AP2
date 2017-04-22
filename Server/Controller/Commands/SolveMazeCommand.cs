using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using MazeLib;
using SearchAlgorithmsLib;
using Newtonsoft.Json.Linq;
using Server.Model;

namespace Server.Commands
{
    /// <summary>
    /// Solve maze command class
    /// </summary>
    class SolveMazeCommand : ICommand
    {
        IModel model;

        /// <summary>
        /// Constructor for SolveMazeCommand.
        /// </summary>
        /// <param name="model">model</param>
        public SolveMazeCommand(IModel model)
        {
            this.model = model;
        }

        /// <summary>
        /// Executes solve command.
        /// </summary>
        /// <param name="args">user input</param>
        /// <param name="client">user</param>
        /// <returns>result of requested command</returns>
        public Result Execute(string[] args, TcpClient client = null)
        {
            if (args.Count() != 2)
                throw new InvalidOperationException("Not enough arguemnts for generate command.");
            string name = args[0];
            //get the algotithm to solve the maze with
            Algorithm algorithm = (Algorithm)Enum.Parse(typeof(Algorithm), args[1]);
            if (!Enum.IsDefined(typeof(Algorithm), algorithm) && !algorithm.ToString().Contains(","))
                throw new InvalidOperationException("Invalid algorithm. Algorithm type is not defined.");
            //get the solution
            SolutionDetails sol = model.Solve(name, algorithm);

            string result = string.Empty;
            if (sol != null)
            {
                //convert the solution to the needed form
                result = model.GetPathAsString(sol.solution);
            }

            return new Result(ToJSON(result, sol), Status.Close);

        }

        /// <summary>
        /// Converts the solution to Json format.
        /// </summary>
        /// <param name="result">solution path as stirng</param>
        /// <param name="sd">solution details</param>
        /// <returns>the result in the Json format</returns>
        private string ToJSON(string result, SolutionDetails sd)
        {
            JObject mazeObj = new JObject();
            mazeObj["path"] = result;
            mazeObj["name"] = sd.Name;
            mazeObj["NodesEvaluated"] = sd.NodesEvaluated;
            return mazeObj.ToString();
        }
    }
}
