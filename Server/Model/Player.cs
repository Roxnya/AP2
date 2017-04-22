using MazeLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Model
{
    class Player : IObservable
    {
        public Position position { get; set; }
        public event EventHandler<EventArgs> Notify;

        public void CounterMove(string json)
        {
            Notify(this, new ResultEventArgs(new Result(json, Status.Communicating)));
        }
    }
}
