using MazeLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFClient
{
    public class DirectionEventArgs : EventArgs
    {
        public DirectionEventArgs(Direction direction)
        {
            this.Direction = direction;
        }

        public Direction Direction { get; set; }
    }
}
