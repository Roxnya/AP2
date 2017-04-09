using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server.Commands
{
    class CreateMultiplayerGameCommand : ICommand
    {
        public string Execute(string[] args, TcpClient client = null)
        {
            string name = args[0];
            int rows = int.Parse(args[1]);
            int cols = int.Parse(args[2]);
            Maze maze = model.OpenRoom(name, rows, cols);
            return maze.ToJSON();
        }

        public void Finish(TcpClient client)
        {
            
        }
    }
}
