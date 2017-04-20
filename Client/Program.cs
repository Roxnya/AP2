using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            //get port and ip from configuration
            int port = Int32.Parse(ConfigurationManager.AppSettings["Server_Port"]);
            string ip = ConfigurationManager.AppSettings["Server_Ip"];
            Player p = new Player(port, ip);
        }
    }
}
