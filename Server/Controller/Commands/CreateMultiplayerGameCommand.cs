using MazeLib;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Server.Commands
{
    class CreateMultiplayerGameCommand : ICommand
    {
        IModel model;

        public CreateMultiplayerGameCommand(IModel model)
        {
            this.model = model;
        }

        public Result Execute(string[] args, TcpClient client = null)
        {
            string name = args[0];
            int rows = int.Parse(args[1]);
            int cols = int.Parse(args[2]);
            bool result =  model.OpenRoom(name, rows, cols, client);
            if (result)
            {
                return new Result("", Status.Communicating);
            }
            return new Result(JsonConvert.SerializeObject("Game name already exists"), Status.Close);
        }
    }
}
