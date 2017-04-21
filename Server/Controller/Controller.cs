using Newtonsoft.Json;
using Server.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class Controller : IController
    {
        private Dictionary<string, ICommand> commands;
        private IModel model;
        private ICommand lastCommand;
        private IClientHandler clientHandler;
        private IGameRoom gameRoom;
        private Player player;
        private TcpClient client;

        
        public Status ExecuteCommand(string commandLine, TcpClient client)
        {
            this.client = client;
            string[] arr = commandLine.Split(' ');
            string commandKey = arr[0];

            //if there is no such command
            if (!commands.ContainsKey(commandKey))
            {
                clientHandler.SendResponseToClient(client, GetErrorResult());
                return Status.Close;
            }

            string[] args = arr.Skip(1).ToArray();
            lastCommand = commands[commandKey];
            Result result = lastCommand.Execute(args, client);
            if (result.Status == Status.Close)
            {
                clientHandler.SendResponseToClient(client, result);
            }
            return result.Status;
        }

        public void SetGame(IGameRoom room)
        {
            this.gameRoom = room;
            //These commands should be available only if there is a room to play in
            commands.Add("play", new TurnPerformedCommand(this.gameRoom, this.player));
            commands.Add("close", new PlayerQuitMultGameCommand());
        }

        public void SetModel(IModel model)
        {
            this.model = model;
            //all the commands are available only if there's a specific model
            commands = new Dictionary<string, ICommand>
            {
                { "generate", new GenerateMazeCommand(model) },
                { "solve", new SolveMazeCommand(model) },
                { "start", new CreateMultiplayerGameCommand(model) },
                { "list", new GetJoinableGamesCommand(model) },
                { "join", new JoinRequestCommand(model) },
                
            };
        }

        public void SetPlayer(Player player)
        {
            this.player = player;
        }

        public void SetView(IClientHandler view)
        {
            this.clientHandler = view;
        }

        public void Update(object sender, ResultEventArgs e)
        {
            if (e == null) return;
            clientHandler.SendResponseToClient(this.client, e.Result);
        }
        //!!!!!
        public void MoveUpdate(Player p, Result result)
        {
            if(this.player == p)
            {
                clientHandler.SendResponseToClient(this.client, result);

            }
        }

        private Result GetErrorResult()
        {
            return new Result("Command not found", Status.Close);
        }
    }
}