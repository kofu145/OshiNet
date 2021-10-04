using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace OshiNet
{
    class Client
    {
        int port;
        TcpClient tcpClient;
        string ipAddr = "127.0.0.1";


        public void Connect()
        {

            tcpClient = new TcpClient(ipAddr, port);



        }

        public 



    }
}
