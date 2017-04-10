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
        public string Execute(string[] args, TcpClient client = null)
        {
            throw new NotImplementedException();
        }

        public void Finish(TcpClient client)
        {
            throw new NotImplementedException();
        }
    }
}
