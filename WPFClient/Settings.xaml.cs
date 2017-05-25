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
    /// <seealso cref="System.Windows.Window" />
    /// <seealso cref="System.Windows.Markup.IComponentConnector" />
    public partial class Settings : Window
    {
        /// <summary>
        /// The vm
        /// </summary>
        SettingsViewModel vm;
        /// <summary>
        /// The user clicked button
        /// </summary>
        private bool userClickedButton;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="sm">The sm.</param>
        public Settings(ISettingsModel sm)
        {
            InitializeComponent();
            vm = new SettingsViewModel(sm);
            DataContext = vm;
            this.userClickedButton = false;
        }

        /// <summary>
        /// Saves changes
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            vm.SaveSettings();
            CloseWindow();
        }

        /// <summary>
        /// Cancels changes
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            vm.CancelSettings();
            CloseWindow();
        }

        /// <summary>
        /// Closes the window.
        /// </summary>
        private void CloseWindow()
        {
            this.userClickedButton = true;
            this.Close();
        }

        /// <summary>
        /// Handles the Closing event of the Window control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!userClickedButton)
            {
                if (!DialogHelper.ShowAreYouSureDialog())
                {
                    e.Cancel = true;
                    return;
                }
                vm.CancelSettings();
            }
            DialogHelper.ShowMenu();
        }
    }
}
