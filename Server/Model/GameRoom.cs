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
        public Maze Maze { get; private set; }
        public Mode Mode { get; private set; }
        private Position host_pos;
        private Position player2_pos;
        private TcpClient host;
        private TcpClient player2;

        //event through which listeners will be notified of relevant room events such as game started, move was made, etc.
        public event EventHandler<EventArgs> Notify;

        public string Name { get { return Maze.Name; } }

        /// <summary>
        /// Ctor. Initializes Game Room with game's maze and room's mode.
        /// </summary>
        /// <param name="maze"></param>
        public GameRoom(Maze maze, TcpClient host)
        {
            this.Maze = maze;
            this.host = host;
            Mode = Mode.WaitingForPlayer;
        }

        /// <summary>
        /// Allows a player to join the room if it has an open place.
        /// </summary>
        /// <param name="player2">The player that wants to join game</param>
        public void Join(TcpClient player2)
        {
            //if room already reached players capacity return
            if (Mode != Mode.WaitingForPlayer) return;
            this.Mode = Mode.InProgress;
            this.player2 = player2;
            //init position
            Result res = new Result(Maze.ToJSON(), Status.Communicating);
            //Notify?.Invoke(this, new ResultEventArgs(res, host));
            Notify?.Invoke(this, new ResultEventArgs(res));

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