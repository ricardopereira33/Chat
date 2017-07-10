using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Text;

namespace Chat.App_Start.Connection
{
    public class User
    {
        public Room room;
        public IPAddress Ip;
        public NetworkStream stream;

        public User(IPAddress Ip, NetworkStream stream, Room room)
        {
            this.room = room;
            this.stream = stream;
            this.Ip = Ip;
        }

        public void Communicate()
        {
            Byte[] bytes = new Byte[1024];
            int NumBytes;

            while(stream.DataAvailable)
            {
                NumBytes = stream.Read(bytes, 0, bytes.Length);
                String data = Encoding.UTF8.GetString(bytes);
                Console.WriteLine(data);

                room.SendMsg(bytes,Ip);
            }
        }
    }
}