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
using WPFClient.ViewModels;

namespace WPFClient
{
    /// <summary>
    /// Interaction logic for MultiPlayerRoom.xaml
    /// </summary>
    public partial class MultiPlayerRoom : Window
    {
        private MultiPlayerViewModel vm;
        private bool isClosedByEnemy;
        private bool isUserButtonClicked;

        public MultiPlayerRoom(MultiPlayerViewModel vm)
        {
            InitializeComponent();
            isClosedByEnemy = false;
            isUserButtonClicked = false;
            this.vm = vm;
            this.DataContext = vm;
            this.vm.EnemyMoved += Vm_EnemyMoved;
            this.vm.GameClosed += Vm_GameClosed;
            this.vm.CommErrorFailed += Vm_CommErrorFailed;
            this.KeyDown += PlayerMazeDisplay.KeyPressed;
        }

        private void Vm_CommErrorFailed(object sender, EventArgs e)
        {
            DialogHelper.ShowCommErrorMessage();
        }

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

        private void Vm_EnemyMoved(object sender, EventArgs e)
        {
            EnemyMazeDisplay.TryMove(vm.EnemyDirection);
        }

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

        private void TerminateGame()
        {
            //send close message only if this player closed game(instead opponent)
            if(!isClosedByEnemy && !vm.CommError)
                vm.CloseGame();
        }

        private void PlayerMazeDisplay_PlayerMoved(DirectionEventArgs e)
        {
            vm.SendMoveCommand(e.Direction);
        }
        
        private void PlayerMazeDisplay_PlayerReachedExit(object sender, EventArgs e)
        {
           DialogHelper.ShowSuccessMessage();
        }

        private void EnemyMazeDisplay_PlayerReachedExit(object sender, EventArgs e)
        {
            DialogHelper.ShowDefeatMessage();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!isUserButtonClicked && !DialogHelper.ShowAreYouSureDialog())
            {
                e.Cancel = true;
                return;
            }
            this.vm.GameClosed -= Vm_GameClosed;
            TerminateGame();
            DialogHelper.ShowMenu();
        }
    }
}
