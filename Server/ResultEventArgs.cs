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
        public ResultEventArgs(Result result)
        {
            this.Result = result;
        }

        public Result Result { get; private set; }
    }
}
