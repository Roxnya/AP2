using Newtonsoft.Json;
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
    /// Join Command class
    /// </summary>
    class JoinRequestCommand : ICommand
    {
        private IModel model;

        /// <summary>
        /// Constructor for JoinRequestCommand.
        /// </summary>
        /// <param name="model">model</param>
        public JoinRequestCommand(IModel model)
        {
            this.model = model;
        }

        /// <summary>
        /// Executes join command.
        /// </summary>
        /// <param name="args">user input</param>
        /// <param name="client">user</param>
        /// <returns>result of requested command</returns>
        public Result Execute(string[] args, TcpClient client = null)
        {
            string name = args[0];
            bool result = model.Join(name);
            if (result)
            {
                return new Result("", Status.ReadOnly);
            }
            return new Result(JsonConvert.SerializeObject("failed to join game"), Status.Close);


        }
    }
}