using MazeLib;
using Newtonsoft.Json.Linq;
using SearchAlgorithmsLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server.Model
{
    /// <summary>
    /// Class that represents game room for 2 players.
    /// </summary>
    class GameRoom : IMultiPlayerGameRoom
    {
        public Maze Maze { get; private set; }
        public Mode Mode { get; private set; }
        private Player host;
        public Player player2 { get; set; }

        private readonly object joinLocker = new object();

        //event through which listeners will be notified of relevant room events such as game started, move was made, etc.
        public event EventHandler<EventArgs> Notify;

        public string Name { get { return Maze.Name; } }

        /// <summary>
        /// Ctor. Initializes Game Room with game's maze and room's mode.
        /// </summary>
        /// <param name="maze"></param>
        public GameRoom(Maze maze, Player host)
        {
            this.Maze = maze;
            this.host = host;
            Mode = Mode.WaitingForPlayer;
        }

        /// <summary>
        /// Allows a player to join the room if it has an open place.
        /// </summary>
        /// <param name="player2">The player that wants to join game</param>
        public bool Join(Player player2)
        {
            lock (joinLocker)
            {
                //if room already reached players capacity return
                if (Mode != Mode.WaitingForPlayer) return false;
                this.Mode = Mode.InProgress;
                this.player2 = player2;
            }
            //init position
            Result res = new Result(Maze.ToJSON(), Status.Communicating);
            Notify?.Invoke(this, new ResultEventArgs(res));
            return true;

        }

        public void Move(Player player, string direction)
        {
            Player toUpdate;
            toUpdate = (host.Equals(player) ? player2 : host);            
            MakeAMove(direction, player);
            toUpdate.CounterMove(ToJson(direction));
        }

        private string ToJson(string direction)
        {
            JObject mazeObj = new JObject();
            mazeObj["name"] = this.Maze.Name;
            mazeObj["direction"] = direction;
            return mazeObj.ToString();
        }

        private void MakeAMove(string direction, Player toMove)
        {
            if (direction.Equals("left"))
            {
                if (this.Maze.Cols >= toMove.position.Col - 1)
                {
                    toMove.position = new Position(toMove.position.Row, toMove.position.Col - 1);

                }
            }
            if (direction.Equals("right"))
            {
                if (this.Maze.Cols >= toMove.position.Col + 1)
                {
                    toMove.position = new Position(toMove.position.Row, toMove.position.Col + 1);

                }
            }
            if (direction.Equals("up"))
            {
                if (this.Maze.Rows >= toMove.position.Row + 1)
                {
                    toMove.position = new Position(toMove.position.Row + 1, toMove.position.Col);

                }
            }
            if (direction.Equals("down"))
            {
                if (this.Maze.Rows >= toMove.position.Row - 1)
                {
                    toMove.position = new Position(toMove.position.Row - 1, toMove.position.Col);

                }
            }
        }

        public void Quit()
        {
            Result result = new Result("close", Status.Close);
            Notify(this, new ResultEventArgs(result));
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