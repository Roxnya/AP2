using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Model
{
    /// <summary>
    /// IObserver interface
    /// </summary>
    interface IObserver
    {
        /// <summary>
        /// Updating about the event happened
        /// </summary>
        /// <param name="sender">sender of the update</param>
        /// <param name="args">args</param>
        void Update(Object sender, ResultEventArgs args);
    }
}