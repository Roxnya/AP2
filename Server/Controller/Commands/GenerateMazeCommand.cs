using MazeLib;
using Server.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class GenerateMazeCommand : ICommand
    {
        private IModel model;

        public GenerateMazeCommand(IModel model)
        {
            this.model = model;
        }

        public Result Execute(string[] args, TcpClient client)
        {
            if(args.Count() != 3)
                throw new InvalidOperationException("Not enough arguemnts for generate command.");

            string name = args[0];
            int rows = int.Parse(args[1]);
            int cols = int.Parse(args[2]);

            Maze maze = model.GenerateMaze(name, rows, cols);
            return new Result(Serialize(maze), Status.Close);
        }

        private string Serialize(Maze maze)
        {
            return maze != null ? maze.ToJSON() : "Error. Game name already exists";
        }

    }
}
