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
        IModel model; 

        public TurnPerformedCommand(IModel model)
        {
            this.model = model;
        }
        public Result Execute(string[] args, TcpClient client = null)
        {

            string direction = args[0];
            model.Move(direction, client);
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
