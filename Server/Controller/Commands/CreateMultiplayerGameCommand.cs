using MazeLib;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Server.Model;

namespace Server.Commands
{
    /// <summary>
    /// Start a multiplayer game command class.
    /// </summary>
    class CreateMultiplayerGameCommand : ICommand
    {
        IModel model;

        /// <summary>
        /// Constructor for CreateMultiplayerGameCommand.
        /// </summary>
        /// <param name="model">model</param>
        public CreateMultiplayerGameCommand(IModel model)
        {
            this.model = model;
        }

        /// <summary>
        /// Executes start command.
        /// </summary>
        /// <param name="args">user input</param>
        /// <param name="client">user</param>
        /// <returns>result of requested command</returns>
        public Result Execute(string[] args, TcpClient client = null)
        {
            if (args.Count() != 3)
                throw new InvalidOperationException("error: Not enough arguemnts for generate command.");

            string name = args[0];
            int rows = int.Parse(args[1]);
            int cols = int.Parse(args[2]);
            bool result =  model.OpenRoom(name, rows, cols);
            if (result)
            {
                return new Result("", Status.Communicating);
            }
            return new Result(JsonConvert.SerializeObject("Game name already exists"), Status.Close);
        }
    }
}
