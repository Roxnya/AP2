using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WPFClient.Models;
using WPFClient.ViewModels;

namespace WPFClient
{
    /// <summary>
    /// Interaction logic for MultiPlayerRoom.xaml
    /// </summary>
    /// <seealso cref="System.Windows.Window" />
    /// <seealso cref="System.Windows.Markup.IComponentConnector" />
    public partial class MultiPlayerRoom : Window
    {

        private MultiPlayerViewModel vm;

        private bool isClosedByEnemy;
        /// <summary>
        /// used to determine if user exited via exit button or "back to menu" button
        /// </summary>
        private bool isUserButtonClicked;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="sm">The sm.</param>
        /// <param name="name">The name.</param>
        /// <param name="rows">The rows.</param>
        /// <param name="cols">The cols.</param>
        /// <param name="isHost">if set to <c>true</c> [is host].</param>
        public MultiPlayerRoom(ISettingsModel sm, string name, string rows, string cols, bool isHost)
        {
            Init(sm, name, rows, cols, isHost);
        }

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="sm">The sm.</param>
        /// <param name="name">The name.</param>
        public MultiPlayerRoom(ISettingsModel sm, string name)
        {
            Init(sm, name, string.Empty, string.Empty, false);
        }

        /// <summary>
        /// Initializes the window
        /// </summary>
        /// <param name="sm">The sm.</param>
        /// <param name="name">The name.</param>
        /// <param name="rows">The rows.</param>
        /// <param name="cols">The cols.</param>
        /// <param name="isHost">if set to <c>true</c> [is host].</param>
        private void Init(ISettingsModel sm, string name, string rows, string cols, bool isHost)
        {
            InitializeComponent();
            isClosedByEnemy = false;
            isUserButtonClicked = false;
            this.vm = new MultiPlayerViewModel(sm, name, rows, cols, isHost);
            this.DataContext = vm;
            //register to relevant events
            this.vm.EnemyMoved += Vm_EnemyMoved;
            this.vm.GameClosed += Vm_GameClosed;
            this.vm.CommErrorFailed += Vm_CommErrorFailed;
            this.KeyDown += PlayerMazeDisplay.KeyPressed;
        }

        /// <summary>
        /// Displays Error message when there is no communication with server
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void Vm_CommErrorFailed(object sender, EventArgs e)
        {
            DialogHelper.ShowCommErrorMessage();
        }

        /// <summary>
        /// Notifies Opponent left and disables control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void Vm_GameClosed(object sender, EventArgs e)
        {
            this.vm.CommErrorFailed -= Vm_CommErrorFailed;
            MessageBox.Show("Opponent left the room!", "Game Closed", MessageBoxButton.OK);
            Dispatcher.Invoke(() =>
            {
                this.EnemyMazeDisplay.IsEnabled = false;
                this.PlayerMazeDisplay.IsEnabled = false;
            });
            isClosedByEnemy = true;
        }

        /// <summary>
        /// Invoked when opponent moved. Tries to move opponent in his representing board.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void Vm_EnemyMoved(object sender, EventArgs e)
        {
            EnemyMazeDisplay.TryMove(vm.EnemyDirection);
        }

        /// <summary>
        /// Returns to menu.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void BackToMenu_Click(object sender, RoutedEventArgs e)
        {
            if (DialogHelper.ShowAreYouSureDialog())
            {
                this.isUserButtonClicked = true;
                this.vm.GameClosed -= Vm_GameClosed;
                TerminateGame();
                this.Close();
            }
        }

        /// <summary>
        /// Terminates the game.
        /// </summary>
        private void TerminateGame()
        {
            //send close message only if this player closed game(instead opponent)
            if(!isClosedByEnemy && !vm.CommError)
                vm.CloseGame();
        }

        /// <summary>
        /// Notifies server that this player moved.
        /// </summary>
        /// <param name="e">The <see cref="DirectionEventArgs"/> instance containing the event data.</param>
        private void PlayerMazeDisplay_PlayerMoved(DirectionEventArgs e)
        {
            vm.SendMoveCommand(e.Direction);
        }

        /// <summary>
        /// Handles the PlayerReachedExit event of the PlayerMazeDisplay control.
        /// Shows success message.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void PlayerMazeDisplay_PlayerReachedExit(object sender, EventArgs e)
        {
           DialogHelper.ShowSuccessMessage();
        }

        /// <summary>
        /// Handles the PlayerReachedExit event of the EnemyMazeDisplay control.
        /// shows defeat message.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void EnemyMazeDisplay_PlayerReachedExit(object sender, EventArgs e)
        {
            DialogHelper.ShowDefeatMessage();
        }

        /// <summary>
        /// Handles the Closing event of the Window control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //makes sure user wants to exit only if the closing event is due to user's click on X button.
            if (!isUserButtonClicked && !DialogHelper.ShowAreYouSureDialog())
            {
                e.Cancel = true;
                return;
            }
            this.vm.GameClosed -= Vm_GameClosed;
            TerminateGame();
            DialogHelper.ShowMenu();
        }

        /// <summary>
        /// Handles the LostFocus event of the Grid control.
        /// This is used because focus lose disables keydown event and messes up the game.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void Grid_LostFocus(object sender, RoutedEventArgs e)
        {
            //this.PlayerMazeDisplay.canvas.Focus();
        }
    }
}
