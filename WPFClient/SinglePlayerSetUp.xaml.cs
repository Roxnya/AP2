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
using Client;

namespace WPFClient
{
    /// <summary>
    /// Window for setting up new maze - single player room
    /// </summary>
    /// <seealso cref="System.Windows.Window" />
    /// <seealso cref="System.Windows.Markup.IComponentConnector" />
    public partial class SinglePlayerSetUp : Window
    {
        ISettingsModel sm;
        SinglePlayerSetUpViewModel vm;
        /// <summary>
        /// used to determine if user exited via X button or 'back to menu" button
        /// </summary>
        private bool isUserButtonClick;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="sm">The sm.</param>
        public SinglePlayerSetUp(ISettingsModel sm)
        {
            InitializeComponent();
            this.sm = sm;
            this.vm = new SinglePlayerSetUpViewModel(sm);
            this.DataContext = vm;
            this.isUserButtonClick = false;
        }

        /// <summary>
        /// Handles the Click event of the btnStartGame control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void btnStartGame_Click(object sender, RoutedEventArgs e)
        {
            this.isUserButtonClick = true;
            //singlePlayerVM.StartNewGame();
            
            DialogHelper.OpenWindow(new SinglePlayerRoom(/*singlePlayerVM*/sm, ucNewGameSP.txtName.Text, ucNewGameSP.txtRows.Text, ucNewGameSP.txtColumns.Text));
            this.Close();
        }

        /// <summary>
        /// Handles the Closing event of the Window control.
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
