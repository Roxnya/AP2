using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Model
{
    /// <summary>
    /// Interface for MultiPlayerGameRoom
    /// Rooms need to allow subscribtion to them for updates, thus it inherits IObservable.
    /// </summary>
    interface IMultiPlayerGameRoom : IGameRoom, IObservable
    {
        Mode Mode { get; }

        /// <summary>
        /// Allows a player to join the room if it has an open place.
        /// </summary>
        /// <param name="player2">The player that wants to join game</param>
        ///<returns>true if playered was added successfully to the room. false otherwise.</returns>
        bool Join(Player player2);

        /// <summary>
        /// Moves player in game
        /// </summary>
        /// <param name="player">player that wants to move</param>
        /// <param name="Direction">direction in which the player wants to move</param>
        void Move(Player player, string Direction);

        /// <summary>
        /// Notifies room that given player is quitting
        /// </summary>
        void Quit();
    }
}
