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
    }
}
