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
        Status ExecuteCommand(string commandLine, TcpClient client);

        /// <summary>
        /// Set's controller's view
        /// </summary>
        /// <param name="view">view to set</param>
        void SetView(IClientHandler view);

        /// <summary>
        /// Set's controller's model
        /// </summary>
        /// <param name="model">model to set</param>
        void SetModel(IModel model);
        void SetPlayer(Player player);
        void SetGame(IGameRoom room);
    }
}
