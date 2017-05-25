using Client;
using MazeLib;
using System;

namespace WPFClient.ViewModels
{
    public abstract class RoomViewModel : ViewModel
    {
        protected Player spM;
        public event EventHandler CommErrorFailed;

        public int Rows { get; set; }
        public int Columns { get; set; }
        public Maze Maze { get; set; }
        public string Name { get; set; }
        public bool CommError { get; private set; }

        protected RoomViewModel(Player p)
        {
            this.spM = p;
            this.spM.InitFlow();
            this.spM.CommunicationError += SpM_CommunicationError;
            this.CommError = false;
        }

        private void SpM_CommunicationError(object sender, EventArgs e)
        {
            CommError = true;
            CommErrorFailed?.Invoke(this, EventArgs.Empty);
            NotifyPropertyChanged("CommError");
        }

    }
}
