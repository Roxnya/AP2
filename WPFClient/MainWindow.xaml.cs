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
    /// Main window.
    /// </summary>
    /// <seealso cref="System.Windows.Window" />
    /// <seealso cref="System.Windows.Markup.IComponentConnector" />
    public partial class MainWindow : Window
    {
        /// <summary>
        /// The vm
        /// </summary>
        MenuViewModel vm;
        /// <summary>
        /// Ctor
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            vm = new MenuViewModel();
            this.DataContext = vm;
        }

        /// <summary>
        /// Opens Single player set up window
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void SinglePlayer_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            DialogHelper.OpenModalWindow(new SinglePlayerSetUp(vm.SettingsModel));
        }

        /// <summary>
        /// Opens multiplayer player set up window
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void MultiPlayer_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            DialogHelper.OpenModalWindow(new MultiPlayerSetUp(vm.SettingsModel));
        }

        /// <summary>
        /// Opens Settings window
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();

            DialogHelper.OpenModalWindow(new Settings(vm.SettingsModel));
        }
    }
}
