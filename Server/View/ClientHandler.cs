using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server.View
{
    class ClientHandler : IClientHandler
    {
        private TcpClient client;
        private BinaryWriter writer = null;
        private BinaryReader reader = null;
        private NetworkStream stream = null;

        public ClientHandler(TcpClient client)
        {
            this.client = client;
            this.stream = client.GetStream();
            this.writer = new BinaryWriter(stream);
        }

        public void SendResponseToClient(Result result)
        {
            new Task(() =>
            {
                try
                {
                    Console.WriteLine("Sending Response");
                    //Clears all buffers for the current writer and causes
                    //any buffered data to be written to the underlying stream.
                    writer.Flush();
                    writer.Write(result.Json);
                    writer.Flush();
                    if(result.Status == Status.Close)
                    {
                        HandleTermination();
                    }
                }
                catch (Exception)
                {
                    HandleTermination();
                }
            }).Start();
        }

        public void HandleClient(IController controller)
        {
            new Task(() =>
            {
               // StreamWriter writer = null;
                try
                {
                    this.reader = new BinaryReader(stream);
                    while (client != null)
                    {
                        string commandLine = reader.ReadString();
                        Console.WriteLine("Got command: {0}", commandLine);
                        //Clears all buffers for the current writer and causes
                        //any buffered data to be written to the underlying stream.
                        Status status = controller.ExecuteCommand(commandLine, client);
                        if (status == Status.Close)
                        {
                            HandleTermination();
                        }
                    }
                }
                catch(Exception)
                {
                    HandleTermination();
                }
            }).Start();
        }

        private void HandleTermination()
        {
            if (stream != null) stream.Dispose();
            if (reader != null) stream.Dispose();
            if (writer != null) stream.Dispose();
            if (client != null)
            {
                client.Close();
                client = null;
            }
        }
    }
}