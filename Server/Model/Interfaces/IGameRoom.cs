using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Model
{
    /// <summary>
    /// Interface for game rooms.
    /// If for example we will have a room with more than 2 players we won't need any adjustments in the model layer.
    /// </summary>
    interface IGameRoom
    {
        /// <summary>
        /// get the name of the game
        /// </summary>
        string Name { get; }
       
    }
}
