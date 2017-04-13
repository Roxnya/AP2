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
    /// <summary>
    /// Class that represents game room for 2 players.
    /// </summary>
    class GameRoom : IGameRoom
    {
        public Maze maze;
        public Mode Mode { get; private set; }
        public Solution<Position> solution;
        public Position host;
        public Position Player2;

        //event through which listeners will be notified of relevant room events such as game started, move was made, etc.
        public event EventHandler<EventArgs> Notify;

        public string Name { get { return maze.Name; } }

        /// <summary>
        /// Ctor. Initializes Game Room with game's maze and room's mode.
        /// </summary>
        /// <param name="maze"></param>
        public GameRoom(Maze maze)
        {
            this.maze = maze;
            Mode = Mode.WaitingForPlayer;
        }

        /// <summary>
        /// Allows a player to join the room if it has an open place.
        /// </summary>
        /// <param name="player2">The player that wants to join game</param>
        public void Join(TcpClient player2)
        {
            if (Mode != Mode.WaitingForPlayer) return;
            this.Mode = Mode.InProgress;
            //init position
            Notify?.Invoke(this, EventArgs.Empty);
        }
    }

    /// <summary>
    /// Represents room modes.
    /// </summary>
    internal enum Mode
    {
        WaitingForPlayer,
        InProgress
    }
}
