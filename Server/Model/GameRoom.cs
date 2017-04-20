using MazeLib;
using Newtonsoft.Json.Linq;
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
        private Player host;
        public Player player2 { get; set; }

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
        public void Join(Player player2)
        {
            //if room already reached players capacity return
            if (Mode != Mode.WaitingForPlayer) return;
            this.Mode = Mode.InProgress;
            this.player2 = player2;
            //init position
            Result res = new Result(Maze.ToJSON(), Status.Communicating);
            Notify?.Invoke(this, new ResultEventArgs(res));

        }

        public string Move(Player player, string direction)
        {
            Player toMove, toUpdate;
            if (host == player)
            {
                toMove = host;
                toUpdate = player2;
            }
            else
            {
                toMove = player2;
                toUpdate = host;
            }
            //update the position
            
            MakeAMove(direction, toMove);
            string result = ToJson(direction);
            return result;
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
            if (direction == "left")
            {
                if (this.Maze.Cols >= toMove.position.Col - 1)
                {
                    toMove.position = new Position(toMove.position.Row, toMove.position.Col - 1);

                }
            }
            if (direction == "right")
            {
                if (this.Maze.Cols >= toMove.position.Col + 1)
                {
                    toMove.position = new Position(toMove.position.Row, toMove.position.Col + 1);

                }
            }
            if (direction == "up")
            {
                if (this.Maze.Rows >= toMove.position.Row + 1)
                {
                    toMove.position = new Position(toMove.position.Row + 1, toMove.position.Col);

                }
            }
            if (direction == "down")
            {
                if (this.Maze.Rows >= toMove.position.Row - 1)
                {
                    toMove.position = new Position(toMove.position.Row - 1, toMove.position.Col);

                }
            }
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