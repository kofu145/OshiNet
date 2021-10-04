using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace OshiNet
{


    public class Server
    {

        TcpListener tcpListener;
        public ushort port = 13000;
        IPAddress localAddr = IPAddress.Any;

        //Dictionary<int, TcpClient> clients = new Dictionary<int, TcpClient>();
        TcpClient[] clients = new TcpClient[99];
        Byte[] bytes = new Byte[256];



        public void Start()
        {
            this.tcpListener = new TcpListener(localAddr, port);

            this.tcpListener.Start();

            byte[] bytes = new byte[256];

            

        }

        public void Run()
        {
            var listeningThread = new Thread(new ThreadStart(this.Listen));
            var syncThread = new Thread(new ThreadStart(this.Sync));

            listeningThread.Start();
            syncThread.Start();



        }

        public void Sync()
        {
            while (true)
            {
                for (int i = 0; i < clients.Length; i++)
                {
                    if (clients[i].GetStream().DataAvailable)
                    {
                        Thread processingThread = new Thread(() => this.ProcessMessage(clients[i]));
                        processingThread.Start();

                    }
                }
            }
        }

        public void Listen()
        {
            int count = 0;

            while (true)
            {
                Console.Write("waiting for a connection");

                TcpClient client = tcpListener.AcceptTcpClient();
                Console.WriteLine("Connected!");

                clients[count] = client;
                count++;

            }
        }

        // Processes any data sent by a client.
        public string ProcessMessage(TcpClient client)
        {
            String data = null;
            int i;

            // Loop to receive all the data sent by the client.
            while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
            {
                // Translate data bytes to a ASCII string.
                data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
                Console.WriteLine("Received: {0}", data);

                // Process the data sent by the client.
                data = data.ToUpper();

                byte[] msg = System.Text.Encoding.ASCII.GetBytes(data);

                // Send back a response.
                stream.Write(msg, 0, msg.Length);
                Console.WriteLine("Sent: {0}", data);
            }


            return data;
        }

        public void Broadcast()
        {

        }



    }
}
