using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WPFClient
{
    /// <summary>
    /// Class DialogHelper.
    /// </summary>
    class DialogHelper
    {
        /// <summary>
        /// Opens the modal window.
        /// </summary>
        /// <param name="window">The window to open.</param>
        public static void OpenModalWindow(Window window)
        {
            try
            {
                window.ShowDialog();
            }
            catch (Exception ex)
            { }
        }

        /// <summary>
        /// Opens window.
        /// </summary>
        /// <param name="window">The window to open.</param>
        public static void OpenWindow(Window window)
        {
            try
            {
                window.Show();
            }
            catch (Exception ex)
            { }
        }

        /// <summary>
        /// Shows the success message.
        /// </summary>
        public static void ShowSuccessMessage()
        {
            new FinishMessage(FinishMessage.Mode.WIN).ShowDialog();
        }

        /// <summary>
        /// Shows the defeat message.
        /// </summary>
        public static void ShowDefeatMessage()
        {
            new FinishMessage(FinishMessage.Mode.DEFEAT).ShowDialog();
        }

        /// <summary>
        /// Shows the are you sure dialog.
        /// </summary>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public static bool ShowAreYouSureDialog()
        {
            var result = MessageBox.Show("Are you sure you want to procceed?", "Caution!", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
                return true;
            return false;
        }

        /// <summary>
        /// Shows the comm error message.
        /// </summary>
        public static void ShowCommErrorMessage()
        {
            MessageBox.Show("No Communication With Server, Please Try To Reopen Room", "Error!", MessageBoxButton.OK);
        }

        /// <summary>
        /// Shows the menu.
        /// </summary>
        public static void ShowMenu()
        {
            MainWindow win = (MainWindow)Application.Current.MainWindow;
            win.Show();
        }
    }
}
