using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class InputNetInfoForm : Form
    {
        private const string localhost = "127.0.0.1";

        NetInfo netInfo;

        public NetInfo ReturnNetInfo { get; private set; }

        public InputNetInfoForm()
        {
            InitializeComponent();
            netInfo = new NetInfo(localhost, 0);
        }
        
        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                netInfo.Address = txtInputIP.Text;
                netInfo.Port = int.Parse(txtInputPort.Text);

                netInfo = new NetInfo(netInfo.Address, netInfo.Port);

                ReturnNetInfo = netInfo;

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch(Exception ex)
            {
                if (txtInputIP.Text == "")
                {
                    MessageBox.Show("Input IP Address", "Warn", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //txtInputIP.Text = "";
                }
                else if (txtInputPort.Text == "")
                {
                    MessageBox.Show("Input Port", "Warn", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (!int.TryParse(txtInputPort.Text, out _))
                {
                    MessageBox.Show("Input Integer", "Warn", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //txtInputPort.Text = "";
                }
                else
                    MessageBox.Show(ex.Message);
            }
        }
        private void txtInputKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnOK_Click(sender, e);
        }
    }
}
