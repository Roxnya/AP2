using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WPFClient
{
    class DialogHelper
    {
        public static void OpenModalWindow(Window window)
        {
            try
            {
                window.ShowDialog();
            }
            catch (Exception ex)
            { }
        }

        public static void OpenWindow(Window window)
        {
            try
            {
                window.Show();
            }
            catch (Exception ex)
            { }
        }

        public static void ShowSuccessMessage()
        {
            new FinishMessage(FinishMessage.Mode.WIN).ShowDialog();
        }

        public static void ShowDefeatMessage()
        {
            new FinishMessage(FinishMessage.Mode.DEFEAT).ShowDialog();
        }

        public static bool ShowAreYouSureDialog()
        {
            var result = MessageBox.Show("Are you sure you want to procceed?", "Caution!", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
                return true;
            return false;
        }

        public static void ShowMenu()
        {
            MainWindow win = (MainWindow)Application.Current.MainWindow;
            win.Show();
        }
    }
}
