using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Web;

namespace app
{
    public class Connection
    {
        public void Connect()
        {
            Room room = new Room();

            try
            {
                //setup and start server
                TcpListener server = new TcpListener(IPAddress.Any, 3333);
                server.Start();

                //accept clients
                TcpClient client;

                while ((client = server.AcceptTcpClient()) != null)
                {
                    Console.WriteLine("entrei");
                    //get stream and regist user in the room

                    NetworkStream stream = client.GetStream();
                    IPAddress ip = ((IPEndPoint)client.Client.RemoteEndPoint).Address;
                    room.Regist(ip, stream);

                    //creat user and join in the communication
                    User user = new User(ip, stream, room);
                    Thread thread = new Thread(new ThreadStart(user.Communicate));
                    thread.Start();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Erro "+ e.ToString());
            }
        }
    }
}