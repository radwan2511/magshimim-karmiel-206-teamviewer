using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace file_transfer
{
    static class Constants // defines
    {
        public const string LOCAL_HOST = "127.0.0.1",
        STATISTIC_MSG = "260";
    }

    static class Program
    {
        //What we are using for ID generation
        public static Random Rand;
        [STAThread]
        static void Main()
        {
            Rand = new Random();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Main());
        }
    }
}
