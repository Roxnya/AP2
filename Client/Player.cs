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
    class Player
    {
        private TcpClient client;
        private BinaryReader reader;
        private BinaryWriter writer;
        private NetworkStream stream;
        private int command;
        private IPEndPoint ep;
        private int port;
        private bool keepCom;
        private string message;
        private bool result;

        public Player(int port, string ip)
        {
            this.ep = new IPEndPoint(IPAddress.Parse(ip), 5555);
            this.port = port;
        }

        private void ConnectToServer()
        {
            TerminateConnection();
            this.client = new TcpClient();
            client.Connect(ep);
            stream = client.GetStream();
            writer = new BinaryWriter(client.GetStream());
            reader = new BinaryReader(client.GetStream());
        }

        public void Handle()
        {
            Console.WriteLine("Please enter command...");
            message = Console.ReadLine();
            string[] words = message.Split();
            Option(words[0]);

            while (this.command != 0)
            {
                EstablishConnection();
                writer.Write(message);
                if (HandleCommand())
                {
                    continue;
                }
                message = Console.ReadLine();
                words = message.Split();
                Option(words[0]);
            }
        }

        private bool HandleCommand()
        {
            string s = reader.ReadString();
            Console.WriteLine(s);
            if (s.Contains("error")) return false;
            if (!IsCommandSinglePlayer())
            {
                //if the command is start/join init multiplayer flow
                return HandleMultiplayerFlow();
            }
            return false;
        }

        private bool HandleMultiplayerFlow()
        {
            Task task = new Task(Listen);
            task.Start();
            string[] words;
            try
            {
                do
                {
                    message = Console.ReadLine();
                    words = message.Split();
                    Option(words[0]);
                    if (client != null)
                    {
                        writer.Write(message);
                    }
                    else
                    {
                        result = true;
                    }
                }
                while (keepCom);
            }
            catch (Exception)
            {
                result = false;
            }
            return result;
        }

        private bool IsCommandSinglePlayer()
        {
            //if command is not join or start - we are inside the main while loop and still in single player mode
            //in which close/play are invalid anyways.
            return (this.command != 3 && this.command != 5);
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

        private void EstablishConnection()
        {
            if (command == -1 || command == 1 || command == 2 || command == 3 || command == 4 || command == 5 || command == 0)
            {
                ConnectToServer();
            }
        }

        public void Listen()
        {
            keepCom = true;
            do
            {
                string s = reader.ReadString();
                Console.WriteLine(s);
                if (s.Equals("close"))
                {
                    keepCom = false;
                    TerminateConnection();
                }
            } while (keepCom);
        }

        private void Option(string command)
        {
            switch (command)
            {
                case "generate":
                    this.command = 1;
                    break;
                case "solve":
                    this.command = 2;
                    break;
                case "start":
                    this.command = 3;
                    break;
                case "list":
                    this.command = 4;
                    break;
                case "join":
                    this.command = 5;
                    break;
                case "play":
                    this.command = 6;
                    break;
                case "close":
                    this.command = 0;
                    break;
                default:
                    this.command = -1;
                    break;
            }
            
        }



    }
}




