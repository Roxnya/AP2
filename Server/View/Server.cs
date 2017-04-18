using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    /// <summary>
    /// A Class for server side communication. Starts server. Delegates client handling to view.
    /// </summary>
    class Server
    {
        private int port;
        private TcpListener listener;
        private IClientHandler ch;
        private GameData data;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="port">Server's port through which clients will connect</param>
        /// <param name="ch">View class that will handle clients</param>
        public Server(int port, IClientHandler ch)
        {
            this.port = port;
            this.ch = ch;
            //initialize game's content class (maze list, solutions, game romms..)
            this.data = new GameData();
        }

        /// <summary>
        /// Initializes Server - listents to incoming connections.
        /// Upon client communication - delegates client handling to IClientHandler.
        /// </summary>
        public void Start()
        {
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), port);
            listener = new TcpListener(ep);

            listener.Start();
            Console.WriteLine("Waiting for connections...");
            Task task = new Task(() => {
                while (true)
                {
                    try
                    {
                        TcpClient client = listener.AcceptTcpClient();
                        Console.WriteLine("Got new connection");
                        //Init controller and model for new specific client
                        IController controller = new Controller(client);
                        IModel model = new MazeModel(controller, this.data);
                        controller.SetModel(model);

                        //delegate client handling
                        ch.HandleClient(client, controller);
                    }
                    catch (SocketException ex)
                    {
                        Console.WriteLine("Error In Server Start\n" + ex);
                        break;
                    }
                }
                Console.WriteLine("Server stopped");
            });
            task.Start();
        }

        /// <summary>
        /// Terminates Server - stops listenning to incoming connection's
        /// </summary>
        public void Stop()
        {
            listener.Stop();
        }

    }


}
