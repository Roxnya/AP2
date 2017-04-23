using Server.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    /// <summary>
    /// ClientHandlerFactory.
    /// </summary>
    class ClientHandlerFactory
    {
        /// <summary>
        /// GenerateClientHandler
        /// </summary>
        /// <param name="client">client to handle</param>
        /// <returns>client handler</returns>
        public static IClientHandler GenerateClientHandler(TcpClient client)
        {
            return new ClientHandler(client);
        }
    }
}
