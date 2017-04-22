using Server.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server.Commands
{
    /// <summary>
    /// Quit command class
    /// </summary>
    class PlayerQuitMultGameCommand : ICommand
    {
        private IModel model;

        /// <summary>
        /// Constructor for PlayerQuitMultGameCommand.
        /// </summary>
        /// <param name="model">model</param>
        public PlayerQuitMultGameCommand(IModel model)
        {
            this.model = model;
        }

        /// <summary>
        /// Executes quit command.
        /// </summary>
        /// <param name="args">user input</param>
        /// <param name="client">user</param>
        /// <returns>result of requested command</returns>
        public Result Execute(string[] args, TcpClient client = null)
        {
            if (args.Count() != 1)
                throw new InvalidOperationException("Not enough arguemnts for generate command.");

            model.Quit(args[0]);
            return new Result("close", Status.ReadOnly);
        }
    }
}
