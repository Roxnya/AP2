using MazeLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public class EnemyMovedEventArgs : EventArgs
    {
        public EnemyMovedEventArgs(Direction dir, string name)
        {
            this.Direction = dir;
            this.Name = name;
        }

        public Direction Direction { get; private set; }
        public string Name { get; private set; }
    }
}
