using Newtonsoft.Json;
using Server.Model;
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
            string name = args[0];
            bool result = model.Join(name);
            if (result)
            {
                return new Result("", Status.ReadOnly);
            }
            return new Result(JsonConvert.SerializeObject("failed to join game"), Status.Close);


        }
    }
}