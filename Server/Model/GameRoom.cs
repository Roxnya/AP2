using MazeLib;
using SearchAlgorithmsLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class GameRoom
    {
        public Maze maze;
        public Mode mode;
        public Solution<string> solution;
        public Position Player1;
        public Position Player2;

        public GameRoom(Maze maze)
        {
            this.maze = maze;
            mode = Mode.WaitingForPlayer;
        }
    }

    internal enum Mode
    {
        WaitingForPlayer,
        InProgrss
    }
}
