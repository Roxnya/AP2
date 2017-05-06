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
using WPFClient.ViewModels;

namespace WPFClient
{
    /// <summary>
    /// Interaction logic for SinglePlayerRoom.xaml
    /// </summary>
    public partial class SinglePlayerRoom : Window
    {
        SinglePlayerViewModel vm;
        public SinglePlayerRoom()
        {
            InitializeComponent();
            vm = new SinglePlayerViewModel();
            DataContext = vm;
        }

        private void MainMenu_Click(object sender, RoutedEventArgs e)
        {
            //insert are u sure message
            this.Close();
        }

        private void SolveMaze_Click(object sender, RoutedEventArgs e)
        {
            //vm.GetMazeSolution();
            //loop solution
        }

        private void RestartGame_Click(object sender, RoutedEventArgs e)
        {
            //insert are you sure message
            MazeDisplay.MovePlayer(vm.Maze.InitialPos);
        }
    }
}
