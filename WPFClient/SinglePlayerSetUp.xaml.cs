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
    /// Interaction logic for SinglePlayerSetUp.xaml
    /// </summary>
    public partial class SinglePlayerSetUp : Window
    {
        SettingsViewModel settingsVM;
        SinglePlayerViewModel singlePlayerVM;

        public SinglePlayerSetUp(ISettingsModel sm)
        {
            InitializeComponent();
            this.settingsVM = new SettingsViewModel(sm);
            this.DataContext = settingsVM;
            this.singlePlayerVM = new SinglePlayerViewModel();
        }

        private void btnStartGame_Click(object sender, RoutedEventArgs e)
        {
            singlePlayerVM.StartNewGame(uc.txtName.Text, int.Parse(uc.txtRows.Text), 
                                        int.Parse(uc.txtColumns.Text));
            DialogHelper.OpenWindow(new SinglePlayerRoom());
            this.Close();
        }
    }
}
