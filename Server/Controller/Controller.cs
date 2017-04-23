using Newtonsoft.Json;
using Server.Commands;
using Server.Model;
using Server.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    /// <summary>
    /// Controller of the game
    /// </summary>
    class Controller : IController
    {
        private Dictionary<string, ICommand> commands;
        private IModel model;
        private ICommand lastCommand;
        private IClientHandler clientHandler;
        private IMultiPlayerGameRoom gameRoom;
        private Player player;

        /// <summary>
        /// Executed command from client.
        /// </summary>
        /// <param name="commandLine">user input</param>
        /// <param name="client">user</param>
        /// <returns>result of requested command</returns>
        public Status ExecuteCommand(string commandLine, TcpClient client)
        {
            string[] arr = commandLine.Split(' ');
            string commandKey = arr[0];

            //if there is no such command, send an error message to the client
            if (!commands.ContainsKey(commandKey))
            {
                clientHandler.SendResponseToClient(GetErrorResult());
                return Status.Close;
            }
            //execute the command
            string[] args = arr.Skip(1).ToArray();
            lastCommand = commands[commandKey];
            Result result = lastCommand.Execute(args, client);
            //send the response to the client
            if (result.Status == Status.Close)
            {
                clientHandler.SendResponseToClient(result);
            }
            return result.Status;
        }

        /// <summary>
        /// Set controller's game.
        /// </summary>
        /// <param name="room"></param>
        public void SetGame(IMultiPlayerGameRoom room)
        {
            this.gameRoom = room;
            //These commands should be available only if there is a room to play in
            commands.Add("play", new TurnPerformedCommand(this.gameRoom, this.player));
            commands.Add("close", new PlayerQuitMultGameCommand(model));
        }

        /// <summary>
        /// Set's controller's model
        /// </summary>
        /// <param name="model">model to set</param>
        public void SetModel(IModel model)
        {
            this.model = model;
            InitCommands();
        }

        /// <summary>
        /// Initialize commands.
        /// </summary>
        private void InitCommands()
        {
            commands = new Dictionary<string, ICommand>
            {
                { "generate", new GenerateMazeCommand(model) },
                { "solve", new SolveMazeCommand(model) },
                { "start", new CreateMultiplayerGameCommand(model) },
                { "list", new GetJoinableGamesCommand(model) },
                { "join", new JoinRequestCommand(model) }
            };
        }

        /// <summary>
        /// Sets controller's player.
        /// </summary>
        /// <param name="player"></param>
        public void SetPlayer(Player player)
        {
            this.player = player;
            this.player.Notify += Update;
        }

        /// <summary>
        /// Sets controller's view
        /// </summary>
        /// <param name="view">view to set</param>
        public void SetView(IClientHandler view)
        {
            this.clientHandler = view;
        }

        /// <summary>
        /// updating the client that the some action happend
        /// </summary>
        /// <param name="sender">sender of the update</param>
        /// <param name="e">result to send</param>
        public void Update(object sender, ResultEventArgs e)
        {
            if (e == null) return;
            clientHandler.SendResponseToClient(e.Result);
        }

        /// <summary>
        /// retturn the error result with closed status
        /// </summary>
        /// <returns>error result</returns>
        private Result GetErrorResult()
        {
            return new Result("Command not found", Status.Close);
        }
    }
}