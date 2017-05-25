using Client;
using MazeLib;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFClient.Models;

namespace WPFClient.ViewModels
{
    /// <summary>
    /// Class MultiPlayerViewModel.
    /// </summary>
    /// <seealso cref="WPFClient.ViewModels.RoomViewModel" />
    public class MultiPlayerViewModel : RoomViewModel
    {
        /// <summary>
        /// The sm
        /// </summary>
        private ISettingsModel sm;
        /// <summary>
        /// The name
        /// </summary>
        private string name;

        public event EventHandler EnemyMoved;
        public event EventHandler GameClosed;
        /// <summary>
        /// Gets the direction in which oponnent moved.
        /// </summary>
        public Direction EnemyDirection { get; private set; }

        /// <summary>
        /// ctor.
        /// </summary>
        /// <param name="sm">Setting model.</param>
        /// <param name="name">Maze name.</param>
        /// <param name="rows">Maze rows.</param>
        /// <param name="cols">Maze cols.</param>
        /// <param name="isHost">if set to <c>true</c> [is host].</param>
        public MultiPlayerViewModel(ISettingsModel sm, string name, string rows, string cols, bool isHost) : base(new Player(sm.ServerPort, sm.ServerIP, false))
        {
            this.sm = sm;
            this.name = name;
            //adjust request according to host
            if (isHost)
                RequestToOpenRoom(rows, cols);
            else
                RequestToJoinGame();
            this.spM.GameClosed += EnemyClosedGame;
        }

        /// <summary>
        /// Sends requests to join game.
        /// </summary>
        public void RequestToJoinGame()
        {
            this.spM.MazeChanged += GameStarted;
            spM.InjectCommand(CommandsFactory.GetJoinCommand(this.name));
        }

        /// <summary>
        /// Sends requests to open a new room.
        /// </summary>
        /// <param name="rows">Room's rows.</param>
        /// <param name="cols">Room's cols.</param>
        public void RequestToOpenRoom(string rows, string cols)
        {
            this.spM.MazeChanged += GameStarted;
            spM.InjectCommand(CommandsFactory.GetStartCommand(this.name, int.Parse(rows), int.Parse(cols)));
        }

        /// <summary>
        /// Closes the game.
        /// </summary>
        public void CloseGame()
        {
            spM.InjectCommand(CommandsFactory.GetCloseCommand(this.name));
        }

        /// <summary>
        /// Notifies server of movement.
        /// </summary>
        /// <param name="direction">The direction.</param>
        public void SendMoveCommand(Direction direction)
        {
            spM.InjectCommand(CommandsFactory.GetPlayCommand(direction));
        }

        /// <summary>
        /// Games the started.
        /// </summary>
        /// <param name="e">The <see cref="MazeEventArgs"/> instance containing the event data.</param>
        private void GameStarted(MazeEventArgs e)
        {
            if (e.Maze != null)
            {
                this.spM.MazeChanged -= GameStarted;
                this.spM.EnemyPositionChanged += PlayerMoved;
                this.Maze = e.Maze;
                NotifyPropertyChanged("Maze");
            }
        }

        /// <summary>
        /// Handlers Opponents movement. Updates the relevant property and notifies the view.
        /// </summary>
        /// <param name="e">The <see cref="EnemyMovedEventArgs"/> instance containing the event data.</param>
        private void PlayerMoved(EnemyMovedEventArgs e)
        {
            if (Maze != null && e.Name == Maze.Name)
            {
                EnemyDirection = e.Direction;
                EnemyMoved?.Invoke(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Handles the game being closed by opponnent.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void EnemyClosedGame(object sender, EventArgs e)
        {
            GameClosed?.Invoke(sender, e);
        }
    }
}
