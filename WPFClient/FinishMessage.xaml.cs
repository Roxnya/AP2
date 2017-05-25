using System.Windows;

namespace WPFClient
{
    /// <summary>
    /// Window for displaying a message at the end of the game
    /// </summary>
    public partial class FinishMessage : Window
    {
        /// <summary>
        /// Enum Mode - window's mode
        /// </summary>
        public enum Mode { WIN, DEFEAT};

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="mode">The mode.</param>
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

        /// <summary>
        /// Handles the Click event of the btnOk control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
 