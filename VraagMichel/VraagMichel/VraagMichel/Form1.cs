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

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                MainFactory.Instance.Login("1234567890");
            }
            catch (Exception)
            {
                MessageBox.Show(MainFactory.Instance.ErrorMsg);
            }
        }
    }
}
