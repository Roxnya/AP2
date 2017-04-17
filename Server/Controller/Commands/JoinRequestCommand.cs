using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

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
            List<string> list = model.GetJoinableGamesList();
            return new Result(JsonConvert.SerializeObject(list), Status.Close);

        }
    }
}
