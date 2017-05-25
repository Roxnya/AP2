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
    /// <seealso cref="System.Windows.Controls.UserControl" />
    /// <seealso cref="System.Windows.Markup.IComponentConnector" />
    public partial class NewMaze : UserControl
    {
        /// <summary>
        /// Ctor.
        /// </summary>
        public NewMaze()
        {
            InitializeComponent();
        }

        // Register the routed event
        /// <summary>
        /// The BTN start game clicked event
        /// Invoke when btnStartGame is clicked.
        /// </summary>
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

        /// <summary>
        /// Handles the Click event of the btnStartGame button.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void btnStartGame_Click(object sender, RoutedEventArgs e)
        {
            RaiseEvent(new RoutedEventArgs(NewMaze.btnStartGameClickedEvent));
        }
    }
}
