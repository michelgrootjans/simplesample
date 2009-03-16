using System;
using System.Windows.Forms;
using Logics;

namespace VraagMichel
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public IAuthenticationService Authenticator { get; set; }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                var barcode = "1234567890";
                var user = Authenticator.Login("1234567890");
                MessageBox.Show(string.Format("Logged in as {0} with code {1}", user.Name, barcode));
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }
    }
}
