using Client;
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
    /// Interaction logic for SinglePlayerRoom.xaml
    /// </summary>
    public partial class SinglePlayerRoom : Window
    {
        SinglePlayerViewModel vm;
        /// <summary>
        /// used to determine if user exited via X button or 'back to menu" button
        /// </summary>
        private bool isUserButtonClicked;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="sm">Maze sm.</param>
        /// <param name="name">Maze name.</param>
        /// <param name="rows">Maze rows.</param>
        /// <param name="cols">Maze cols.</param>
        public SinglePlayerRoom(ISettingsModel sm, string name, string rows, string cols)
        {
            InitializeComponent();
            this.vm = new SinglePlayerViewModel(sm, name, rows, cols);
            vm.SolutionChangedEvent += SolutionChanged;
            vm.CommErrorFailed += Vm_CommErrorFailed;
            DataContext = vm;
            this.isUserButtonClicked = false;
            this.KeyDown += MazeDisplaySP.KeyPressed;
        }

        /// <summary>
        /// Display error when there is no communication with server
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void Vm_CommErrorFailed(object sender, EventArgs e)
        {
            DialogHelper.ShowCommErrorMessage();
        }

        /// <summary>
        /// Returns to main menu
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void MainMenu_Click(object sender, RoutedEventArgs e)
        {
            if (DialogHelper.ShowAreYouSureDialog())
            {
                this.isUserButtonClicked = true;
                this.Close();
            }
        }

        /// <summary>
        /// Request maze solution from server
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void SolveMaze_Click(object sender, RoutedEventArgs e)
        {
            vm.RequestSolution();
        }

        /// <summary>
        /// Resets game. Draws player at InitialPos
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void RestartGame_Click(object sender, RoutedEventArgs e)
        {
            if(DialogHelper.ShowAreYouSureDialog())
                MazeDisplaySP.Reset();
        }

        /// <summary>
        /// Solutions changed Handler.
        /// Disables buttons until animation ended
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void SolutionChanged(object sender, EventArgs e)
        {
            this.Dispatcher.Invoke(() =>
            {
                BtnReturn.IsEnabled = false;
                BtnSolve.IsEnabled = false;
                BtnRestart.IsEnabled = false;
            });
            MazeDisplaySP.AnimateSolution(vm.Solution);
        }

        /// <summary>
        /// Displays success message on game end(reaching GoalPos).
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void MazeDisplaySP_PlayerReachedExit(object sender, EventArgs e)
        {
            DialogHelper.ShowSuccessMessage();
        }

        /// <summary>
        /// Handles the Closing event of the SpWindow control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
        private void SpWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!isUserButtonClicked && !DialogHelper.ShowAreYouSureDialog())
            {
                e.Cancel = true;
                return;
            }
            DialogHelper.ShowMenu();
        }

        /// <summary>
        /// Handles the LostFocus event of the MazeDisplay control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void MazeDisplay_LostFocus(object sender, RoutedEventArgs e)
        {
            MazeDisplaySP.Focus();
        }

        /// <summary>
        /// ReEnables buttons at the end of solution animation
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void MazeDisplaySP_AnimationEnded(object sender, RoutedEventArgs e)
        {
            this.Dispatcher.Invoke(() =>
            {
                BtnSolve.IsEnabled = true;
                BtnReturn.IsEnabled = true;
                BtnRestart.IsEnabled = true;
            });
        }
    }
}
