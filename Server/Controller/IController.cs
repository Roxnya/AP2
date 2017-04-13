using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    /// <summary>
    /// Interface for Controllers.
    /// Controllers need to be able to subscribe to relevant classes for updates.
    /// </summary>
    interface IController : IObserver
    {
        /// <summary>
        /// Executed command from client.
        /// </summary>
        /// <param name="commandLine">user input</param>
        /// <param name="client">user</param>
        /// <returns>result of requested command</returns>
        string ExecuteCommand(string commandLine, TcpClient client);

        /// <summary>
        /// Method that indicates view is done with client's request,
        /// thus the controller needs to terminate relevant data.
        /// </summary>
        /// <param name="client"></param>
        void Finish(TcpClient client);

        /// <summary>
        /// Set's controller's model
        /// </summary>
        /// <param name="model">model to set</param>
        void SetModel(IModel model);
    }
}
