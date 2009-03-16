using System;
using System.Windows.Forms;
using Logics;

namespace VraagMichel
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
            var form = new Form1{Authenticator = new ExceptionHanldingAuthenticationService(new AuthenticationService(new DummyUserRao()))};
            Application.Run(form);
        }
    }
}
