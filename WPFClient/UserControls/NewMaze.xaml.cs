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

namespace WPFClient.UserControls
{
    /// <summary>
    /// Interaction logic for NewMaze.xaml
    /// </summary>
    public partial class NewMaze : UserControl
    {
        public NewMaze()
        {
            InitializeComponent();
        }

        // Register the routed event
        public static readonly RoutedEvent btnStartGameClickedEvent =
            EventManager.RegisterRoutedEvent("btnStartGameClicked", RoutingStrategy.Bubble,
            typeof(RoutedEventHandler), typeof(NewMaze));

        // .NET wrapper
        public event RoutedEventHandler btnStartGameClicked
        {
            add
            {
                AddHandler(btnStartGameClickedEvent, value);
            }
            remove { RemoveHandler(btnStartGameClickedEvent, value); }
        }

        private void btnStartGame_Click(object sender, RoutedEventArgs e)
        {
            RaiseEvent(new RoutedEventArgs(NewMaze.btnStartGameClickedEvent));
        }
    }
}
