using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using MazeLib;
using Server.Model;

namespace Server.Commands
{
    /// <summary>
    /// Play command class
    /// </summary>
    class TurnPerformedCommand : ICommand
    {
        private IMultiPlayerGameRoom gameRoom;
        private Player player;

        /// <summary>
        /// Constructor for TurnPerformedCommand.
        /// </summary>
        /// <param name="gameRoom">relevant gameroom</param>
        /// <param name="player">player that performs a step</param>
        public TurnPerformedCommand(IMultiPlayerGameRoom gameRoom, Player player)
        {
            this.gameRoom = gameRoom;
            this.player = player;
        }

        /// <summary>
        /// Executes play command.
        /// </summary>
        /// <param name="args">user input</param>
        /// <param name="client">user</param>
        /// <returns>result of requested command</returns>
        public Result Execute(string[] args, TcpClient client = null)
        {
            if (args.Count() != 1)
                throw new InvalidOperationException("Not enough arguemnts for generate command.");

            string direction = args[0];
            gameRoom.Move(player, direction);
            return new Result("", Status.ReadOnly);
        }
    }

    enum Move
    {
        Up,
        Down,
        Left,
        Right
    }
}
