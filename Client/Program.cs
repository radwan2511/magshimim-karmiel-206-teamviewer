using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TeamViewer___Client
{
    static class Constants // defines
    {
        public const string IP = "192.168.1.91",
        LOG_IN = "200",
        LOG_OUT = "201",//NOT USED
        SIGN_UP = "203",
        FAIL = "299",
        ZERO = "0",
        SUCCESS = "206",
        MOVE = "220",
        CLICK = "103",
        CREATE_ROOM = "213",
        JOIN_ROOM = "209";
        public const int PORT = 8820,
        STRING_PORT = 8000,
        SCREEN_PORT = 13000;
    }
        static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new LogInScreen());
        }
    }
}
