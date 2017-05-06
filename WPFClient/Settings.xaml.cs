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
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : Window
    {
        SettingsViewModel vm;
        public Settings(ISettingsModel sm)
        {
            InitializeComponent();
            vm = new SettingsViewModel(sm);
            DataContext = vm;
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            vm.SaveSettings();
            CloseWindow();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            vm.CancelSettings();
            CloseWindow();
        }

        private void CloseWindow()
        {
            MainWindow win = (MainWindow)Application.Current.MainWindow;
            win.Show();
            this.Close();
        }
    }
}
