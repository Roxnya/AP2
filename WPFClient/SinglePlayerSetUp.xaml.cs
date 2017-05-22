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
    /// Interaction logic for SinglePlayerSetUp.xaml
    /// </summary>
    public partial class SinglePlayerSetUp : Window
    {
        //SettingsViewModel settingsVM;
        SinglePlayerViewModel singlePlayerVM;
        ISettingsModel sm;
        private bool isUserButtonClick;

        public SinglePlayerSetUp(ISettingsModel sm)
        {
            InitializeComponent();
            //this.settingsVM = new SettingsViewModel(sm);
            //this.DataContext = settingsVM;
            this.singlePlayerVM = new SinglePlayerViewModel(sm);
            this.sm = sm;
            this.DataContext = singlePlayerVM;
            this.isUserButtonClick = false;
        }

        private void btnStartGame_Click(object sender, RoutedEventArgs e)
        {
            this.isUserButtonClick = true;
            singlePlayerVM.StartNewGame();
            DialogHelper.OpenWindow(new SinglePlayerRoom(singlePlayerVM));
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
