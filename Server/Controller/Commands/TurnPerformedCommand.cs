using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using MazeLib;
namespace Server.Commands
{
    /**
     * Relevant only for multiplayer game.
     **/
    class TurnPerformedCommand : ICommand
    {
        private IGameRoom gameRoom;
        private Player player;
        public TurnPerformedCommand(IGameRoom gameRoom, Player player)
        {
            this.gameRoom = gameRoom;
        }
        public Result Execute(string[] args, TcpClient client = null)
        {
            //make it in the maze model . Here noone has to know sth about position
            string direction = args[0];
            
            //update the second player
                
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
