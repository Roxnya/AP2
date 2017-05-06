using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFClient.ViewModels
{
    class SinglePlayerViewModel : ViewModel
    {
        public MazeLib.Maze Maze { get; private set; }

        public SinglePlayerViewModel()
        {

        }

        public void StartNewGame(string name, int rows, int cols)
        { 
        }
    }
}
