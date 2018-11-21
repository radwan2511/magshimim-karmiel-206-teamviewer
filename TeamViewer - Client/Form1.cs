﻿using System;
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
using System.Windows.Forms;
using System.Security.Cryptography;
using System.IO;

namespace TeamViewer___Client
{

    public partial class Form1 : Form
    {
        public static string MSG { get; set; }
        public static string RecivedMSG { get; set; }
        public static TcpClient client;
        public static NetworkStream clientStream;
        public static byte[] key { get; set; }
        public Form1()
        {
            InitializeComponent();
            Thread c = new Thread(serverConnection);
            c.Start();
            
        }

        private void serverConnection()
        {
            try
            {
                //start the connection
                client = new TcpClient();
                IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Parse(Constants.LOCAL_HOST), Constants.PORT);
                client.Connect(serverEndPoint);
                NetworkStream clientStream = client.GetStream();
                MSG = "hi";
                sendMSG();
                reciveMSG();
                Label tb= new Label();
                this.Controls.Add(tb);
                tb.Top = 50;
                tb.Left = 15;
                tb.Text = RecivedMSG;
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
                Application.Exit();
            }

        }
        public static void sendMSG()
        {
            try
            {
                clientStream = client.GetStream();
                byte[] buffer = new ASCIIEncoding().GetBytes(MSG);
                clientStream.Write(buffer, 0, buffer.Length);
                clientStream.Flush();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }
        private static void reciveMSG()
        {
            try
            {
                byte[] bufferIn = new byte[4];
                int bytesRead = clientStream.Read(bufferIn, 0, 15);
                RecivedMSG = new ASCIIEncoding().GetString(bufferIn);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }
    }
}
