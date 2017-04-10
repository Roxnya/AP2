using Server.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class Controller
    {
        private Dictionary<string, ICommand> commands;
        private IModel model;
        private ICommand lastCommand;

        public Controller()
        {
            model = new MazeModel();
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

        public string ExecuteCommand(string commandLine, System.Net.Sockets.TcpClient client)
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

        public void Finish(System.Net.Sockets.TcpClient client)
        {
            lastCommand.Finish(client);
            lastCommand = null;
        }
    }
}
