using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Web;

namespace Chat.App_Start.Connection
{
    public class Connection
    {
        
        public void Connect()
        {
            Room room = new Room();

            //setup and start server
            TcpListener server = new TcpListener(IPAddress.Any, 3333);
            server.Start();
            System.Diagnostics.Debug.WriteLine("passei");
            //accept clients
            TcpClient client;

            while ((client = server.AcceptTcpClient()) != null)
            {
                System.Diagnostics.Debug.WriteLine("entrei");
                //get stream and regist user in the room
                NetworkStream stream = client.GetStream();
                IPAddress ip = ((IPEndPoint)client.Client.RemoteEndPoint).Address;
                room.Regist(ip,stream);

                //creat user and join in the communication
                User user = new User(ip, stream, room);
                Thread thread = new Thread(new ThreadStart(user.Communicate));
                thread.Start();
            }
        }
    }
}