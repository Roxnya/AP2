using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    interface IClientHandler
    {
        void HandleClient(TcpClient client, IController controller);

        void SendResponseToClient(TcpClient client, Result result);
    }
}
