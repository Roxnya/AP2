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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPFClient.ViewModels;

namespace WPFClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MenuViewModel vm;
        public MainWindow()
        {
            InitializeComponent();
            vm = new MenuViewModel();
            this.DataContext = vm;
        }

        private void SinglePlayer_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            vm.OpenSinglePlayerRoom();
        }

        private void MultiPlayer_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            vm.OpenMultiPlayerRoom();
        }

        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            vm.OpenSettings();
        }
    }
}
