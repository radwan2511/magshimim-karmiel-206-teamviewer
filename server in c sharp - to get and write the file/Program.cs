using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace server_in_c_sharp
{
    class Program
    {
        static void Main(string[] args)
        {

            IPAddress localAdd = IPAddress.Parse("127.0.0.1");
            TcpListener listener = new TcpListener(localAdd, 8820);
            Console.WriteLine("Listening...");
            listener.Start();
            //---incoming client connected---
            TcpClient client = listener.AcceptTcpClient();
            //---get the incoming data through a network stream---
            NetworkStream nwStream = client.GetStream();
            byte[] buffer = new byte[client.ReceiveBufferSize];
            //---read incoming stream---
            int bytesRead = nwStream.Read(buffer, 0, client.ReceiveBufferSize);

            BinaryWriter binWriter = new BinaryWriter(File.Open("C:\\Users\\magshimim\\Desktop\\movedToHere\\passedpic.jpg", FileMode.CreateNew,FileAccess.ReadWrite));
            binWriter.Write(buffer);
            binWriter.Close();
            listener.Stop();
        }
    }
}
