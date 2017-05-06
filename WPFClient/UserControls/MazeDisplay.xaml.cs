using MazeLib;
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
    /// Interaction logic for Maze.xaml
    /// </summary>
    public partial class MazeDisplay : UserControl
    {
        private const int squareSize = 30;
        private const int baseMargin = 30;
        //private Position? initialPosition = null;
        private Image image;
        private Position currPosition;

        public enum PlayerImage { CHICKEN, DARTH_VAIDER };

        public PlayerImage Player
        {
            get { return (PlayerImage)GetValue(PlayerProperty); }
            set 
            { 
                SetValue(PlayerProperty, value);
                SetImage();
            }
        }

        public static readonly DependencyProperty PlayerProperty =
        DependencyProperty.Register("Player", typeof(PlayerImage), typeof(MazeDisplay), new UIPropertyMetadata(IconChanged));

        public int Rows
        {
            get { return (int)GetValue(RowsProperty); }
            set { SetValue(RowsProperty, value); }
        }

        public static readonly DependencyProperty RowsProperty =
            DependencyProperty.Register("Rows", typeof(int), typeof(MazeDisplay), new PropertyMetadata(0));



        public int Cols
        {
            get {
                return (int)GetValue(ColsProperty); 
            }
            set {
                SetValue(ColsProperty, value);
            }
        }

        // Using a DependencyProperty as the backing store for Cols.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ColsProperty =
            DependencyProperty.Register("Cols", typeof(int), typeof(MazeDisplay), new PropertyMetadata(0));



        public Maze Maze
        {
            get { return (Maze)GetValue(MazeProperty); }
            set 
            {
                SetValue(MazeProperty, value);
                DrawMaze();
            }
        }

        public string MazeStr
        {
            get { return (string)GetValue(MazeStrProperty); }
            set 
            { 
                SetValue(MazeStrProperty, value);
                try
                {
                    Maze = Maze.FromJSON(MazeStr);
                }
                catch (Exception ex)
                {
                }
            }
        }

        // Using a DependencyProperty as the backing store for MazeStr.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MazeStrProperty =
            DependencyProperty.Register("MazeStr", typeof(string), typeof(MazeDisplay), new PropertyMetadata(0));



        // Using a DependencyProperty as the backing store for Maze.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MazeProperty =
            DependencyProperty.Register("Maze", typeof(Maze), typeof(MazeDisplay), new UIPropertyMetadata(MazeChanged));

        public MazeDisplay()
        {
            InitializeComponent();
            SetImage();

        }

        public void MovePlayer(Position position)
        {
            if (canvas.Children.Contains(image))
            {
                canvas.Children.Remove(image);
            }
            canvas.Children.Add(image);
            Canvas.SetLeft(image, position.Col * baseMargin);
            Canvas.SetTop(image, position.Row * baseMargin);
        }

        private static void MazeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            MazeDisplay display = (MazeDisplay)d;
            display.DrawMaze();
        }

        private static void IconChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            MazeDisplay display = (MazeDisplay)d;
            display.SetImage();
        }

        private void DrawMaze()
        {
            string mazeStr = Maze.ToString();
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Cols; j++)
                {
                    var mazeIdx = Maze[i, j];

                    //if cell is a wall
                    if (mazeIdx.Equals(CellType.Wall))
                    {
                        AddWallToCanvas(i, j);
                    }
                }
            } //end of both for loops
            MovePlayer(Maze.InitialPos);
            AddExitToCanvas(Maze.GoalPos);
        }

        private void AddExitToCanvas(Position exitPos)
        {
            var rect = AddWallToCanvas(exitPos.Row, exitPos.Col);

            ImageBrush imageBrush = new ImageBrush();
            imageBrush.ImageSource = new BitmapImage(new Uri(@"pack://application:,,,/Resources/door.png", UriKind.Absolute));
            rect.Fill = imageBrush;
        }

        private Rectangle AddWallToCanvas(int topMargin, int leftMargin)
        {
            var rect = new Rectangle() { Height = squareSize, Width = squareSize, 
                                        Fill = new SolidColorBrush(Colors.Black)
            };
            canvas.Children.Add(rect);
            Canvas.SetLeft(rect, leftMargin * baseMargin);
            Canvas.SetTop(rect, topMargin * baseMargin);
            return rect;
        }

        private void SetImage()
        {
            if (Player.Equals(PlayerImage.CHICKEN))
            {
                image = new Image()
                {
                    Width = 30, Height = 30,
                    Source = new BitmapImage(
                        new Uri(@"pack://application:,,,/Resources/chicken.png", UriKind.Absolute))
                };
            }
            else if (Player.Equals(PlayerImage.DARTH_VAIDER))
            {
                image = new Image()
                {
                    Width = 30,
                    Height = 30,
                    Source = new BitmapImage(
                        new Uri(@"pack://application:,,,/Resources/darth.png", UriKind.Absolute))
                };
            }
        }

        private void KeyPressed(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Up)
            {
                TryMove(new Position(currPosition.Row - 1, currPosition.Col));
            }
            else if (e.Key == Key.Down)
            {
                TryMove(new Position(currPosition.Row + 1, currPosition.Col));
            }
            else if (e.Key == Key.Left)
            {
                TryMove(new Position(currPosition.Row, currPosition.Col - 1));
            }
            else if (e.Key == Key.Right)
            {
                TryMove(new Position(currPosition.Row, currPosition.Col + 1));
            }
        }

        private void TryMove(Position newPosition)
        {
            if (Maze[currPosition.Row, currPosition.Col] == CellType.Free)
            {
                currPosition = new Position(currPosition.Row, currPosition.Col);
                MovePlayer(newPosition);
            }
        }

        public void Reset()
        {
            currPosition = Maze.InitialPos;
            MovePlayer(currPosition);
        }
    }
}
