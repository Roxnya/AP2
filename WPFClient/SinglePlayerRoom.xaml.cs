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
        private bool isUserButtonClicked;

        public SinglePlayerRoom(SinglePlayerViewModel vm)
        {
            InitializeComponent();
            vm.SolutionChangedEvent += SolutionChanged;
            vm.CommErrorFailed += Vm_CommErrorFailed;
            DataContext = vm;
            this.vm = vm;
            this.isUserButtonClicked = false;
            this.KeyDown += MazeDisplaySP.KeyPressed;
        }

        private void Vm_CommErrorFailed(object sender, EventArgs e)
        {
            DialogHelper.ShowCommErrorMessage();
        }

        private void MainMenu_Click(object sender, RoutedEventArgs e)
        {
            if (DialogHelper.ShowAreYouSureDialog())
            {
                this.isUserButtonClicked = true;
                this.Close();
            }
        }

        private void SolveMaze_Click(object sender, RoutedEventArgs e)
        {
            vm.RequestSolution();
        }

        private void RestartGame_Click(object sender, RoutedEventArgs e)
        {
            if(DialogHelper.ShowAreYouSureDialog())
                MazeDisplaySP.Reset();
        }

        private void SolutionChanged(object sender, EventArgs e)
        {
            MazeDisplaySP.AnimateSolution(vm.Solution);
        }

        private void MazeDisplaySP_PlayerReachedExit(object sender, EventArgs e)
        {
            DialogHelper.ShowSuccessMessage();
        }

        private void SpWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!isUserButtonClicked && !DialogHelper.ShowAreYouSureDialog())
            {
                e.Cancel = true;
                return;
            }
            DialogHelper.ShowMenu();
        }

        private void MazeDisplay_LostFocus(object sender, RoutedEventArgs e)
        {
            MazeDisplaySP.Focus();
        }
    }
}
