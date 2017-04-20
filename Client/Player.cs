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
        private StreamReader reader;
        private StreamWriter writer;
        private TcpClient client;
        private NetworkStream stream;
        private IPEndPoint ep;
        private int port;

        public Player(int port, string ip)
        {
            this.ep = new IPEndPoint(IPAddress.Parse(ip), 5555);
            this.port = port;
            this.client = ConnectToServer();
            this.reader = new StreamReader(this.stream);
            this.writer = new StreamWriter(this.stream);

        }

        private TcpClient ConnectToServer()
        {
            TcpClient client = new TcpClient();
            client.Connect(this.ep);
            this.stream = client.GetStream();
            return client;
        }

        public void Handle()
        {
            Action a = new Action(Listen);
            Task task = new Task(a);

        }


        public void Listen()
        {

        }
    }
}















 /*       //private Mode mode;
        private TcpClient ConnectToServer()
        {
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 5555);
            TcpClient client = new TcpClient();
            client.Connect(ep);
            return client;
        }

        public void Menu()
        {
            int option = GetCommand();

            while (option != 0)
            {
                switch (option)
                {
                    case 1:
                        //need connect
                        GenerateRequest();
                        break;
                    case 2:
                        //need connect
                        SolveRequest();
                        break;
                    case 3:
                        //need connect
                        StartRequest();
                        break;
                    case 4:
                        //doesnt!!!
                        ListRequest();
                        break;
                    case 5:
                        //need connect
                        JoinRequest();
                        break;
                    //all the others doesnt need connect , it creates a new tcp !!!
                

                }
                option = GetCommand();
            }
        }

        private int GetCommand()
        {
            Console.WriteLine("Please Select Your Action Number:");
            Console.WriteLine("0.exit\n1.generate\n2.solve\n3.start\n4.list\n5.join");
            string line = Console.ReadLine();
            int option;
            bool isNum = int.TryParse(line, out option);
            while (!isNum || !IsInitialCommandValid(option))
            {
                Console.WriteLine("Invalid Input, Please Insert A Valid Option");
                Console.WriteLine("1.generate\n2.solve\n3.start\n4.list\n5.join\n");
                isNum = int.TryParse(line, out option);
            }
            return option;
        }

        private void GenerateRequest()
        {
            Console.WriteLine("Insert maze name, number of rows, number of columns:");
            string line = Console.ReadLine();

            TcpClient client = null;
            try
            {
                client = ConnectToServer();
                using (NetworkStream stream = client.GetStream())
                using (StreamReader reader = new StreamReader(stream))
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.WriteLine("generate " + line);
                    writer.Flush();
                    // Get result from server
                    StringBuilder sb = new StringBuilder();
                    while (reader.Peek() > 0)
                    {
                        sb.Append(reader.ReadLine());
                    }
                    if (sb.ToString() == "Error. Game name already exists")
                    {
                        Console.WriteLine("Error. Game name already exists");
                    }
                    else
                    {
                        JObject mazeObj = JObject.Parse(sb.ToString());
                        string mazeRep = (string)mazeObj["Maze"];
                        Maze maze = ParseMaze(sb.ToString());
                        //Maze maze = ParseResult(sb);
                        if (maze != null)
                        {
                            Console.WriteLine("Created Maze Name: {0}, Rows: {1}, Columns: {2}, Maze representation: {3}", maze.Name,
                                maze.Rows, maze.Cols, mazeRep);
                        }
                    }
            
                }
                client.Close();
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

        }


        private void SolveRequest()
        {
            Console.WriteLine("Insert maze name and type of algorithm:");
            string line = Console.ReadLine();

            TcpClient client = null;
            try
            {
                client = ConnectToServer();
                using (NetworkStream stream = client.GetStream())
                using (StreamReader reader = new StreamReader(stream))
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.WriteLine("solve " + line);
                    writer.Flush();
                    // Get result from server
                    StringBuilder sb = new StringBuilder();
                    while (reader.Peek() > 0)
                    {
                        sb.Append(reader.ReadLine());
                    }

                    JObject mazeObj = JObject.Parse(sb.ToString());
                    string name = (string)mazeObj["name"];
                    string solution = (string)mazeObj["path"];
                    int nodesEvaluated = (int)mazeObj["NodesEvaluated"];
                    //maybe need to check of the strings are not empty
                    Console.WriteLine("Name: {0}, Solution: {1}, NodesEvaluated: {2} ", name, solution, nodesEvaluated);

                }
                client.Close();
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

        }

        private void ListRequest()
        {
            TcpClient client = null;
            try
            {
                client = ConnectToServer();
                using (NetworkStream stream = client.GetStream())
                using (StreamReader reader = new StreamReader(stream))
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.WriteLine("list");
                    writer.Flush();
                    // Get result from server
                    StringBuilder sb = new StringBuilder();
                    while (reader.Peek() > 0)
                    {
                        sb.Append(reader.ReadLine());
                    }

                    JObject mazeObj = JObject.Parse(sb.ToString());
                    //print the list here
                }
                client.Close();
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

        }

        private void StartRequest()
        {
            Console.WriteLine("Insert maze name, number of rows, number of columns:");
            string line = Console.ReadLine();
            TcpClient client = null;

            try
            {
                client = ConnectToServer();
                using (NetworkStream stream = client.GetStream())
                using (StreamReader reader = new StreamReader(stream))
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.WriteLine("start " + line);
                    writer.Flush();
                    // Get result from server
                    StringBuilder sb = new StringBuilder();
                    while (reader.Peek() > 0)
                    {
                        sb.Append(reader.ReadLine());
                    }


                    //JObject mazeObj = JObject.Parse(sb.ToString());
                    //string mazeRep = (string)mazeObj["Maze"];
                    //Maze maze = ParseMaze(sb.ToString());
                    //if (maze != null)
                    //{
                        Console.WriteLine("Created Maze Name: {0}, Rows: {1}, Columns: {2}, Maze representation: {3}", maze.Name,
                            maze.Rows, maze.Cols, mazeRep);
                    }

               }
                client.Close();
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

        }

        private void JoinRequest()
        {
            Console.WriteLine("Insert maze name:");
            string line = Console.ReadLine();

            TcpClient client = null;
            try
            {
                client = ConnectToServer();
                using (NetworkStream stream = client.GetStream())
                using (StreamReader reader = new StreamReader(stream))
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.WriteLine("join " + line);
                    writer.Flush();
                    // Get result from server
                    StringBuilder sb = new StringBuilder();
                    while (reader.Peek() > 0)
                    {
                        sb.Append(reader.ReadLine());
                    }

                    JObject mazeObj = JObject.Parse(sb.ToString());
                    string mazeRep = (string)mazeObj["Maze"];
                    Maze maze = ParseMaze(sb.ToString());
                    if (maze != null)
                    {
                        Console.WriteLine("Created Maze Name: {0}, Rows: {1}, Columns: {2}, Maze representation: {3}", maze.Name,
                            maze.Rows, maze.Cols, mazeRep);
                    }

                }
                client.Close();
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

        }

        private Maze ParseMaze(String str)
        {
            Maze maze = null;
            try
            {
                JObject mazeObj = JObject.Parse(str);
                maze = new Maze((int)mazeObj["Rows"], (int)mazeObj["Cols"]);
                string mazeRep = (string)mazeObj["Maze"];
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
*/