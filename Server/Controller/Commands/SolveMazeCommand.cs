using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using MazeLib;
using SearchAlgorithmsLib;

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
            Solution<Position> sol = model.Solve(name, algorithm);
            //serialize the solution to a string 
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(sol);
            return new Result(json, Status.Close);
        }
    }
}
