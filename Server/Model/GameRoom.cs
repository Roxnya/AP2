using MazeLib;
using SearchAlgorithmsLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class GameRoom : IGameRoom
    {
        public Maze maze;
        public Mode Mode { get; private set; }
        public Solution<string> solution;
        public Position host;
        public Position Player2;

        public event EventHandler<EventArgs> Notify;

        public string Name { get { return maze.Name; } }

        public GameRoom(Maze maze)
        {
            this.maze = maze;
            Mode = Mode.WaitingForPlayer;
        }

        public void Join()
        {
            this.Mode = Mode.InProgrss;
            //init position
            Notify?.Invoke(this, EventArgs.Empty);
        }
    }

    internal enum Mode
    {
        WaitingForPlayer,
        InProgrss
    }
}
