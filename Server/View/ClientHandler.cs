using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server.View
{
    /// <summary>
    /// Client handler class.
    /// </summary>
    class ClientHandler : IClientHandler
    {
        private TcpClient client;
        private BinaryWriter writer = null;
        private BinaryReader reader = null;
        private NetworkStream stream = null;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="client">client to handle</param>
        public ClientHandler(TcpClient client)
        {
            this.client = client;
            this.stream = client.GetStream();
            this.writer = new BinaryWriter(stream);
        }

        /// <summary>
        /// Sending response to the client
        /// </summary>
        /// <param name="result"></param>
        public void SendResponseToClient(Result result)
        {
            //creating a new task for sending response
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
                catch (Exception ex)
                {
                    HandleTermination();
                }
            }).Start();
        }

        /// <summary>
        /// Handling the client
        /// </summary>
        /// <param name="controller">controller</param>
        public void HandleClient(IController controller)
        {
            //creating a new task for handling a client
            new Task(() =>
            {
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
                    }
                }
                catch(Exception ex)
                {
                   if (client != null && writer != null)
                    {
                        writer.Flush();
                        writer.Write(ex.Message);
                    }
                    HandleTermination();
                }
            }).Start();
        }

        /// <summary>
        /// Handle termination
        /// </summary>
        private void HandleTermination()
        {
            //disposing stream and reader/writer streamers
            if (stream != null) stream.Dispose();
            if (reader != null) reader.Dispose();
            if (writer != null) writer.Dispose();
            if (client != null)
            {
                client.Close();
                client = null;
            }
        }
    }
}