using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TeamViewer___Client
{
    static class Constants // defines
    {
        public const string LOCAL_HOST = "127.0.0.1",
        EMPTY = "",
        SIGNIN = "100",
        SIGNUP = "101",
        Fail = "200",
        ZERO = "0",
        SUCCESS = "206",
        MOVE = "102" ,
        CLICK = "103" ;
        public const int PORT = 8820;
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
            //Application.Run(new LogInScreen());
            Application.Run(new DesktopRemoteControl());
        }
    }
}
