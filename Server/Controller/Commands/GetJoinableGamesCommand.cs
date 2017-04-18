using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server.Commands
{
    class GetJoinableGamesCommand : ICommand
    {
        IModel model;

        public GetJoinableGamesCommand(IModel model)
        {
            this.model = model;
        }

        public Result Execute(string[] args, TcpClient client = null)
        {
            List<string> rooms = model.GetJoinableGamesList();
            return new Result(JsonConvert.SerializeObject(rooms), Status.Close);
        }
    }
}