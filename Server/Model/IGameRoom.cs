using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        Mode Mode { get; }
    }
}
