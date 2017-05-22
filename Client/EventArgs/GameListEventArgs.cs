using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public class GameListEventArgs : EventArgs
    {
        public GameListEventArgs(List<string> games)
        {
            this.Games = games;
        }

        public List<string> Games { get; private set; }
    }
}
