using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class ResultEventArgs : EventArgs
    {
        public ResultEventArgs(Result result, TcpClient client)
        {
            this.Result = result;
            this.Client = client;
        }

        public Result Result { get; private set; }

        public TcpClient Client { get; private set; }
    }
}
