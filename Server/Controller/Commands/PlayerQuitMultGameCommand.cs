using Server.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server.Commands
{
    class PlayerQuitMultGameCommand : ICommand
    {
        private IModel model;

        public PlayerQuitMultGameCommand(IModel model)
        {
            this.model = model;
        }

        public Result Execute(string[] args, TcpClient client = null)
        {
            model.Quit(args[0]);
            return new Result("close", Status.ReadOnly);
        }
    }
}
