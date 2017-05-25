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
    /// Class for multiplayer menu
    /// </summary>
    public partial class MultiPlayerSetUp : Window
    {
        private MultiPlayerSetupViewModel mpSVM;
        /// <summary>
        /// used to determine if user exited via X button or 'back to menu" button
        /// </summary>
        private bool isUserButtonClick;
        private ISettingsModel sm;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="sm">The sm.</param>
        public MultiPlayerSetUp(ISettingsModel sm)
        {
            InitializeComponent();
            this.sm = sm;
            mpSVM = new MultiPlayerSetupViewModel(sm);
            this.DataContext = mpSVM;
            this.isUserButtonClick = false;
        }

        /// <summary>
        /// Opens multiplayer room as the host.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void btnStart_Clicked(object sender, RoutedEventArgs e)
        {
            isUserButtonClick = true;
            
            OpenMultiplayerRoom(new MultiPlayerRoom(this.sm, ucNewGameMP.txtName.Text, ucNewGameMP.txtRows.Text, ucNewGameMP.txtColumns.Text, true));
        }

        /// <summary>
        /// Joins multiplayer room.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void btnJoin_Click(object sender, RoutedEventArgs e)
        {
            isUserButtonClick = true;
            OpenMultiplayerRoom(new MultiPlayerRoom(this.sm, mpSVM.SelectedGame));
        }

        /// <summary>
        /// Opens the multiplayer room.
        /// </summary>
        /// <param name="room">The room.</param>
        private void OpenMultiplayerRoom(MultiPlayerRoom room)
        {
            DialogHelper.OpenWindow(room);
            this.Close();
        }

        /// <summary>
        /// Handles the Closing event of the Window control.
        /// Closes the window only if player is sure.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!isUserButtonClick)
            {
                if (!DialogHelper.ShowAreYouSureDialog())
                {
                    e.Cancel = true;
                    return;
                }
                else
                    DialogHelper.ShowMenu();
            }
        }
    }
}
