using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace TeamViewer___Client
{
    public partial class GiveControl : Form
    {
        public static int x { get; set; }
        public static int y { get; set; }

        public GiveControl()
        {
            InitializeComponent();
            Thread m = new Thread(movment);
            m.Start();
        }

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool SetCursorPos(int x, int y);

        [DllImport("user32.dll")]
        private static extern bool mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);

        public void movment()
        {
            LogInScreen.reciveMSG();
            int xLen = 3;//Convert.ToInt32(LogInScreen.RecivedMSG.Substring(0,2));
            int x = Convert.ToInt32(LogInScreen.RecivedMSG.Substring(2, xLen));
            int yLen = 3;//Convert.ToInt32(LogInScreen.RecivedMSG.Substring(2+xLen , 2));
            int y = Convert.ToInt32(LogInScreen.RecivedMSG.Substring(2 + xLen + 2, yLen));
            SetCursorPos(x, y);
        }

    }
}
