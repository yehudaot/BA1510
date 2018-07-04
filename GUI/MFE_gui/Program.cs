using System;
using System.Collections.Generic;
using System.Windows.Forms;
using atpLib;

[assembly: log4net.Config.XmlConfigurator(ConfigFile =
                "./log4net.xml", Watch = true)]

namespace mfe_gui
{
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
            Application.Run(new frmMain());
        }
    }
}
