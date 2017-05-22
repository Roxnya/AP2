using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace Server
{
    /// <summary>
    /// Program class
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            //get port from configuration
            int port = Int32.Parse(ConfigurationManager.AppSettings["Server_Port"]);
            string ip = ConfigurationManager.AppSettings["Server_Ip"];
            Server s = new Server(ip, port);
            s.Start();
            Console.WriteLine("Press any key to kill server");
            Console.ReadLine();
            s.Stop();
        }
    }
}
