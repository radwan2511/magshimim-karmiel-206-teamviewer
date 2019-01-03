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

namespace TeamViewer___Client
{
    public partial class LogInScreen : Form
    {
        public static string MSG { get; set; }
        public static string RecivedMSG { get; set; }
        public static TcpClient client;
        public static NetworkStream clientStream;
        public static byte[] key { get; set; }
        public LogInScreen()
        {
            InitializeComponent();
            Thread c = new Thread(serverConnection);
            c.Start();

        }

        private void serverConnection()
        {
            try
            {
                //starts the connection
                client = new TcpClient();
                IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Parse(Constants.LOCAL_HOST), Constants.PORT);
                client.Connect(serverEndPoint);
                NetworkStream clientStream = client.GetStream();
                MSG = "";
                sendMSG();
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
        public static void reciveMSG()
        {
            try
            {
                byte[] bufferIn = new byte[100];
                int bytesRead = clientStream.Read(bufferIn, 0, 100);
                RecivedMSG = new ASCIIEncoding().GetString(bufferIn);
                RecivedMSG = RecivedMSG.Replace("\0","");
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }
        public static void msgHandler()
        {
            sendMSG();
            reciveMSG();
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
                    MSG = MSG.Remove(MSG.Length - 2, 1);
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

        private void LogInButton_Click(object sender, EventArgs e)
        {
            try
            {
                if(usernameBox.Text != "" || passwordBox.Text != "" )
                {
                    string pass = passwordBox.Text;

                    errorLabel.Invoke(new Action(() => errorLabel.Text = Constants.EMPTY));

                    string uLen = (usernameBox.Text.Length < 10) ? Constants.ZERO + usernameBox.Text.Length.ToString() : usernameBox.Text.Length.ToString();
                    string pLen = (pass.Length < 10) ? Constants.ZERO + pass.Length.ToString() : pass.Length.ToString();

                    MSG = Constants.LOG_IN + uLen + usernameBox.Text + pLen + pass;
                    msgHandler();
                    if (RecivedMSG == Constants.SUCCESS)
                    {
                        this.Hide();
                        Form Main = new MainScreen();
                        Main.ShowDialog();
                        this.Show();
                    }
                }
                else if(usernameBox.Text == "" || passwordBox.Text == "")
                {
                    MessageBox.Show("Username or Password is Empty", "Error", MessageBoxButtons.OK);
                }
            }

            catch (Exception)
            {
                MessageBox.Show("Error", "Something Happened", MessageBoxButtons.OK);
            }
        }

        private void SignUpButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form signUp = new SignUpForm();
            signUp.ShowDialog();
            this.Show();
        }

        private void LogInScreen_Load(object sender, EventArgs e)
        {

        }
    }
}
