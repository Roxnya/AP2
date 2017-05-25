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
    /// <summary>
    /// Class SinglePlayerViewModel.
    /// </summary>
    /// <seealso cref="WPFClient.ViewModels.RoomViewModel" />
    public class SinglePlayerViewModel : RoomViewModel
    {
        /// <summary>
        /// Settings Model
        /// </summary>
        private ISettingsModel sm;

        /// <summary>
        /// Gets maze solution.
        /// </summary>
        public string Solution { get; private set; }
        public event EventHandler SolutionChangedEvent;

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="sm">Setting model.</param>
        /// <param name="name">maze name</param>
        /// <param name="rows">maze rows</param>
        /// <param name="cols">maze cols.</param>
        public SinglePlayerViewModel(ISettingsModel sm, string name, string rows, string cols) : base(new Player(sm.ServerPort, sm.ServerIP, false))
        {
            this.sm = sm;
            StartNewGame(name, rows, cols);
        }

        /// <summary>
        /// Sends new game request and registers to maze changed event.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="rows">The rows.</param>
        /// <param name="cols">The cols.</param>
        public void StartNewGame(string name, string rows, string cols)
        {
            spM.MazeChanged += MazeChanged;
            spM.InjectCommand(CommandsFactory.GetGenerateCommand(name, int.Parse(rows), int.Parse(cols)));
        }

        /// <summary>
        /// Requests maze's solution.
        /// </summary>
        public void RequestSolution()
        {
            if(Maze != null)
            {
                spM.SolutionChanged += SolutionChanged;
                spM.InjectCommand(CommandsFactory.GetSolveCommand(Maze.Name, sm.SearchAlgorithm));
            }
        }

        /// <summary>
        /// Mazes the changed handler. Notifies maze changed.
        /// </summary>
        /// <param name="e">Event args - contains maze</param>
        private void MazeChanged(MazeEventArgs e)
        {
            this.Maze = e.Maze;
            NotifyPropertyChanged("Maze");
            spM.MazeChanged -= MazeChanged;
        }

        /// <summary>
        /// Solutions changed handler. If Solution's maze name is the same as this maze name, sets solution and notifies it changed.
        /// </summary>
        /// <param name="e">Event args containing solution and maze name</param>
        private void SolutionChanged(SolutionEventArgs e)
        {
            spM.SolutionChanged -= SolutionChanged;
            if (Maze != null && e.MazeName == Maze.Name)
            {
                this.Solution = e.Solution;
                SolutionChangedEvent?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
