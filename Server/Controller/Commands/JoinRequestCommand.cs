using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using MazeLib;
namespace Server.Commands
{
    class JoinRequestCommand : ICommand
    {
        private IModel model;

        public JoinRequestCommand(IModel model)
        {
            this.model = model;
        }

        public Result Execute(string[] args, TcpClient client = null)
        {
            string name = args[0];
            Maze result = model.Join(name);
            if (result!=null)
            {
                return new Result(result.ToJSON(), Status.Close);
            }
            return new Result(JsonConvert.SerializeObject("Game name already exists"), Status.Close);
            
        }
    }
}