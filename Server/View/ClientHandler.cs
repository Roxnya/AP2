using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class ClientHandler : IClientHandler
    {
        public void SendResponseToClient(TcpClient client, Result result)
        {
            new Task(() =>
            {
                NetworkStream stream = null;
                //StreamReader reader = null;
                StreamWriter writer = null;
                try
                {
                    stream = client.GetStream();
                    writer = new StreamWriter(stream);
                    while (true)
                    {
                        Console.WriteLine("Sending Response");
                        //Clears all buffers for the current writer and causes
                        //any buffered data to be written to the underlying stream.
                        writer.Flush();
                        writer.WriteLine(result.Json);
                        writer.Flush();
                        if(result.Status == Status.Close)
                        {
                            client.Close();
                            break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    if (stream != null) stream.Dispose();
                    //if (reader != null) stream.Dispose();
                    if (writer != null) stream.Dispose();
                    client.Close();
                }
            }).Start();
        }

        public void HandleClient(TcpClient client, IController controller)
        {
            new Task(() =>
            {
                NetworkStream stream = null;
                StreamReader reader = null;
               // StreamWriter writer = null;
                try
                {
                    stream = client.GetStream();
                    reader = new StreamReader(stream);
                    while (true)
                    {
                        string commandLine = reader.ReadLine();
                        Console.WriteLine("Got command: {0}", commandLine);
                        //Clears all buffers for the current writer and causes
                        //any buffered data to be written to the underlying stream.
                        Status status = controller.ExecuteCommand(commandLine, client);
                        if (status == Status.Close) break;

                    }
                }
                catch(Exception ex)
                {
                    if (stream != null) stream.Dispose();
                    if (reader != null) stream.Dispose();
                    client.Close();
                }
            }).Start();
        }
    }
}