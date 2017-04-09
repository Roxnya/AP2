using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server.Commands
{
    class SolveMazeCommand : ICommand
    {
        IModel model;

        public SolveMazeCommand(IModel model)
        {
            this.model = model;
        }

        public string Execute(string[] args, TcpClient client = null)
        {
            string name = args[0];
            Algorithm algorithm = (Algorithm)int.Parse(args[1]);
            //Maze maze = model.SolveMaze(name, rows, cols);
            //return maze.ToJSON();
            return "";
        }

        public void Finish(TcpClient client)
        {
            //Shouldn't close communication with client
        }
    }
}
