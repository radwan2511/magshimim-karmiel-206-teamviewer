using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace client_ppp
{
    public partial class Option : Form
    {
        public Option()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            MainScreen Main = new MainScreen();
            Main.Show();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Main file_transfer = new Main();
            file_transfer.Show();
        }
    }
}
