using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server.View
{
    /// <summary>
    /// IClientHandler interface
    /// </summary>
    interface IClientHandler
    {
        /// <summary>
        /// Handle the client
        /// </summary>
        /// <param name="controller"></param>
        void HandleClient(IController controller);

        /// <summary>
        /// Send response to the client
        /// </summary>
        /// <param name="result"></param>
        void SendResponseToClient(Result result);
    }
}