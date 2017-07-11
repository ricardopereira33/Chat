using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Text;
using System.Text.RegularExpressions;
using System.Security.Cryptography;

namespace app
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
            Console.WriteLine("new user");
            while (true)
            {
                while (stream.DataAvailable)
                {
                    Console.WriteLine("read");
                    NumBytes = stream.Read(bytes, 0, bytes.Length);
                    String data = Encoding.UTF8.GetString(bytes);
                    Console.WriteLine(data);

                    if (new Regex("^GET").IsMatch(data))
                    {
                        String text = "HTTP/1.1 101 Switching Protocols" + Environment.NewLine
                            + "Connection: Upgrade" + Environment.NewLine
                            + "Upgrade: websocket" + Environment.NewLine
                            + "Sec-WebSocket-Accept: " + Convert.ToBase64String(
                                SHA1.Create().ComputeHash(
                                    Encoding.UTF8.GetBytes(
                                        new Regex("Sec-WebSocket-Key: (.*)").Match(data).Groups[1].Value.Trim() + "258EAFA5-E914-47DA-95CA-C5AB0DC85B11"
                                    )
                                )
                            ) + Environment.NewLine
                            + Environment.NewLine;

                        Byte[] response = Encoding.UTF8.GetBytes(text);

                        Console.WriteLine(text);
                        //stream.Write(response,0,response.Length);
                        room.SendMsg(response,Ip);
                    }
                    else
                    {
                        room.SendMsg(bytes, Ip);
                        bytes = new Byte[1024];
                    }
                }
            }
        }
    }
}