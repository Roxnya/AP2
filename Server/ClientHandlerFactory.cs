using Server.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class ClientHandlerFactory
    {
        public static IClientHandler GenerateClientHandler(TcpClient client)
        {
            return new ClientHandler(client);
        }
    }
}
