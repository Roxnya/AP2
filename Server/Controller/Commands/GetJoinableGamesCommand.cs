using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
    /// Get Joinable Games  list Command class
    /// </summary>
    class GetJoinableGamesCommand : ICommand
    {
        IModel model;

        /// <summary>
        /// Constructor for GetJoinableGamesCommand.
        /// </summary>
        /// <param name="model">model</param>
        public GetJoinableGamesCommand(IModel model)
        {
            this.model = model;
        }

        /// <summary>
        /// Executes list command.
        /// </summary>
        /// <param name="args">user input</param>
        /// <param name="client">user</param>
        /// <returns>result of requested command</returns>
        public Result Execute(string[] args, TcpClient client = null)
        {
            if (args.Count() != 0)
                throw new InvalidOperationException("Not enough arguemnts for generate command.");

            List<string> rooms = model.GetJoinableGamesList();
            return new Result(JArray.FromObject(rooms).ToString(), Status.Close);
        }
    }
}