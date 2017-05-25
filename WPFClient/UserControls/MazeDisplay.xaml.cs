using MazeLib;
using SearchAlgorithmsLib;
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
using System.Windows.Threading;

namespace WPFClient.UserControls
{
    /// <summary>
    /// Interaction logic for Maze.xaml
    /// </summary>
    public partial class MazeDisplay : UserControl
    {
        private object _locker = new object();
        private const int squareSize = 30;
        private const int baseMargin = 30;
        private Image image;
        private string solution;
        private DispatcherTimer timer;
        private int tick;

        private Position CurrentPosition { get; set; }

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

        public delegate void PlayerMovedEventHandler (DirectionEventArgs e);
        public event PlayerMovedEventHandler PlayerMoved;
        
        public event EventHandler PlayerReachedExit;

        #region Dependency Properties
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



        public int CanvasWidth
        {
            get { return (int)GetValue(CanvasWidthProperty); }
            set { SetValue(CanvasWidthProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CanvasWidth.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CanvasWidthProperty =
            DependencyProperty.Register("CanvasWidth", typeof(int), typeof(MazeDisplay), new PropertyMetadata(300));

        public int CanvasHeight
        {
            get { return (int)GetValue(CanvasHeightProperty); }
            set { SetValue(CanvasHeightProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CanvasHeight.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CanvasHeightProperty =
            DependencyProperty.Register("CanvasHeight", typeof(int), typeof(MazeDisplay), new PropertyMetadata(300));



        public Maze Maze
        {
            get
            {
                return (Maze)GetValue(MazeProperty);
            }
            set 
            {

                SetValue(MazeProperty, value.ToJSON());
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
                    if(MazeStr != string.Empty)
                        Maze = Maze.FromJSON(MazeStr);
                }
                catch (Exception ex)
                {
                }
            }
        }

        // Using a DependencyProperty as the backing store for MazeStr.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MazeStrProperty =
            DependencyProperty.Register("MazeStr", typeof(string), typeof(MazeDisplay), new PropertyMetadata(String.Empty));

        // Using a DependencyProperty as the backing store for Maze.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MazeProperty =
            DependencyProperty.Register("Maze", typeof(Maze), typeof(MazeDisplay), new UIPropertyMetadata(null, new PropertyChangedCallback( MazeChanged)));
        #endregion

        #region PropertyChanged Events
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
        #endregion

        private void ExitReached()
        {
            this.IsEnabled = false;
            PlayerReachedExit?.Invoke(this, EventArgs.Empty);
        }

        public MazeDisplay()
        {
            InitializeComponent();
            //set default player image
            SetImage();
            this.timer = new DispatcherTimer();
            this.timer.Interval = TimeSpan.FromMilliseconds(200);
            this.timer.Tick += SolutionAnimation_Tick;
            this.tick = 0;
            this.Focusable = true;
        }

        #region Initial Drawing related methods
        private void AdjustCanvasSize()
        {
            var newSize = Rows * baseMargin;
            if (newSize > canvas.Height)
            {
                CanvasHeight = newSize;
            }
            newSize = Cols * baseMargin;
            if (newSize > canvas.Width)
            {
                CanvasWidth = newSize;
            }
        }

        private void DrawMaze()
        {
            AdjustCanvasSize();
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
            var rect = new Rectangle();
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

        #endregion

        #region Player Drawing methods
        internal void KeyPressed(object sender, KeyEventArgs e)
        {
            e.Handled = true;
            Direction direction = Direction.Unknown;
            if (e.Key == Key.Up)
            {
                if (TryMove(new Position(CurrentPosition.Row - 1, CurrentPosition.Col)))
                    direction = Direction.Up;

            }
            else if (e.Key == Key.Down)
            {
                if (TryMove(new Position(CurrentPosition.Row + 1, CurrentPosition.Col)))
                    direction = Direction.Down;
            }
            else if (e.Key == Key.Left)
            {
                if (TryMove(new Position(CurrentPosition.Row, CurrentPosition.Col - 1)))
                    direction = Direction.Left;
            }
            else if (e.Key == Key.Right)
            {
                if (TryMove(new Position(CurrentPosition.Row, CurrentPosition.Col + 1)))
                    direction = Direction.Right;
            }

            if (direction != Direction.Unknown)
                PlayerMoved?.Invoke(new DirectionEventArgs(direction));
        }

        public void MovePlayer(Position pos)
        {
            if (canvas.Children.Contains(image))
            {
                canvas.Children.Remove(image);
            }
            canvas.Children.Add(image);
            CurrentPosition = pos;
            Canvas.SetLeft(image, pos.Col * baseMargin);
            Canvas.SetTop(image, pos.Row * baseMargin);
            if (CurrentPosition.Equals(Maze.GoalPos))
            {
                ExitReached();
            }
        }

        private bool TryMove(Position newPosition)
        {
            if (!(newPosition.Row < 0 || newPosition.Col < 0 || newPosition.Col >= Maze.Cols 
                || newPosition.Row >= Maze.Rows) && Maze[newPosition.Row, newPosition.Col] == CellType.Free)
            {
                MovePlayer(newPosition);
                return true;
            }
            return false;
        }

        public void TryMove(Direction direction)
        {
            this.Dispatcher.Invoke(() =>
            {
                TryMove(GetPositionByDirection(direction));
            });
        }

        public void Reset()
        {
            if (!this.IsEnabled)
            {
                this.IsEnabled = true;
            }
            MovePlayer(Maze.InitialPos);
        }

        public void AnimateSolution(string solution)
        {
            this.solution = solution;

            this.Dispatcher.Invoke(() =>
            {
                MovePlayer(Maze.InitialPos);
            });
            //make sure to cancel the timer if it's already running
            InitTimer();
            timer.Start();
        }

        private void InitTimer()
        {
            if (timer.IsEnabled)
            {
                timer.Stop();
                tick = 0;
            }
        }

        private void SolutionAnimation_Tick(object sender, EventArgs e)
        {
            if (solution.Length == 0) return;

            Direction direction;
            if (Enum.TryParse<Direction>(solution[tick].ToString(), out direction))
            {
                MovePlayer(GetPositionByDirection(direction));
                tick++;
                if (tick.Equals(solution.Length))
                {
                    InitTimer();
                }
            }
        }

        private Position GetPositionByDirection(Direction direction)
        {
            Position pos;
            switch (direction)
            {
                case Direction.Up:
                    pos = new Position(CurrentPosition.Row - 1, CurrentPosition.Col);
                break;
                case Direction.Down:
                    pos = new Position(CurrentPosition.Row + 1, CurrentPosition.Col);
                break;
                case Direction.Left:
                    pos = new Position(CurrentPosition.Row, CurrentPosition.Col - 1);
                break;
                case Direction.Right:
                    pos = new Position(CurrentPosition.Row, CurrentPosition.Col + 1);
                break;
                default:
                    throw new Exception("Invalid position");
                break;
            }
            return pos;
        }
        #endregion
    }
}
