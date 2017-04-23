using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    /// <summary>
    /// ICommand interface.
    /// </summary>
    interface ICommand
    {
        /// <summary>
        /// Executes command.
        /// </summary>
        /// <param name="args">user input</param>
        /// <param name="client">user</param>
        /// <returns>result of requested command</returns>
        Result Execute(string[] args, TcpClient client = null);
    }
}
