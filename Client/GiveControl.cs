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
    public partial class GiveControl : Form
    {
        //private PictureBox pictureBox2;
        public static TcpClient client3 { get; set; }
        public static TcpListener listener { get; set; }
        
        public static int x { get; set; }
        public static int y { get; set; }
        public static string Massage { get; set; }

        public GiveControl()
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
                /*LogInScreen.client = new TcpClient();
                LogInScreen.serverEndPoint = new IPEndPoint(IPAddress.Parse(Constants.LOCAL_HOST), Constants.PORT);
                LogInScreen.client.Connect(LogInScreen.serverEndPoint);
                LogInScreen.clientStream = client.GetStream();*/

                LogInScreen.clientStream.Read(b, 0, 100);
                System.Text.UTF8Encoding enc = new System.Text.UTF8Encoding();

                string result = enc.GetString(b);

                string st = "aa";
                string iip = "";
                while (result[0] == 'a')
                {
                    LogInScreen.clientStream.Read(b, 0, 16);
                    st = enc.GetString(b);
                    int i = 0;

                    while (System.Char.IsDigit(st[i]) || st[i] == '.')
                    {
                        iip = iip + st[i];
                        i++;
                    }

                    TcpClient cl = new TcpClient();
                    LogInScreen.client.Connect(iip, Constants.CLIENT_PORT);

                    while (1 == 1)
                    {
                        try
                        {
                            //ControlPanel.Massage = XNumbers.Text + YNumbers.Text;
                            ControlPanel.Massage = "220500900";
                            //sending the mouse coordinates
                            BinaryFormatter binFormatter = new BinaryFormatter();
                            LogInScreen.clientStream2 = cl.GetStream();
                            binFormatter.Serialize(LogInScreen.clientStream2, ControlPanel.Massage);
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


                    client3 = new TcpClient();
                    listener = new TcpListener(Constants.CLIENT_PORT);
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
                        update(newOne.Substring(0, 3), newOne.Substring(0, 3));
                        movment();
                        //label1.Invoke(new Action(() => label1.Text = newOne));
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
            ControlPanel.x = Int32.Parse(recived_y);
        }

        private void move_Click(object sender, EventArgs e)
        {/*
            ControlPanel.Massage = "500900";
            //sending the mouse coordinates
            BinaryFormatter binFormatter = new BinaryFormatter();
            clientStream2 = cl.GetStream();
            binFormatter.Serialize(clientStream2, ControlPanel.Massage);*/
        }
        private void clickbutton_Click(object sender, EventArgs e)
        {
            ;
        }
    }
}
