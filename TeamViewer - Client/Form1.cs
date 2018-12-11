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
using System.Windows.Forms;
using System.Security.Cryptography;
using System.IO;
using MSTSCLib;

namespace TeamViewer___Client
{

    public partial class MainScreen : Form
    {
        public static string MSG { get; set; }
        public static string RecivedMSG { get; set; }
        public static TcpClient client;
        public static NetworkStream clientStream;
        public static byte[] key { get; set; }
        public MainScreen()
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
                MSG = "";
                sendTextFile();
                reciveTextFile();
                //label1.Invoke(new Action(() => label1.Text = RecivedMSG));
                


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
                byte[] bufferIn = new byte[100];
                int bytesRead = clientStream.Read(bufferIn, 0, 100);
                RecivedMSG = new ASCIIEncoding().GetString(bufferIn);
                RecivedMSG = RecivedMSG.Replace("\0", string.Empty);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private static void sendTextFile()
        {
            try
            {
                //gets the path of the file 
                string[] lines = System.IO.File.ReadAllLines(@"C:\Users\magshimim\Desktop\Teamviewer\TeamViewer - Client\info.txt");
                foreach (string line in lines)
                {
                    MSG = MSG + line + "#";
                    MSG = MSG.Remove(MSG.Length-2, 1);
                }
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

         private static void reciveTextFile()
        {
            try
            {
                byte[] bufferIn = new byte[10000];
                int bytesRead = clientStream.Read(bufferIn, 0, 10000);
                RecivedMSG = new ASCIIEncoding().GetString(bufferIn);
                RecivedMSG = RecivedMSG.Replace("\0", string.Empty);
                string[] lines = RecivedMSG.Split('#');
                int last = lines.Length - 1;
                lines[last] = null;
                System.IO.File.WriteAllLines(@"C:\Users\magshimim\Desktop\new.txt", lines);
            }

            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void connectButton_Click(object sender, EventArgs e)
        {
            try
            {
                //rdp.Server = "magshimim";
                //rdp.UserName = "Vihorev's Connection";

                
                
                rdp.Server = "192.168.0.101"; //adress
                //rdp.Domain = "localdomain"; //domain
                rdp.UserName = "test"; //login
                IMsTscNonScriptable secured = (IMsTscNonScriptable)rdp.GetOcx();
                secured.ClearTextPassword = "123456";//password
                rdp.Connect();
            }

            catch(Exception Ex)
            {
                MessageBox.Show("Connection Error!!", Ex.Message , MessageBoxButtons.OK , MessageBoxIcon.Error);
            }
        }

        private void disconnectButton_Click(object sender, EventArgs e)
        {
            try
            {
                if(rdp.Connected.ToString() == "1")
                {
                    rdp.Disconnect();
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show("Connection Error!!", Ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
