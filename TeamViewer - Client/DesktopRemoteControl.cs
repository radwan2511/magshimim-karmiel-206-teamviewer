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
    public partial class DesktopRemoteControl : Form
    {
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
                //rdp.Invoke(new Action(() => rdp.Connect()));
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
    }
}
