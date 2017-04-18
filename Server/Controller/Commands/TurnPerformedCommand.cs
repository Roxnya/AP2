using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server.Commands
{
    /**
     * Relevant only for multiplayer game.
     **/
    class TurnPerformedCommand : ICommand
    {
        public Result Execute(string[] args, TcpClient client = null)
        {
            string move = args[0];
            return new Result("", Status.Communicating);
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
