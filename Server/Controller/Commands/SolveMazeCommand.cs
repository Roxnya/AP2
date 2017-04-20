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
    class SolveMazeCommand : ICommand
    {
        IModel model;

        public SolveMazeCommand(IModel model)
        {
            this.model = model;
        }

        public Result Execute(string[] args, TcpClient client = null)
        {
            string name = args[0];
            Algorithm algorithm = (Algorithm)int.Parse(args[1]);
            SolutionDetails sol = model.Solve(name, algorithm);

            //convert the solution to the needed form
            string result = model.GetPathAsString(sol.solution);

            return new Result(ToJSON(result, sol), Status.Close);

        }

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
