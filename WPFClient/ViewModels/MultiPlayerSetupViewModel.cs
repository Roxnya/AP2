using Client;
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
    /// Class MultiPlayerSetupViewModel.
    /// </summary>
    /// <seealso cref="WPFClient.ViewModels.ViewModel" />
    public class MultiPlayerSetupViewModel : SinglePlayerSetUpViewModel
    {
        /// <summary>
        /// The player model
        /// </summary>
        private Player playerModel;
        /// <summary>
        /// List of games open for joinning.
        /// </summary>
        private ObservableCollection<String> games;

        /// <summary>
        /// Gets or sets the selected game.
        /// </summary>
        /// <value>The selected game.</value>
        public string SelectedGame { get; set; }
        /// <summary>
        /// Gets the games.
        /// </summary>
        /// <value>The games.</value>
        public ObservableCollection<String> Games { get { return games; } private set { games = value; NotifyPropertyChanged("Games"); } }

        /// <summary>
        /// Initializes a new instance of the <see cref="MultiPlayerSetupViewModel"/> class.
        /// </summary>
        /// <param name="model">The model.</param>
        public MultiPlayerSetupViewModel(ISettingsModel model) : base (model)
        {
            this.playerModel = new Player(model.ServerPort, model.ServerIP, false);
            this.playerModel.InitFlow();
            RequestGames();
            
        }

        /// <summary>
        /// Requests list of games open for joinning.
        /// </summary>
        public void RequestGames()
        {
            this.playerModel.GamesListChanged += ListChanged;
            this.playerModel.InjectCommand(CommandsFactory.GetGamesListCommand());
        }

        /// <summary>
        /// Lists of games changed handler. Sets list of games.
        /// </summary>
        /// <param name="e">The <see cref="GameListEventArgs"/> instance containing the event data.</param>
        private void ListChanged(GameListEventArgs e)
        {
            if (e.Games != null)
            {
                Games = new ObservableCollection<string>(e.Games);
            }
        }
    }
}
