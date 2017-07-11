using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Sockets;

namespace app
{
    public class Room
    {
        public Dictionary<IPAddress, NetworkStream> clients;

        public Room()
        {
            clients = new Dictionary<IPAddress, NetworkStream>();
        }

        public void Regist(IPAddress ip, NetworkStream stream)
        {
            if(!clients.ContainsKey(ip))
                clients.Add(ip, stream);
        }

        public void SendMsg(Byte[] msg, IPAddress ip)
        {
            foreach(KeyValuePair < IPAddress, NetworkStream > entry in clients)
            {
                Console.WriteLine(ip); 
                Console.WriteLine(clients.Count);
                if (!entry.Key.Equals(ip))
                {
                    Console.WriteLine(ip);
                    entry.Value.Write(msg,0,msg.Length);
                }
            }
        }

    }
}