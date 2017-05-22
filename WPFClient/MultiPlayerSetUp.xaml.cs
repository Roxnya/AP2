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
    /// Interaction logic for MultiPlayerSetUp.xaml
    /// </summary>
    public partial class MultiPlayerSetUp : Window
    {
        MultiPlayerViewModel mpVM;
        private bool isUserButtonClick;

        public MultiPlayerSetUp(ISettingsModel sm)
        {
            InitializeComponent();
            mpVM = new MultiPlayerViewModel(sm);
            mpVM.RequestGames();
            this.DataContext = mpVM;
            this.isUserButtonClick = false;
        }

        private void btnStart_Clicked(object sender, RoutedEventArgs e)
        {
            isUserButtonClick = true;
            mpVM.RequestToOpenRoom();
            OpenMultiplayerRoom();
        }

        private void btnJoin_Click(object sender, RoutedEventArgs e)
        {
            isUserButtonClick = true;
            mpVM.RequestToJoinGame();
            OpenMultiplayerRoom();
        }

        private void OpenMultiplayerRoom()
        {
            DialogHelper.OpenWindow(new MultiPlayerRoom(this.mpVM));
            this.Close();
        }

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
