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
    /**
     * Relevant only for multiplayer game.
     **/
    class TurnPerformedCommand : ICommand
    {
        private IMultiPlayerGameRoom gameRoom;
        private Player player;

        public TurnPerformedCommand(IMultiPlayerGameRoom gameRoom, Player player)
        {
            this.gameRoom = gameRoom;
            this.player = player;
        }
        public Result Execute(string[] args, TcpClient client = null)
        {
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
