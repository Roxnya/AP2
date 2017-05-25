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
    /// MazeDisplay is a user control responsible for maze's logic. Drawing it, moving in it, etc.
    /// </summary>
    public partial class MazeDisplay : UserControl
    {
        /// <summary>
        /// The square size
        /// </summary>
        private const int squareSize = 30;
        /// <summary>
        /// The base margin
        /// </summary>
        private const int baseMargin = 30;
        /// <summary>
        /// Player's Representing image in the game
        /// </summary>
        private Image image;
        /// <summary>
        /// The solution
        /// </summary>
        private string solution;
        /// <summary>
        /// Responsible for timing solution animation.
        /// </summary>
        private DispatcherTimer timer;
        /// <summary>
        /// represents current tick in the animation
        /// </summary>
        private int tick;

        /// <summary>
        /// Gets or sets the current position.
        /// </summary>
        /// <value>The current position.</value>
        private Position CurrentPosition { get; set; }


        /// <summary>
        /// Ctor.
        /// </summary>
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
            //neccessary in order for key down to happen :|
            this.scroller.Focusable = false;
        }

        /// <summary>
        /// Enum PlayerImage - Available images for representing players
        /// </summary>
        public enum PlayerImage { CHICKEN, DARTH_VAIDER };

        /// <summary>
        /// Gets or sets selected image.
        /// </summary>
        /// <value>The player.</value>
        public PlayerImage Player
        {
            get { return (PlayerImage)GetValue(PlayerProperty); }
            set 
            { 
                SetValue(PlayerProperty, value);
                SetImage();
            }
        }

        /// <summary>
        /// Delegate PlayerMovedEventHandler
        /// </summary>
        public delegate void PlayerMovedEventHandler (DirectionEventArgs e);
        public event PlayerMovedEventHandler PlayerMoved;
        
        /// <summary>
        /// Invoked when player reached exit.
        /// </summary>
        public event EventHandler PlayerReachedExit;

        #region Dependency Properties
        /// <summary>
        /// The player property
        /// </summary>
        public static readonly DependencyProperty PlayerProperty =
        DependencyProperty.Register("Player", typeof(PlayerImage), typeof(MazeDisplay), new UIPropertyMetadata(IconChanged));

        /// <summary>
        /// Gets or sets the rows.
        /// </summary>
        /// <value>The rows.</value>
        public int Rows
        {
            get { return (int)GetValue(RowsProperty); }
            set { SetValue(RowsProperty, value); }
        }

        /// <summary>
        /// The rows property
        /// </summary>
        public static readonly DependencyProperty RowsProperty =
            DependencyProperty.Register("Rows", typeof(int), typeof(MazeDisplay), new PropertyMetadata(0));



        /// <summary>
        /// Gets or sets the cols.
        /// </summary>
        /// <value>The cols.</value>
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
        /// <summary>
        /// The cols property
        /// </summary>
        public static readonly DependencyProperty ColsProperty =
            DependencyProperty.Register("Cols", typeof(int), typeof(MazeDisplay), new PropertyMetadata(0));



        /// <summary>
        /// Gets or sets the width of the canvas.
        /// This property is used since canvas must have initial value, and won't change it's width and height even if it has more elements then set size.
        /// Because of that scrolling won't work unless canvas is bounded to this property.
        /// </summary>
        /// <value>The width of the canvas.</value>
        public int CanvasWidth
        {
            get { return (int)GetValue(CanvasWidthProperty); }
            set { SetValue(CanvasWidthProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CanvasWidth.  This enables animation, styling, binding, etc...
        /// <summary>
        /// The canvas width property
        /// </summary>
        public static readonly DependencyProperty CanvasWidthProperty =
            DependencyProperty.Register("CanvasWidth", typeof(int), typeof(MazeDisplay), new PropertyMetadata(300));

        /// <summary>
        /// Gets or sets the height of the canvas.
        /// This property is used since canvas must have initial value, and won't change it's width and height even if it has more elements then set size.
        /// Because of that scrolling won't work unless canvas is bounded to this property.
        /// </summary>
        /// <value>The height of the canvas.</value>
        public int CanvasHeight
        {
            get { return (int)GetValue(CanvasHeightProperty); }
            set { SetValue(CanvasHeightProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CanvasHeight.  This enables animation, styling, binding, etc...
        /// <summary>
        /// The canvas height property
        /// </summary>
        public static readonly DependencyProperty CanvasHeightProperty =
            DependencyProperty.Register("CanvasHeight", typeof(int), typeof(MazeDisplay), new PropertyMetadata(300));



        /// <summary>
        /// Gets or sets the maze.
        /// On set - draws maze
        /// </summary>
        /// <value>The maze.</value>
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

        /// <summary>
        /// Gets or sets the maze string.
        /// On set - tries to parse string to maze.
        /// </summary>
        /// <value>The maze string.</value>
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
        /// <summary>
        /// The maze string property
        /// </summary>
        public static readonly DependencyProperty MazeStrProperty =
            DependencyProperty.Register("MazeStr", typeof(string), typeof(MazeDisplay), new PropertyMetadata(String.Empty));

        // Using a DependencyProperty as the backing store for Maze.  This enables animation, styling, binding, etc...
        /// <summary>
        /// The maze property
        /// </summary>
        public static readonly DependencyProperty MazeProperty =
            DependencyProperty.Register("Maze", typeof(Maze), typeof(MazeDisplay), new UIPropertyMetadata(null, new PropertyChangedCallback( MazeChanged)));
        #endregion

        #region PropertyChanged Events
        /// <summary>
        /// Mazes the changed.
        /// </summary>
        /// <param name="d">The d.</param>
        /// <param name="e">The <see cref="DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        private static void MazeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            MazeDisplay display = (MazeDisplay)d;
            display.DrawMaze();
        }

        /// <summary>
        /// Icons the changed.
        /// </summary>
        /// <param name="d">The d.</param>
        /// <param name="e">The <see cref="DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        private static void IconChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            MazeDisplay display = (MazeDisplay)d;
            display.SetImage();
        }

        // Register the routed event
        /// <summary>
        /// Invoked when solution animation ended.
        /// </summary>
        public static readonly RoutedEvent AnimationEndedEvent =
            EventManager.RegisterRoutedEvent("AnimationEnded", RoutingStrategy.Bubble,
            typeof(RoutedEventHandler), typeof(MazeDisplay));

        // .NET wrapper
        public event RoutedEventHandler AnimationEnded
        {
            add
            {
                AddHandler(AnimationEndedEvent, value);
            }
            remove { RemoveHandler(AnimationEndedEvent, value); }
        }
        #endregion

        /// <summary>
        /// Handles End of game. Disables the user control and invokes end event.
        /// </summary>
        private void ExitReached()
        {
            this.IsEnabled = false;
            PlayerReachedExit?.Invoke(this, EventArgs.Empty);
        }

        #region Initial Drawing related methods
        /// <summary>
        /// Adjusts the size of the canvas.
        /// Calculates Width and Height according to maze's rows and cols.
        /// This is neccessary for scrolling to work, since in order to scroll untill the end of the maze, canvas must be set with
        /// actual size.
        /// </summary>
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

        /// <summary>
        /// Draws the maze.
        /// </summary>
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

        /// <summary>
        /// Adds game exit.
        /// </summary>
        /// <param name="exitPos">The exit position.</param>
        private void AddExitToCanvas(Position exitPos)
        {
            var rect = AddWallToCanvas(exitPos.Row, exitPos.Col);

            ImageBrush imageBrush = new ImageBrush();
            imageBrush.ImageSource = new BitmapImage(new Uri(@"pack://application:,,,/Resources/door.png", UriKind.Absolute));
            rect.Fill = imageBrush;
        }

        /// <summary>
        /// Adds the wall to canvas.
        /// </summary>
        /// <param name="topMargin">The top margin.</param>
        /// <param name="leftMargin">The left margin.</param>
        /// <returns>Rectangle.</returns>
        private Rectangle AddWallToCanvas(int topMargin, int leftMargin)
        {
            var rect = new Rectangle();
            canvas.Children.Add(rect);
            Canvas.SetLeft(rect, leftMargin * baseMargin);
            Canvas.SetTop(rect, topMargin * baseMargin);
            return rect;
        }

        /// <summary>
        /// Sets the image.
        /// </summary>
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
        /// <summary>
        /// Handles key pressed event. Moves the player if movement is not into a wall.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
        internal void KeyPressed(object sender, KeyEventArgs e)
        {
            if (!this.IsEnabled) return;
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

        /// <summary>
        /// Moves the player.
        /// Redraws player in given position.
        /// </summary>
        /// <param name="pos">The position.</param>
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

        /// <summary>
        /// Tries to move player to given position.
        /// </summary>
        /// <param name="newPosition">The new position.</param>
        /// <returns>true if movement is not to a wall or outside of maze range. false otherwise.</returns>
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

        /// <summary>
        /// Tries the move.
        /// </summary>
        /// <param name="direction">The direction.</param>
        public void TryMove(Direction direction)
        {
            this.Dispatcher.Invoke(() =>
            {
                TryMove(GetPositionByDirection(direction));
            });
        }

        /// <summary>
        /// Redraws player in InitialPos.
        /// </summary>
        public void Reset()
        {
            if (!this.IsEnabled)
            {
                this.IsEnabled = true;
            }
            MovePlayer(Maze.InitialPos);
        }

        /// <summary>
        /// Animates the solution.
        /// </summary>
        /// <param name="solution">The solution.</param>
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

        /// <summary>
        /// Initializes the timer.
        /// </summary>
        private void InitTimer()
        {
            if (timer.IsEnabled)
            {
                timer.Stop();
                tick = 0;
                RaiseEvent(new RoutedEventArgs(MazeDisplay.AnimationEndedEvent));
            }
        }

        /// <summary>
        /// Occurres on every tick of solution animation. Moves player to it's new location.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
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

        /// <summary>
        /// Gets the position by direction.
        /// </summary>
        /// <param name="direction">The direction.</param>
        /// <returns>Position.</returns>
        /// <exception cref="System.Exception">Invalid position</exception>
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
