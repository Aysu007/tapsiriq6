using System;
using System.Windows.Forms;

namespace PulXirdalama
{
    static class Program
    {
        /// <summary>
        /// Tətbiqin əsas giriş nöqtəsi.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
