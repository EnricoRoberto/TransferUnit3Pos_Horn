using System;
using System.Windows.Forms;

namespace TransferUnit3Pos
{
    public partial class ConfigPage : Form 
    {
        private ConfigUserPage configurationPage = new ConfigUserPage();
        public ConfigPage()
        {
            InitializeComponent();
        }
        private void ConfigPage_Load(object sender, EventArgs e)
        {
            configurationPage.Show();

        }
        private void ConfigPage_FormClosing(object sender, FormClosingEventArgs e)
        {

            ConfigPage.ActiveForm.Dispose();
        }

        private void configUserPage1_Load(object sender, EventArgs e)
        {

        }
    }
}
