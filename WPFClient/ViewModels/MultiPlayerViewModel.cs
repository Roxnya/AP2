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
    public class MultiPlayerViewModel : RoomViewModel
    {
        private ISettingsModel sm;
        private ObservableCollection<String> games;

        public event EventHandler EnemyMoved;
        public event EventHandler GameClosed;
        public Direction EnemyDirection { get; private set; }
        public string SelectedGame { get; set; }
        public ObservableCollection<String> Games { get { return games; }  private set { games = value; NotifyPropertyChanged("Games"); } }
        
        public MultiPlayerViewModel(ISettingsModel sm) : base(new Player(sm.ServerPort, sm.ServerIP, false))
        {
            this.sm = sm;
            this.Rows = sm.MazeRows;
            this.Columns = sm.MazeCols;
            this.spM.GameClosed += EnemyClosedGame;
        }

        public void RequestGames()
        {
            this.spM.GamesListChanged += ListChanged;
            this.spM.InjectCommand(CommandsFactory.GetGamesListCommand());
        }

        private void ListChanged(GameListEventArgs e)
        {
            if (e.Games != null)
            {
                Games = new ObservableCollection<string>(e.Games);
            }
        }

        public void RequestToJoinGame()
        {
            this.spM.MazeChanged += GameStarted;
            spM.InjectCommand(CommandsFactory.GetJoinCommand(SelectedGame));
            Name = SelectedGame;
        }

        public void RequestToOpenRoom()
        {
            this.spM.MazeChanged += GameStarted;
            spM.InjectCommand(CommandsFactory.GetStartCommand(Name, Rows, Columns));
        }

        public void CloseGame()
        {
            spM.InjectCommand(CommandsFactory.GetCloseCommand(Name));
        }

        public void SendMoveCommand(Direction direction)
        {
            spM.InjectCommand(CommandsFactory.GetPlayCommand(direction));
        }

        private void GameStarted(MazeEventArgs e)
        {
            if (e.Maze != null)
            {
                this.spM.MazeChanged -= GameStarted;
                this.spM.EnemyPositionChanged += PlayerMoved;
                this.Maze = e.Maze;
                NotifyPropertyChanged("Maze");
                NotifyPropertyChanged("Maze.Rows");
                NotifyPropertyChanged("Maze.Cols");
            }
        }

        private void PlayerMoved(EnemyMovedEventArgs e)
        {
            if (Maze != null && e.Name == Maze.Name)
            {
                EnemyDirection = e.Direction;
                EnemyMoved?.Invoke(this, EventArgs.Empty);
            }
        }

        private void EnemyClosedGame(object sender, EventArgs e)
        {
            GameClosed?.Invoke(sender, e);
        }
    }
}
