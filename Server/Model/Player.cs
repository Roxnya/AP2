using MazeLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Model
{
    /// <summary>
    /// Player class. Implements observable interface
    /// </summary>
    class Player : IObservable
    {
        /// <summary>
        /// Position get and set
        /// </summary>
        public Position position { get; set; }

        /// <summary>
        /// Notify event
        /// </summary>
        public event EventHandler<EventArgs> Notify;

        /// <summary>
        /// Notifying about the move
        /// </summary>
        /// <param name="json">result to send</param>
        public void CounterMove(string json)
        {
            Notify(this, new ResultEventArgs(new Result(json, Status.Communicating)));
        }
    }
}
