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
using System.Net;
using System.Net.Sockets;
using static Client.Program;

namespace Client
{

    public partial class Form1 : Form
    {
        public static TcpClient client;
        public static NetworkStream clientStream;

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

                /////////////////////////////////////////

                //start the connection
                client = new TcpClient();
                IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Parse(Constants.LOCAL_HOST), Constants.PORT);
                client.Connect(serverEndPoint);
                NetworkStream clientStream = client.GetStream();

                // this is the send function
                // trying to send image file

               // reading the image file
                BinaryReader binReader = new BinaryReader(File.Open("C:\\Users\\magshimim\\Desktop\\image.jpg", FileMode.Open,FileAccess.Read));
                binReader.BaseStream.Position = 0;
                byte[] binFile = binReader.ReadBytes(Convert.ToInt32(binReader.BaseStream.Length)); // converting the image file
                binReader.Close();
               


                //server.Send(Size, Size.Length, SocketFlags.None);
                clientStream.Write(binFile, 0, binFile.Length);
                clientStream.Flush();
                
                Console.WriteLine("Disconnecting from server ...");
                client.Close();

            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);

                Application.Exit();
            }
        }

    }
}