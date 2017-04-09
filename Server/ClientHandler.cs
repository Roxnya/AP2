using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    /**
     * A class for handling clients - our view in mvc
     **/
    class ClientHandler : IClientHandler
    {
        public void HandleClient(TcpClient client, Controller controller)
        {
            new Task(() =>
            {
                using (NetworkStream stream = client.GetStream())
                using (StreamReader reader = new StreamReader(stream))
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    string commandLine = reader.ReadLine();
                    Console.WriteLine("Got command: {0}", commandLine);
                    string result = controller.ExecuteCommand(commandLine, client);
                    writer.WriteLine(result);
                    writer.Flush();
                }
                //client.Close();
            }).Start();
        }
    }
}
