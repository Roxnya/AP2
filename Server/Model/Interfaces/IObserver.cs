using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Model
{
    interface IObserver
    {
        void Update(Object sender, ResultEventArgs args);
    }
}