using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server.View
{
    interface IClientHandler
    {
        void HandleClient(IController controller);

        void SendResponseToClient(Result result);
    }
}