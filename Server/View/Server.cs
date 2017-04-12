using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class Server : IView
    {
        private int port;
        private TcpListener listener;
        private IClientHandler ch;
        private GameData data;

        public Server(int port, IClientHandler ch)
        {
            this.port = port;
            this.ch = ch;
            this.data = new GameData();
        }

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
                        IController controller = new Controller();
                        IModel model = new MazeModel(controller, this.data);
                        controller.SetModel(model);
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
        public void Stop()
        {
            listener.Stop();
        }

    }


}
