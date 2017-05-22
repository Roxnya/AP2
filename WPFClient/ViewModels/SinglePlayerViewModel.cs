using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFClient.Models;
using Client;
using System.Windows;
using MazeLib;
using SearchAlgorithmsLib;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WPFClient.ViewModels
{
    public class SinglePlayerViewModel : RoomViewModel
    {
        private ISettingsModel sm;
        
        public string Solution { get; private set; }
        public event EventHandler SolutionChangedEvent;

        public SinglePlayerViewModel(ISettingsModel sm) : base(new Player(sm.ServerPort, sm.ServerIP, false))
        {
            this.sm = sm;
            this.Rows = sm.MazeRows;
            this.Columns = sm.MazeCols;
        }

        public void StartNewGame()
        {
            spM.MazeChanged += MazeChanged;
            spM.InjectCommand(CommandsFactory.GetGenerateCommand(Name, Rows, Columns));
        }

        public void RequestSolution()
        {
            if(Maze != null)
            {
                spM.SolutionChanged += SolutionChanged;
                spM.InjectCommand(CommandsFactory.GetSolveCommand(Maze.Name, sm.SearchAlgorithm));
            }
        }

        private void MazeChanged(MazeEventArgs e)
        {
            this.Maze = e.Maze;
            NotifyPropertyChanged("Maze");
            spM.MazeChanged -= MazeChanged;
        }

        private void SolutionChanged(SolutionEventArgs e)
        {
            if (Maze != null && e.MazeName == Maze.Name)
            {
                this.Solution = e.Solution;
                SolutionChangedEvent?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
