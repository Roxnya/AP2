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
        public Result Execute(string[] args, TcpClient client = null)
        {
            throw new NotImplementedException();
        }
    }
}
