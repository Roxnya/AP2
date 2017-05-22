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

namespace WPFClient
{
    /// <summary>
    /// Interaction logic for FinishMessage.xaml
    /// </summary>
    public partial class FinishMessage : Window
    {
        public enum Mode { WIN, DEFEAT};

        public FinishMessage(Mode mode)
        {
            InitializeComponent();
            if (mode == Mode.DEFEAT)
            {
                DefeatPanel.Visibility = Visibility.Visible;
            }
            else if (mode == Mode.WIN)
            {
                WinPanel.Visibility = Visibility.Visible;
            }
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
 