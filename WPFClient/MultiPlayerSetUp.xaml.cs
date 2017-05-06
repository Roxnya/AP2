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
        SettingsViewModel settingsVM;
        MultiPlayerViewModel mpVM;
        public MultiPlayerSetUp(ISettingsModel sm)
        {
            InitializeComponent();
            settingsVM = new SettingsViewModel(sm);
            mpVM = new MultiPlayerViewModel();
        }

        private void btnStart_Clicked(object sender, RoutedEventArgs e)
        {
            DialogHelper.OpenWindow(new MultiPlayerRoom(mpVM));
            this.Close();
        }
    }
}
