using Client;
using MazeLib;
using System;

namespace WPFClient.ViewModels
{
    /// <summary>
    /// Class RoomViewModel.
    /// </summary>
    public abstract class RoomViewModel : ViewModel
    {
        /// <summary>
        /// Model
        /// </summary>
        protected Player spM;      
                
        public event EventHandler CommErrorFailed;

        /// <summary>
        /// Gets or sets the maze.
        /// </summary>
        /// <value>The maze.</value>
        public Maze Maze { get; set; }

        /// <summary>
        /// Gets a value indicating whether there is no communication with server
        /// </summary>
        /// <value><c>true</c> if [comm error]; otherwise, <c>false</c>.</value>
        public bool CommError { get; private set; }

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="p">The p.</param>
        protected RoomViewModel(Player p)
        {
            this.spM = p;
            this.spM.InitFlow();
            this.spM.CommunicationError += SpM_CommunicationError;
            this.CommError = false;
        }

        /// <summary>
        /// Handles the CommunicationError event that was invoked from the model.
        /// Notifies view that there is no connection with server.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void SpM_CommunicationError(object sender, EventArgs e)
        {
            CommError = true;
            CommErrorFailed?.Invoke(this, EventArgs.Empty);
            NotifyPropertyChanged("CommError");
        }

    }
}
