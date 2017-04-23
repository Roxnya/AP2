using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    /// <summary>
    /// ResultEventArgs class
    /// </summary>
    class ResultEventArgs : EventArgs
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="result">result</param>
        public ResultEventArgs(Result result)
        {
            this.Result = result;
        }

        /// <summary>
        /// result get and set
        /// </summary>
        public Result Result { get; private set; }
    }
}