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
        TcpClient client;

        public ClientHandler(TcpClient client)
        {
            this.client = client;
        }

        public void SendResponseToClient(Result result)
        {
            new Task(() =>
            {
                NetworkStream stream = null;
                //StreamReader reader = null;
                BinaryWriter writer = null;
                try
                {
                    stream = client.GetStream();
                    writer = new BinaryWriter(stream);
                    while (true)
                    {
                        Console.WriteLine("Sending Response");
                        //Clears all buffers for the current writer and causes
                        //any buffered data to be written to the underlying stream.
                        //writer.Flush();
                        writer.Write(result.Json);
                        //writer.Flush();
                        if(result.Status == Status.Close)
                        {
                            client.Close();
                            break;
                        }
                    }
                }
                catch (Exception)
                {
                    if (stream != null) stream.Dispose();
                    //if (reader != null) stream.Dispose();
                   // if (writer != null) stream.Dispose();
                    client.Close();
                }
            }).Start();
        }

        public void HandleClient(IController controller)
        {
            new Task(() =>
            {
                NetworkStream stream = null;
                BinaryReader reader = null;
               // StreamWriter writer = null;
                try
                {
                    stream = client.GetStream();
                    reader = new BinaryReader(stream);
                    while (true)
                    {
                        string commandLine = reader.ReadString();
                        Console.WriteLine("Got command: {0}", commandLine);
                        //Clears all buffers for the current writer and causes
                        //any buffered data to be written to the underlying stream.
                        Status status = controller.ExecuteCommand(commandLine, client);
                        if (status == Status.Close) break;

                    }
                }
                catch(Exception)
                {
                    if (stream != null) stream.Dispose();
                    //if (reader != null) stream.Dispose();
                    client.Close();
                }
            }).Start();
        }
    }
}