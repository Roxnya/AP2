using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{

    delegate void EventHandler<EventArgs>(object sender, EventArgs e);
    interface IObservable
    {
        event EventHandler<EventArgs> Notify;
    }
}
