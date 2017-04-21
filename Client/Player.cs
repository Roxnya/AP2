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
        TcpClient client;
        BinaryReader reader;
        BinaryWriter writer;
        NetworkStream stream;
        int command;
        private IPEndPoint ep;
        private int port;

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
            string s = Console.ReadLine();
            string[] words = s.Split();
            string command = words[0];
            Option(command);
            while (this.command != 0)
            {
                EstablishConnection();
                Task task = new Task(Listen);
                task.Start();
                writer.Write(s);
                s = Console.ReadLine();
                EstablishConnection();
                words = s.Split();
                command = words[0];
                Option(command);

            }
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
            if(command == 1 || command == 2 || command == 3 || command == 4 || command == 5 || command == 0)
            ConnectToServer();
        }

        public void Listen()
        {
            bool KeepCom = true;
            do
            {
                StringBuilder sb = new StringBuilder();
                string s = reader.ReadString();
                Console.WriteLine(s);
                Console.WriteLine(sb.ToString());

                switch (this.command)
                {
                    case 1:
                        KeepCom = false;
                        break;

                    case 2:
                        KeepCom = false;
                        break;

                    case 4:

                        KeepCom = false;
                        break;

                    case 0:
                        KeepCom = false;
                        break;

                    default:
                        break;

                }
            } while (KeepCom);
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
            }
            
        }



    }
}




