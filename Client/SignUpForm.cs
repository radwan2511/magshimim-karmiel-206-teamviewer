using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.IO;

namespace TeamViewer___Client
{
    public partial class SignUpForm : Form
    {
        public SignUpForm()
        {
            InitializeComponent();
        }

        private void ok_Click(object sender, EventArgs e)
        {
            try
            {
                if (userText.Text != null && passText.Text != null && emailText != null)
                {
                    string password = passText.Text;
                    erorrLabel.Text = string.Empty;
                    string uLen = userText.Text.Length.ToString();
                    string pLen = password.Length.ToString();
                    string eLen = emailText.Text.Length.ToString();
                    if (userText.Text.Length < 10)
                    {
                        uLen = "0" + userText.Text.Length.ToString();
                    }
                    if (password.Length < 10)
                    {
                        pLen = "0" + password.Length.ToString();
                    }
                    if (emailText.Text.Length < 10)
                    {
                        eLen = "0" + emailText.Text.Length.ToString();
                    }
                    
                    LogInScreen.MSG = Constants.SIGN_UP + uLen + userText.Text + pLen + password + eLen + emailText.Text;
                    LogInScreen.msgHandler();
                    if (LogInScreen.RecivedMSG == Constants.SUCCESS)
                    {
                        this.Hide();
                    }
                }
                else
                {
                    erorrLabel.Text = "Error";
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void back_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }
    }
}
