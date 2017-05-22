using MazeLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public class MazeEventArgs : EventArgs
    {
        public MazeEventArgs(Maze maze)
        {
            this.Maze = maze;
        }
        public Maze Maze { get; private  set; }
    }
}
