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

namespace client_ppp
{
    public partial class LogInScreen : Form
    {
        public static string MSG { get; set; }
        public static string RecivedMSG { get; set; }
        public static TcpClient client { get; set; }
        public static NetworkStream clientStream { get; set; }
        public static IPEndPoint serverEndPoint { get; set; }
        public static NetworkStream clientStream2 { get; set; }
        public static TcpClient client3 { get; set; }
        public static byte[] key { get; set; }
        public LogInScreen()
        {
            InitializeComponent();
            //Init_Data();
            Thread c = new Thread(serverConnection);
            c.Start();

        }

        private void serverConnection()
        {
            try
            {
                //starts the connection
                client = new TcpClient();
                // change ip here
                // ip for computer that runs the server on it
                serverEndPoint = new IPEndPoint(IPAddress.Parse("10.0.0.36"), Constants.PORT);
                client.Connect(serverEndPoint);
                clientStream = client.GetStream();
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
                if(usernameBox.Text != string.Empty || passwordBox.Text != string.Empty)
                {
                    string pass = passwordBox.Text;

                    errorLabel.Invoke(new Action(() => errorLabel.Text = string.Empty));

                    string uLen = (usernameBox.Text.Length < 10) ? Constants.ZERO + usernameBox.Text.Length.ToString() : usernameBox.Text.Length.ToString();
                    string pLen = (pass.Length < 10) ? Constants.ZERO + pass.Length.ToString() : pass.Length.ToString();

                    MSG = Constants.LOG_IN + uLen + usernameBox.Text + pLen + pass;
                    msgHandler();
                    if (RecivedMSG == Constants.SUCCESS)
                    {
                        //save_data();
                        this.Hide();

                        //MSG = Constants.LOG_OUT;
                        //byte[] buffer = new ASCIIEncoding().GetBytes(MSG);
                        //clientStream.Write(buffer, 0, buffer.Length);
                        //clientStream.Flush();
                        //client.Close();


                        //MainScreen Main = new MainScreen();
                        //Main.Show();

                        Option opt = new Option();
                        opt.Show();
                        ///////////////////////////////////////////////////////////////////////////


                        //this.Show();
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
        /*
        private void Init_Data()
        {
            if(Properties.Settings.Default.Username != string.Empty)
            {
                if (Properties.Settings.Default.Remember == "yes")
                {
                    usernameBox.Text = Properties.Settings.Default.Username;
                    passwordBox.Text = Properties.Settings.Default.Password;
                }
                else
                {
                    usernameBox.Text = Properties.Settings.Default.Username;
                }
            }
            
        }

        private void save_data()
        {
            if (remeberBox.Checked)
            {
                Properties.Settings.Default.Username = usernameBox.Text;
                Properties.Settings.Default.Password = passwordBox.Text;
                Properties.Settings.Default.Remember = "yes";
                Properties.Settings.Default.Save();
            }
            else
            {
                Properties.Settings.Default.Username = usernameBox.Text;
                Properties.Settings.Default.Password = "";
                Properties.Settings.Default.Remember = "no";
                Properties.Settings.Default.Save();
            }
        }*/
    }
}
