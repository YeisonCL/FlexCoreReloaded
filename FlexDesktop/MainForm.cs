using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FlexDesktop
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            comboCoin.SelectedItem = 1;
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void mouseHoverConf(object sender, EventArgs e)
        {
            labConf.ForeColor = Color.White;
        }

        private void mouseLeaveConf(object sender, EventArgs e)
        {
            labConf.ForeColor = Color.FromArgb(100, 100, 255);
        }

        private void mainLoad(object sender, EventArgs e)
        {
            labInfo.Text = "";
        }
    }
}
