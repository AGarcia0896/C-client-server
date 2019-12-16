using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            // Creating the Socket Listener
            IPAddress ip = Dns.GetHostEntry("localhost").AddressList[0];
            TcpListener server = new TcpListener(ip, 8080);
            TcpClient client = default(TcpClient);

            try
            {
                server.Start();
                Console.WriteLine("Server started...");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Console.Read(); // Wait until you press any key
            }

            while (true)
            {
                client = server.AcceptTcpClient();
                byte[] receivedBuffer = new byte[100];
                NetworkStream stream = client.GetStream();

                stream.Read(receivedBuffer, 0, receivedBuffer.Length);

                // convert bytes to string
                //string msg = Encoding.ASCII.GetString(receivedBuffer, 0, receivedBuffer.Length);
                StringBuilder msg = new StringBuilder();
                foreach(byte b in receivedBuffer)
                {
                    if (b.Equals(00))
                    {
                        break;
                    }
                    else
                        msg.Append(Convert.ToChar(b).ToString());
                }

                Console.WriteLine(msg.ToString() + msg.Length);
            }
        }
    }
}
