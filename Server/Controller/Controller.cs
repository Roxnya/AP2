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
        private IClientHandler client;

        public Controller()
        {
           
        }
        
        
        public string ExecuteCommand(string commandLine, TcpClient client)
        {
            string[] arr = commandLine.Split(' ');
            string commandKey = arr[0];

            if (!commands.ContainsKey(commandKey))
                return "Command not found";

            string[] args = arr.Skip(1).ToArray();
            lastCommand = commands[commandKey];
            string result = lastCommand.Execute(args, client);
            return result;
        }

        public void Finish(TcpClient client)
        {
            lastCommand.Finish(client);
            lastCommand = null;
        }

        public void SetModel(IModel model)
        {
            this.model = model;
            commands = new Dictionary<string, ICommand>
            {
                { "generate", new GenerateMazeCommand(model) },
                { "solve", new SolveMazeCommand(model) },
                { "start", new CreateMultiplayerGameCommand(model) },
                { "list", new GetJoinableGamesCommand(model) },
                { "join", new JoinRequestCommand() },
                { "play", new TurnPerformedCommand() },
                { "close", new PlayerQuitMultGameCommand() }
            };
        }

        public void Update(object sender, EventArgs e)
        {
            //cast event args to something that contains maze
            //return maze.ToJson();
        }
    }
}
