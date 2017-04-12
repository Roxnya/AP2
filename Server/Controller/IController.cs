using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    interface IController : IObserver
    {
        string ExecuteCommand(string commandLine, TcpClient client);
        void Finish(TcpClient client);
        void SetModel(IModel model);
    }
}
