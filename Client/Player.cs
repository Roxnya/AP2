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
        Boolean KeepCom;
        int command;
        private IPEndPoint ep;
        private int port;

        public Player(int port, string ip)
        {
            this.ep = new IPEndPoint(IPAddress.Parse(ip), 5555);
            this.port = port;
            this.client = ConnectToServer();
            KeepCom = true;

            writer = new BinaryWriter(client.GetStream());
            reader = new BinaryReader(client.GetStream());
        }
        private TcpClient ConnectToServer()
        {
            TcpClient client = new TcpClient();
            client.Connect(ep);
            return client;
        }
        public void Handle()
        {
            Action a = new Action(Listen);
            Task task = new Task(a);
            task.Start();

            Console.WriteLine("Please enter command...");
            string s = Console.ReadLine();
            string[] words = s.Split();
            string command = words[0];
            Option(command);
            while (KeepCom)
            {
                
                writer.Write(s);
                s = Console.ReadLine();

                words = s.Split();
                command = words[0];
                Option(command);

            }

            task.Wait();
        }

        public void Listen()
        {

            do
            {

                string s = reader.ReadString();
                Console.WriteLine(s);

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
            Console.WriteLine("Connection is closed. Press enter to continue...");
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




