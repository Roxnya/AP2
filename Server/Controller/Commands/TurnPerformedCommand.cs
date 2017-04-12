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
        public string Execute(string[] args, TcpClient client = null)
        {
            string move = args[0];
            throw new NotImplementedException();
        }

        public void Finish(TcpClient client)
        {
            //doesn't need to close connection
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
