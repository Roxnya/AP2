using MazeLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFClient
{
    /// <summary>
    /// Class DirectionEventArgs.
    /// </summary>
    /// <seealso cref="System.EventArgs" />
    public class DirectionEventArgs : EventArgs
    {
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="direction">The direction.</param>
        public DirectionEventArgs(Direction direction)
        {
            this.Direction = direction;
        }

        /// <summary>
        /// Gets or sets the direction.
        /// </summary>
        /// <value>The direction.</value>
        public Direction Direction { get; set; }
    }
}
