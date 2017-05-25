using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Newtonsoft.Json;
using MazeLib;
using Newtonsoft.Json.Linq;

namespace Client
{
    public class Player
    {
        //connection members
        private int port;
        private TcpClient client;
        private IPEndPoint ep;
        //read/write members
        private NetworkStream stream;
        private BinaryWriter writer;
        private BinaryReader reader;
        //flow members
        private enum Command { ERROR = -1, GENERATE = 0, SOLVE, LIST, START, JOIN, PLAY, CLOSE };
        private bool isMultiPlayerFlow;
        private bool isConsole;
        private Queue<string> msgQueue;
        private Command command;
        //delegates
        public delegate void MazeChangedHandler(MazeEventArgs e);
        public delegate void SolutionChangedHandler(SolutionEventArgs e);
        public delegate void GameListChangedHandler(GameListEventArgs e);
        public delegate void EnemyMovedHandler(EnemyMovedEventArgs e);
        //parsing events
        public event MazeChangedHandler MazeChanged;
        public event SolutionChangedHandler SolutionChanged;
        public event GameListChangedHandler GamesListChanged;
        public event EnemyMovedHandler EnemyPositionChanged;
        public event EventHandler GameClosed;
        public event EventHandler CommunicationError;

        public Player(int port, string ip, bool isConsole)
        {
            this.ep = new IPEndPoint(IPAddress.Parse(ip), 5555);
            this.port = port;
            this.isMultiPlayerFlow = false;
            this.isConsole = isConsole;
            this.msgQueue = new Queue<string>();
            this.command = Command.ERROR;
        }

        public void InitFlow()
        {
            new Task(Send).Start();
        }

        private void Send()
        {
            try
            {
                string msg = PerformMessageHandling();
                ValidateMessage();
                var listenTask = new Task(Listen);
                listenTask.Start();

                while (true)
                {

                    writer.Write(msg);
                    msg = PerformMessageHandling();
                    ValidateMessage();
                    if (listenTask.Status != TaskStatus.Running)
                    {
                        listenTask = new Task(Listen);
                        listenTask.Start();
                    }

                }
            }
            catch (Exception ex)
            {
                if (isConsole)
                    Console.WriteLine(ex.Message);
                else
                    CommunicationError?.Invoke(this, EventArgs.Empty);
            }
        }

        private void Listen()
        {
            try
            {
                while (true)
                {
                    string s = reader.ReadString();

                    if (isConsole)
                        Console.WriteLine(s);
                    else
                        ParseResponse(s);
                    if (!isMultiPlayerFlow)
                        break;
                }
            }
            catch (Exception e)
            {
                if (isConsole)
                    Console.WriteLine(e.Message);
                else
                    CommunicationError?.Invoke(this, EventArgs.Empty);
            }
        }

        #region Message Validation and Handling
        private string PerformMessageHandling()
        {
            string msg = GetMessage();
            string msgArg = msg.Split(' ')[0];
            ParseMessageToCommand(msgArg);
            EstablishConnection();
            return msg;
        }

        private void EstablishConnection()
        {
            if (ShouldResetConnection())
            {
                ConnectToServer();
            }
        }

        private bool ShouldResetConnection()
        {
            return ((command >= Command.ERROR && command <= Command.JOIN)
                //if it's not multiplayer flow and play/close was inserted(invalid)
                || (!isMultiPlayerFlow && (command == Command.PLAY || command == Command.CLOSE)));
        }

        private void ValidateMessage()
        {
            if (!isMultiPlayerFlow && (command == Command.JOIN || command == Command.START))
                isMultiPlayerFlow = true;
            if (isMultiPlayerFlow && command == Command.CLOSE)
                isMultiPlayerFlow = false;
        }

        private string GetMessage()
        {
            if (isConsole)
            {
                Console.WriteLine("Please enter command...");
                return Console.ReadLine();
            }
            else
            {
                while (msgQueue.Count == 0)
                {
                    Thread.Sleep(200);
                }
                return msgQueue.Dequeue();
            }
        }

        public void InjectCommand(string command)
        {
            msgQueue.Enqueue(command);
        }
        #endregion

        #region Message To Server Parsing Region
        private void ParseMessageToCommand(string command)
        {
            switch (command)
            {
                case "generate":
                    this.command = Command.GENERATE;
                    break;
                case "solve":
                    this.command = Command.SOLVE;
                    break;
                case "start":
                    this.command = Command.START;
                    break;
                case "list":
                    this.command = Command.LIST;
                    break;
                case "join":
                    this.command = Command.JOIN;
                    break;
                case "play":
                    this.command = Command.PLAY;
                    break;
                case "close":
                    this.command = Command.CLOSE;
                    break;
                default:
                    this.command = Command.ERROR;
                    break;
            }

        }

        private void ParseResponse(string json)
        {
            try
            {
                if (json[0] == '[')
                {
                    var args = new GameListEventArgs(JsonConvert.DeserializeObject<List<string>>(json));
                    GamesListChanged?.Invoke(args);
                    return;
                }
                else if (json == "close")
                {
                    GameClosed?.Invoke(this, EventArgs.Empty);
                    return;
                }
                var jObj = JObject.Parse(json);
                if (jObj.Property("Maze") != null)
                {
                    var args = new MazeEventArgs(Maze.FromJSON(json));
                    MazeChanged?.Invoke(args);
                }
                else if (jObj.Property("Solution") != null)
                {
                    var args = new SolutionEventArgs(jObj.Property("Solution").Value.ToString(),
                                                    jObj.Property("Name").Value.ToString());
                    SolutionChanged?.Invoke(args);
                }
                else if (jObj.Property("Direction") != null)
                {
                    Direction dir;
                    if (TryParseToDirection(jObj.Property("Direction").Value.ToString(), out dir))
                    {
                        var args = new EnemyMovedEventArgs(dir, jObj.Property("Name").Value.ToString());
                        EnemyPositionChanged?.Invoke(args);
                    }
                }
            }
            catch (Exception)
            { }
        }
        #endregion

        #region Connection Region
        private void ConnectToServer()
        {
            TerminateConnection();
            this.client = new TcpClient();
            client.Connect(ep);
            stream = client.GetStream();
            writer = new BinaryWriter(client.GetStream());
            reader = new BinaryReader(client.GetStream());
        }

        private void TerminateConnection()
        {
            if (reader != null) reader.Dispose();
            if (writer != null) writer.Dispose();
            if (this.stream != null) this.stream.Dispose();

            if (this.client != null)
            {
                this.client.Close();
                this.client = null;
            }
        }
        #endregion

        private bool TryParseToDirection(string value, out Direction dir)
        {
            bool result = true;
            switch (value)
            {
                case "up":
                    dir = Direction.Up;
                    break;
                case "down":
                    dir = Direction.Down;
                    break;
                case "left":
                    dir = Direction.Left;
                    break;
                case "right":
                    dir = Direction.Right;
                    break;
                default:
                    dir = Direction.Unknown;
                    result = false;
                    break;
            }
            return result;
        }
    }
}




