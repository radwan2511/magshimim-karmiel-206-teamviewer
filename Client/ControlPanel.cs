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
using static TeamViewer___Client.Program;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Runtime.InteropServices;

namespace TeamViewer___Client
{
    public partial class ControlPanel : Form
    {
        //public static TcpClient client;
        //private PictureBox pictureBox2;
        //public static NetworkStream clientStream;

        public static int x { get; set; }
        public static int y { get; set; }


        public ControlPanel()
        {
            InitializeComponent();
            Thread c = new Thread(serverConnection);
            c.Start();

        }

        private void serverConnection()
        {
            try
            {

                /////////////////////////////////////////
                byte[] b = new byte[100];
                //start the connection
                //client = new TcpClient();
                //IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Parse(Constants.LOCAL_HOST), Constants.PORT);
                //client.Connect(serverEndPoint);
                LogInScreen.clientStream = LogInScreen.client.GetStream();

                LogInScreen.clientStream.Read(b, 0, 100);
                System.Text.UTF8Encoding enc = new System.Text.UTF8Encoding();

                string result = enc.GetString(b);

                //int port = 13000;

                // string hostName = Dns.GetHostName();
                //string myIP = Dns.GetHostByName(hostName).AddressList[0].ToString();

                string st = "aa";
                string iip = "";
                while (result[0] == 'a')
                {
                    //clientStream.Write(Encoding.ASCII.GetBytes(myIP),0,myIP.Length);

                    LogInScreen.clientStream.Read(b, 0, 16);
                    st = enc.GetString(b);
                    int i = 0;

                    while (System.Char.IsDigit(st[i]) || st[i] == '.')
                    {
                        iip = iip + st[i];
                        i++;
                    }

                    TcpClient cl = new TcpClient();
                    cl.Connect(iip, 8000);
                    NetworkStream clientStream2;
                    while (1 == 1)
                    {
                        try
                        {
                            string massage = "210140{H}";
                            //sending the mouse coordinates
                            BinaryFormatter binFormatter = new BinaryFormatter();
                            clientStream2 = cl.GetStream();
                            binFormatter.Serialize(clientStream2, massage);
                        }
                        catch (Exception exception)
                        {
                            MessageBox.Show(exception.Message);

                            Application.Exit();
                        }
                    }
                }



                string hostName = Dns.GetHostName();
                string myIP = Dns.GetHostByName(hostName).AddressList[0].ToString();


                while (result[0] == 'r')
                {
                    LogInScreen.clientStream.Write(Encoding.ASCII.GetBytes(myIP), 0, myIP.Length);


                    TcpClient client3 = new TcpClient();
                    TcpListener listener = new TcpListener(8000); //crashes and tells that only one port connection is premitted
                    listener.Start();
                    //---get the incoming data through a network stream---
                    NetworkStream ImageStream;
                    string newOne;

                    //Listening = new Thread(StartListening);
                    //GetImage = new Thread(ReceiveImage);


                    while (!client3.Connected)
                    {
                        listener.Start();
                        client3 = listener.AcceptTcpClient();
                    }


                    BinaryFormatter binFormatter = new BinaryFormatter();
                    while (client3.Connected)
                    {
                        ImageStream = client3.GetStream();
                        newOne = (string)binFormatter.Deserialize(ImageStream);
                        update(newOne.Substring(0, 3), newOne.Substring(3, 3));
                        movment();
                        //label1.Invoke(new Action(() => label1.Text = newOne));
                        click();
                        SendKeys.SendWait(newOne.Substring(6, 3));
                        //SendKeys.SendWait("^%{DEL}");
                        //SendKeys.SendWait("{%}");
                        //SendKeys.SendWait("(DEL)");
                    }


                    listener.Stop();
                }



            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);

                Application.Exit();
            }
        }

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool SetCursorPos(int x, int y);

        [DllImport("user32.dll")]
        private static extern bool mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);


        private const int MOUSEEVENTF_LEFTDOWN = 0x02;
        private const int MOUSEEVENTF_LEFTUP = 0x04;

        public void movment()
        {
            SetCursorPos(x, y);
        }

        public void update(string recived_x, string recived_y)
        {
            ControlPanel.x = Int32.Parse(recived_x);
            ControlPanel.y = Int32.Parse(recived_y);
        }

        public void click()
        {
            mouse_event(MOUSEEVENTF_LEFTDOWN, ControlPanel.x, ControlPanel.y, 0, 0);
            mouse_event(MOUSEEVENTF_LEFTUP, ControlPanel.x, ControlPanel.y, 0, 0);
        }
    }
}
