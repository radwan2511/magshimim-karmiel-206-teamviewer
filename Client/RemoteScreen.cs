using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.IO;
using System.Runtime.InteropServices;

namespace TeamViewer___Client
{
    public partial class RemoteScreen : Form
    {
        Socket socket = null;
        Byte[] bytes = new Byte[9999999];
        string ipp = "";

        public RemoteScreen(string ip)
        {
            ipp = ip;
            InitializeComponent();
        }

        private void Screen_Load(object sender, EventArgs e)
        {
            string host = Dns.GetHostName();
            IPHostEntry ip = Dns.GetHostByName(host);


            //
            //function3("10.0.0.5");
            //
            // change ip here
            IPEndPoint iipp = new IPEndPoint(IPAddress.Parse(Constants.IP),1455);
            //

            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //socket.Bind(new IPEndPoint(IPAddress.Parse(ip.AddressList[0].ToString()), 1453));

            socket.Bind(iipp);

            socket.Listen(1);
            socket.BeginAccept(new AsyncCallback(function1), null);
            function3(ip.AddressList[0].ToString());
        }

        void function1(IAsyncResult var)
        {
            Socket sock = socket.EndAccept(var);
            sock.BeginReceive(bytes,0,bytes.Length,SocketFlags.None,new AsyncCallback(function2), sock);
            socket.BeginAccept(new AsyncCallback(function1), null);
        }

        void function2(IAsyncResult var)
        {
            Socket sock = (Socket)var.AsyncState;
            int num = sock.EndReceive(var);
            Byte[] bytes2 = new Byte[num];
            Array.Copy(bytes, bytes2, bytes2.Length);
            
            if(num < 50)
            {
                string st = Encoding.UTF8.GetString(bytes2);
                int width = int.Parse(st.Substring(0, st.IndexOf(':')));
                int height = int.Parse(st.Substring(st.IndexOf(':') + 1, st.IndexOf('|') - st.IndexOf(':') - 1));
                this.Size = new Size(width+16, height+38);
            }
            else
            {
                MemoryStream ms = new MemoryStream(bytes2);
                Image img = Bitmap.FromStream(ms);
                pictureBox1.Image = img;
            }

        }

        void function3(string ip)
        {
            Socket sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            sock.Connect(IPAddress.Parse(ipp), 1453);
            Byte[] bytes3 = Encoding.UTF8.GetBytes(ip +"|AC");
            sock.Send(bytes3);
            //sock.Send(bytes3, 0, bytes3.Length, SocketFlags.None);
            sock.Close();
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            Byte[] bytes3 = null;
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                bytes3 = Encoding.UTF8.GetBytes("Left:MouseDown");
            }
            else if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                bytes3 = Encoding.UTF8.GetBytes("Right:MouseDown");
            }
            else
            {
                return;
            }
            Socket sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            sock.Connect(IPAddress.Parse(ipp), 1453);
            sock.Send(bytes3);
            sock.Close();
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            int x = e.X;
            int y = e.Y;
            Socket sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            sock.Connect(IPAddress.Parse(ipp), 1453);
            Byte[] bytes3 = Encoding.UTF8.GetBytes(x.ToString() + ":" + y.ToString() + "|MouseMove");
            sock.Send(bytes3);
            sock.Close();
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            Byte[] bytes3 = null;
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                bytes3 = Encoding.UTF8.GetBytes("Left:MouseUp");
            }
            else if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                bytes3 = Encoding.UTF8.GetBytes("Right:MouseUp");
            }
            else
            {
                return;
            }
            Socket sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            sock.Connect(IPAddress.Parse(ipp), 1453);
            sock.Send(bytes3);
            sock.Close();
        }

        private void Screen_KeyUp(object sender, KeyEventArgs e)
        {
            Socket sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            sock.Connect(IPAddress.Parse(ipp), 1453);
            Byte[] bytes3 = Encoding.UTF8.GetBytes(e.KeyValue.ToString()+ ":Key");
            sock.Send(bytes3);
            sock.Close();
        }
    }
}
