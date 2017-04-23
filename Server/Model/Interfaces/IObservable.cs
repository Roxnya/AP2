using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Model
{
    /// <summary>
    /// Event handler
    /// </summary>
    /// <typeparam name="EventArgs">EventArgs</typeparam>
    /// <param name="sender">sender of the update</param>
    /// <param name="e">result</param>
    delegate void EventHandler<EventArgs>(object sender, ResultEventArgs e);

    /// <summary>
    /// IObservable interfase
    /// </summary>
    interface IObservable
    {
        /// <summary>
        /// Notifying the observable
        /// </summary>
        event EventHandler<EventArgs> Notify;
    }
}
