using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using MazeLib;
using Newtonsoft.Json.Linq;

namespace Client
{
    class Player
    {
        TcpClient client;

        //private Mode mode;
        private TcpClient ConnectToServer()
        {
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 5555);
            TcpClient client = new TcpClient();
            client.Connect(ep);
            TcpClient cl = new TcpClient();
            cl.Connect(ep);
            return client;
        }

        public void Menu()
        {
            client = ConnectToServer();
            //while (true)
            //{
                Console.WriteLine("Please Select Your Command:");
                bool res = GenerateRequest();
              //  if (!res) break;
            //}
        }

        private bool GenerateRequest()
        {
            
            try
            {
                using (NetworkStream stream = client.GetStream())
                using (StreamReader reader = new StreamReader(stream))
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    while (true)
                    {

                        string line = Console.ReadLine();
                        writer.WriteLine(line);
                        writer.Flush();
                        // Get result from server
                        //StringBuilder sb = new StringBuilder();
                        //while (reader.Peek() > 0)
                        //{
                        //    sb.Append(reader.ReadLine());
                        //}
                        //Maze maze = ParseMaze(sb.ToString());
                        //if (maze != null)
                        //{
                        //    Console.WriteLine("Created Maze Name: {0}, Rows: {1}, Columns: {2}", maze.Name,
                        //        maze.Rows, maze.Cols);
                        //}
                    }
                }
                //client.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("An Error Has Occured");
            }
            finally
            {
                if (client != null)
                {
                    client.Close();
                }
            }
            //if (line.Equals("exit")) return false;
            return true;
        }

        private Maze ParseMaze(String str)
        {
            Maze maze = null;
            try
            {
                JObject mazeObj = JObject.Parse(str);
                maze = new Maze((int)mazeObj["Rows"], (int)mazeObj["Cols"]);
                maze.Name = (string)mazeObj["Name"];

                maze.InitialPos = new Position((int)mazeObj["Start"]["Row"], (int)mazeObj["Start"]["Col"]);
                maze.GoalPos = new Position((int)mazeObj["End"]["Row"], (int)mazeObj["End"]["Col"]);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return maze;
        }

        private bool IsInitialCommandValid(int command)
        {
            return command > 0 && command < 6;
        }
    }
}
