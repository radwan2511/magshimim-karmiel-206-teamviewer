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
using System.Runtime.InteropServices;

namespace TeamViewer___Client
{
    public partial class DesktopRemoteControl : Form
    {
        public static int x { get; set; }
        public static int y { get; set; }

        public DesktopRemoteControl()
        {
            InitializeComponent();
        }

        private void connectButton_Click(object sender, EventArgs e)
        {
            try
            {
                rdp.Server = "10.68.83.54"; //adress
                //rdp.Domain = "localdomain"; //domain
                rdp.UserName = "admin"; //login
                IMsTscNonScriptable secured = (IMsTscNonScriptable)rdp.GetOcx();
                secured.ClearTextPassword = "mk215681";//password
                rdp.Connect();
                
            }

            catch (Exception Ex)
            {
                MessageBox.Show("Connection Error!!", Ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void disconnectButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (rdp.Connected.ToString() == "1")
                {
                    rdp.Disconnect();
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show("Connection Error!!", Ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool SetCursorPos(int x, int y);

        [DllImport("user32.dll")]
        private static extern bool mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);
        /*XNumbers.Invoke(new Action(() => XNumbers.Text = x.ToString()));
        YNumbers.Invoke(new Action(() => YNumbers.Text = y.ToString()));*/

        private const int MOUSEEVENTF_LEFTDOWN = 0x02;
        private const int MOUSEEVENTF_LEFTUP = 0x04;


        public void movment()
        {
            SetCursorPos(x, y);
        }

        public void click()
        {
            mouse_event(MOUSEEVENTF_LEFTDOWN, x, y, 0, 0);
            mouse_event(MOUSEEVENTF_LEFTUP, x, y, 0, 0);
        }

        public void update(string recived_x, string recived_y)
        {
            DesktopRemoteControl.x = Int32.Parse(recived_x);
            DesktopRemoteControl.x = Int32.Parse(recived_y);
        }

        private void clickbutton_Click(object sender, EventArgs e)
        {
            click();
        }

        private void move_Click(object sender, EventArgs e)
        {
            update(XNumbers.Text.ToString(), YNumbers.Text.ToString());
            movment();
            //sendMove();
        }

        public void sendMove()
        {
            LogInScreen.MSG = Constants.MOVE + (MousePosition.X.ToString()).Length.ToString() + MousePosition.X.ToString() + (MousePosition.Y.ToString()).Length.ToString() + MousePosition.Y.ToString();
            LogInScreen.msgHandler();
            if(LogInScreen.RecivedMSG == Constants.MOVE)
            {
                movment(); 
            }
        }
    }
}
