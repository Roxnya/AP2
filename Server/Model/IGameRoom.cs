using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeLib;
namespace Server
{
    /// <summary>
    /// Interface for game rooms.
    /// If for example we will have a room with more than 2 players we won't need any adjustments in the model layer.
    /// Rooms need to allow subscribtion to them for updates, thus it inherits IObservable.
    /// </summary>
    interface IGameRoom : IObservable
    {
        string Name { get; }
        Maze Maze { get; }
        Mode Mode { get; }
        Player player2 { get; set; }
        

        /// <summary>
        /// Allows a player to join the room if it has an open place.
        /// </summary>
        /// <param name="player2">The player that wants to join game</param>
        void Join(Player player2);

        string Move(Player player, string Direction);
    }
}
