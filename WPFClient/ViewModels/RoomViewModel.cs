using Client;
using MazeLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFClient.ViewModels
{
    public abstract class RoomViewModel : ViewModel
    {
        protected Player spM;

        protected RoomViewModel(Player p)
        {
            this.spM = p;
            this.spM.InitFlow();
        }

        public int Rows { get; set; }
        public int Columns { get; set; }
        public Maze Maze { get; set; }
        public string Name { get; set; }
    }
}
